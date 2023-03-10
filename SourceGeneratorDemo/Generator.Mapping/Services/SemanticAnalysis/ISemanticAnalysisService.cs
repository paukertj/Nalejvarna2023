using Microsoft.CodeAnalysis;

namespace SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis
{
    internal interface ISemanticAnalysisService
    {
        bool IsAutomapperInvocation(SyntaxNode syntaxNode);

        ITypeSymbol GetExistingMappingSource(SyntaxNode syntaxNode);

        ITypeSymbol GetExistingMappingTarget(SyntaxNode syntaxNode);

        NewMappingMember GetNewMappingTarget(SyntaxNode syntaxNode);

        NewMappingMember GetNewMappingSource(SyntaxNode syntaxNode);
    }
}
