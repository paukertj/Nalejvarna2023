using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis
{
    internal sealed class SemanticAnalysisService : ISemanticAnalysisService
    {
        private readonly GeneratorExecutionContext _context;

        public SemanticAnalysisService(GeneratorExecutionContext context)
        {
            _context = context;
        }

        public ExistingMappingMapDescription GetExistingMapping(SyntaxNode syntaxNode)
        {
            // Here I'll get all generics - CreateMap<TSource, TTarget>(), I'll have them in List, so it will be CreateMap<First, Last>()
            var genericArguments = syntaxNode
                .DescendantNodes()
                .OfType<IdentifierNameSyntax>()
                .ToList();

            if (genericArguments.Count != 2)
            {
                throw new Exception($"Unexpected count of generic arguments in CreateMap invocation, got '{genericArguments.Count}' but '2' expected");
            }

            var semanticModel = _context.Compilation.GetSemanticModel(syntaxNode.SyntaxTree);

            // Now investigateing CreateMap<First, Last>(), we need to know types first
            var source = semanticModel.GetTypeInfo(genericArguments.First()).Type;
            var target = semanticModel.GetTypeInfo(genericArguments.Last()).Type;

            return new ExistingMappingMapDescription
            {
                Source = source,
                Target = target
            };
        }

        public NewMappingMember GetNewMappingSource(SyntaxNode syntaxNode)
        {
            var argumentSyntax = syntaxNode
                .DescendantNodes()
                .OfType<IdentifierNameSyntax>()
                .LastOrDefault();

            if (argumentSyntax == null)
            {
                return null;
            }

            // argumentSyntax should be node that represents the source argument, so f.e. in this case '_mapper.Map<object>(o)' this should be IdentifierNameSyntax of 'o'

            var semanticModel = _context.Compilation.GetSemanticModel(syntaxNode.SyntaxTree);

            var declarationTypeInfo = semanticModel.GetTypeInfo(argumentSyntax);

            // Give me all public getters that are available on the object
            var propertiesWithGetters = GetPublicMethodSymbols(declarationTypeInfo.Type, MethodKind.PropertyGet);

            return new NewMappingMember
            {
                PropertyMethod = propertiesWithGetters,
                PropertyMethodsKind = MethodKind.PropertyGet,
                Type = declarationTypeInfo.Type,
            };
        }

        public NewMappingMember GetNewMappingTarget(SyntaxNode syntaxNode)
        {
            var predefinedTypeSyntax = syntaxNode
                .DescendantNodes()
                .OfType<IdentifierNameSyntax>()
                .LastOrDefault();

            if (predefinedTypeSyntax == null)
            {
                return null;
            }

            // predefinedTypeSyntax should be node that represents the target generics, so f.e. in this case '_mapper.Map<object>(o)' this should be IdentifierNameSyntax of 'object'

            var semanticModel = _context.Compilation.GetSemanticModel(syntaxNode.SyntaxTree);

            var declarationTypeInfo = semanticModel.GetTypeInfo(predefinedTypeSyntax);

            // Give me all public setters that are available on the object
            var propertiesWithSetters = GetPublicMethodSymbols(declarationTypeInfo.Type, MethodKind.PropertySet);

            return new NewMappingMember
            {
                PropertyMethod = propertiesWithSetters,
                PropertyMethodsKind = MethodKind.PropertySet,
                Type = declarationTypeInfo.Type,
            };
        }

        public bool IsAutomapperInvocation(SyntaxNode syntaxNode)
        {
            if (syntaxNode == null)
            {
                return false;
            }

            var semanticModel = _context.Compilation.GetSemanticModel(syntaxNode.SyntaxTree);

            var declarationTypeInfo = semanticModel.GetTypeInfo(syntaxNode);

            string ns = declarationTypeInfo.Type?.ContainingNamespace?.ToDisplayString() ?? string.Empty;

            return ns.StartsWith("AutoMapper");
        }

        private IEnumerable<IMethodSymbol> GetPublicMethodSymbols(ITypeSymbol typeSymbol, MethodKind methodKind)
        {
            if (typeSymbol == null)
            {
                // If this is happen, it is indicates bigger problem

                throw new Exception($"Unable to get type for '{nameof(ITypeSymbol)}'");
            }

            var symbols = typeSymbol
                .GetMembers()
                .OfType<IMethodSymbol>()
                .Where(m => m.MethodKind == methodKind && m.DeclaredAccessibility == Accessibility.Public);

            return symbols;
        }
    }
}
