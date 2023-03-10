using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceGeneratorDemo.Generator.Mapping.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SourceGeneratorDemo.Generator.Mapping.Services.CodeGenerating
{
    internal sealed class CodeGeneratingService : ICodeGeneratingService
    {
        public string GenerateCode(IEnumerable<NewMappingDescription> mappingDescriptions)
        {
            var compilationUnit = CompilationUnit();

            foreach (var mappingDescription in mappingDescriptions)
            {
                EnsureMapping(mappingDescription);

                compilationUnit = GenerateMap(mappingDescription, compilationUnit);
            }

            return compilationUnit
                    .SyntaxTree
                    .GetText()
                    .ToString();
        }

        private CompilationUnitSyntax GenerateMap(NewMappingDescription mappingDescription, CompilationUnitSyntax compilationUnit)
        {
            return compilationUnit
                .WithMembers(
                    SingletonList<MemberDeclarationSyntax>(
                        GlobalStatement(
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
                                                    IdentifierName(mappingDescription.Target.Type.ToDisplayString())}))))))))
                .NormalizeWhitespace();
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
                .ToList();

            if (orphanTargetProperties.Count <= 0)
            {
                return;
            }

            string errorMessage =
                $"Unable to map '{mappingDescription.Source.Type.ToDisplayString()}' -> '{mappingDescription.Target.Type.ToDisplayString()}':\r\n" +
                "\tThere is no map defined for target properties:\r\n" +
                "\t\t" + string.Join("\t\t", orphanTargetProperties.Select(p => $"'{p.TargetProperty.FullName}'\r\n")).TrimEnd();

            throw new Exception(errorMessage);
        }
    }
}
