﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceGeneratorDemo.Generator.Mapping.Exceptions;
using SourceGeneratorDemo.Generator.Mapping.Services.Mapping;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SourceGeneratorDemo.Generator.Mapping.Services.CodeGenerating
{
    internal sealed class CodeGeneratingService : ICodeGeneratingService
    {
        private IReadOnlyDictionary<string, ExistingMappingDescription> _definedMaps = null;

        private readonly IMappingService _mappingService;

        public CodeGeneratingService(IMappingService mappingService)
        {
            _mappingService = mappingService;
        }

        public string GenerateCode(IEnumerable<NewMappingDescription> mappingDescriptions)
        {
            var maps = new List<StatementSyntax>();

            foreach (var mappingDescription in mappingDescriptions)
            {
                EnsureMapping(mappingDescription);

                var map = GenerateMap(mappingDescription);
                maps.Add(map);
            }

            return GenerateClass(maps)
                    .NormalizeWhitespace()
                    .SyntaxTree
                    .GetText()
                    .ToString();
        }

        private CompilationUnitSyntax GenerateClass(List<StatementSyntax> mappings)
        {
            return CompilationUnit()
                .WithUsings(
                    SingletonList<UsingDirectiveSyntax>(
                        UsingDirective(
                            IdentifierName("AutoMapper"))))
                .WithMembers(
                    SingletonList<MemberDeclarationSyntax>(
                        ClassDeclaration("GeneratedProfile")
                        .WithModifiers(
                            TokenList(
                                Token(
                                    TriviaList(
                                        new[]{
                                            Comment("// <auto-generated> "),
                                            Comment("// This code was generated by a tool."),
                                            Comment("// </auto-generated> ")}),
                                    SyntaxKind.PublicKeyword,
                                    TriviaList())))
                        .WithBaseList(
                            BaseList(
                                SingletonSeparatedList<BaseTypeSyntax>(
                                    SimpleBaseType(
                                        IdentifierName("Profile")))))
                        .WithMembers(
                            SingletonList<MemberDeclarationSyntax>(
                                ConstructorDeclaration(
                                    Identifier("GeneratedProfile"))
                                .WithModifiers(
                                    TokenList(
                                        Token(SyntaxKind.PublicKeyword)))
                                .WithBody(Block(mappings))))));
        }

        private StatementSyntax GenerateMap(NewMappingDescription mappingDescription)
        {
            return 
                ExpressionStatement(
                    InvocationExpression(
                        GenericName(
                            Identifier("CreateMap"))
                        .WithTypeArgumentList(
                            TypeArgumentList(
                                SeparatedList<TypeSyntax>(
                                    new SyntaxNodeOrToken[]{
                                        IdentifierName(mappingDescription.Source.Type.ToDisplayString()),
                                        Token(SyntaxKind.CommaToken),
                                        IdentifierName(mappingDescription.Target.Type.ToDisplayString())})))));
        }

        private void EnsureMapping(NewMappingDescription mappingDescription)
        {
            var sourceProperties = mappingDescription.Source.PropertyMethod
                .Select(p => new PropertyDescription(p))
                .ToList();

            var targetProperties = mappingDescription.Target.PropertyMethod
                .Select(p => new PropertyDescription(p))
                .ToList();

            var orphanTargetProperties = targetProperties
                .GroupJoin(sourceProperties, t => t.Name, s => s.Name, (t, s) => new
                {
                    TargetProperty = t,
                    HasAnySource = s.Any()
                })
                .Where(p => p.HasAnySource == false)
                .Select(p => p.TargetProperty)
                .ToList();

            if (orphanTargetProperties.Count <= 0)
            {
                return; // All properties are same, there is nothing to resolve
            }

            var unresolvedMappings = GetUnresolvedMappings(mappingDescription, orphanTargetProperties);

            if (unresolvedMappings != null && unresolvedMappings.Count == 0)
            {
                return; // There are orphans, but there is already manual mapping for them, so automapper can map them
            }

            string errorMessage =
                $"Unable to map '{mappingDescription.Source.Type.ToDisplayString()}' -> '{mappingDescription.Target.Type.ToDisplayString()}':" +
                "There is no map defined for target properties:" +
                string.Join(", ", unresolvedMappings.Select(p => $"'{p.FullName}'")).TrimEnd();

            throw new InvalidMappingException(errorMessage);
        }

        private List<PropertyDescription> GetUnresolvedMappings(NewMappingDescription mappingDescription, List<PropertyDescription> orphanTargetProperties)
        {
            var unmappedProperties = new List<PropertyDescription>();

            var definedMaps = GetDefinedMap(mappingDescription);

            if (definedMaps == null)
            {
                return orphanTargetProperties; // There is no manual mapping defined
            }

            foreach (var orphanTargetProperty in orphanTargetProperties) // Caution, high complexity O(n^2)
            {
                bool mappingFound = false;

                foreach (var alreadyExistingMapping in definedMaps.AlreadyExistingMappings)
                {
                    if (orphanTargetProperty.Name == alreadyExistingMapping.FromPropertyName || orphanTargetProperty.Name == alreadyExistingMapping.ToPropertyName)
                    {
                        mappingFound = true;
                        break;
                    }
                }

                if (mappingFound == false)
                {
                    unmappedProperties.Add(orphanTargetProperty); // I want callect all incorrect mappings
                }
            }

            return unmappedProperties;
        }

        private ExistingMappingDescription GetDefinedMap(NewMappingDescription mappingDescription)
        {
            if (_definedMaps == null)
            {
                _definedMaps = _mappingService
                    .GetDefinedMaps()
                    .ToDictionary(kp => kp.GetIdentifier());
            }

            if (_definedMaps.TryGetValue(mappingDescription.GetIdentifier(), out var existingMappingDescription))
            {
                return existingMappingDescription;
            }

            return null;
        }
    }
}