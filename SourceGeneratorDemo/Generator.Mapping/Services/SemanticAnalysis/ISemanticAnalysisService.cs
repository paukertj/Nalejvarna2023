using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis
{
    internal interface ISemanticAnalysisService
    {
        bool IsAutomapperInvocation(SyntaxNode syntaxNode);

        MappingMember GetMappingTarget(SyntaxNode syntaxNode);

        MappingMember GetMappingSource(SyntaxNode syntaxNode);
    }
}
