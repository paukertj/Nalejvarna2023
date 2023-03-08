using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis
{
    internal class SemanticAnalysisService : ISemanticAnalysisService
    {
        private readonly GeneratorExecutionContext _context;

        public SemanticAnalysisService(GeneratorExecutionContext context)
        {
            _context = context;
        }

        public bool IsAutomapperInvocation(SyntaxNode syntaxNode)
        {
            var firstParentNode = syntaxNode.Parent?
                .DescendantNodes()?
                .OfType<NameSyntax>()?
                .FirstOrDefault();

            // firstParentNode should be node that represents the Mapper, so f.e. in this case _mapper.Map<object>(o) this shoudl be NameSyntax of _mapper

            if (firstParentNode == null)
            {
                return false;
            }

            var semanticModel = _context.Compilation.GetSemanticModel(firstParentNode.SyntaxTree);

            var declarationTypeInfo = semanticModel.GetTypeInfo(firstParentNode);

            return declarationTypeInfo.Type?.ContainingNamespace?.ToDisplayString() == "AutoMapper";
        }
    }
}
