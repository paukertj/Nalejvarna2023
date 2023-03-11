using Microsoft.CodeAnalysis;

namespace SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis
{
    internal interface ISemanticAnalysisService
    {
        bool IsAutomapperInvocation(SyntaxNode syntaxNode);

        ExistingMappingMapDescription GetExistingMapping(SyntaxNode syntaxNode);

        NewMappingMember GetNewMappingTarget(SyntaxNode syntaxNode);

        NewMappingMember GetNewMappingSource(SyntaxNode syntaxNode);
    }
}
