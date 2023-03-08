using Microsoft.CodeAnalysis;
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

        public MappingMember GetMappingSource(SyntaxNode syntaxNode)
        {
            var argumentSyntax = syntaxNode
                .Parent
                .Parent
                .DescendantNodes()
                .OfType<IdentifierNameSyntax>()
                .Last();

            if (argumentSyntax == null)
            {
                return null;
            }

            // argumentSyntax should be node that represents the source argument, so f.e. in this case '_mapper.Map<object>(o)' this should be IdentifierNameSyntax of 'o'

            var semanticModel = _context.Compilation.GetSemanticModel(syntaxNode.SyntaxTree);

            var declarationTypeInfo = semanticModel.GetTypeInfo(argumentSyntax);

            // Give me all public getters that are available on the object
            var propertiesWithGetters = GetPublicMethodSymbols(declarationTypeInfo.Type, MethodKind.PropertyGet);

            return new MappingMember
            {
                PropertyMethod = propertiesWithGetters,
                PropertyMethodsKind = MethodKind.PropertyGet,
                Type = declarationTypeInfo.Type,
            };
        }

        public MappingMember GetMappingTarget(SyntaxNode syntaxNode)
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

            if (declarationTypeInfo.Type == null)
            {
                // If this is happen, it is indicates bigger problem

                throw new Exception($"Unable to get type for '{nameof(IdentifierNameSyntax)}' node.");
            }

            // Give me all public setters that are available on the object
            var propertiesWithSetters = GetPublicMethodSymbols(declarationTypeInfo.Type, MethodKind.PropertySet);

            return new MappingMember
            {
                PropertyMethod = propertiesWithSetters,
                PropertyMethodsKind = MethodKind.PropertySet,
                Type = declarationTypeInfo.Type,
            };
        }

        public bool IsAutomapperInvocation(SyntaxNode syntaxNode)
        {
            var firstParentNode = syntaxNode.Parent?
                .DescendantNodes()?
                .OfType<NameSyntax>()?
                .FirstOrDefault();

            // firstParentNode should be node that represents the Mapper, so f.e. in this case '_mapper.Map<object>(o)' this should be NameSyntax of '_mapper'

            if (firstParentNode == null)
            {
                return false;
            }

            var semanticModel = _context.Compilation.GetSemanticModel(firstParentNode.SyntaxTree);

            var declarationTypeInfo = semanticModel.GetTypeInfo(firstParentNode);

            return declarationTypeInfo.Type?.ContainingNamespace?.ToDisplayString() == "AutoMapper";
        }

        private IEnumerable<IMethodSymbol> GetPublicMethodSymbols(ITypeSymbol typeSymbol, MethodKind methodKind)
        {
            var symbols = typeSymbol
                .GetMembers()
                .OfType<IMethodSymbol>()
                .Where(m => m.MethodKind == methodKind && m.DeclaredAccessibility == Accessibility.Public);

            return symbols;
        }
    }
}
