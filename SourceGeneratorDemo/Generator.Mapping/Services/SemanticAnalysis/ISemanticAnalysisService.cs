using Microsoft.CodeAnalysis;

namespace SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis
{
    internal interface ISemanticAnalysisService
    {
        bool IsAutomapperInvocation(SyntaxNode syntaxNode);

        void GetTargetProperties(SyntaxNode syntaxNode);
    }
}
