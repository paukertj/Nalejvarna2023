using Microsoft.CodeAnalysis;

namespace SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis
{
    internal sealed record ExistingPropertyMappingMember
    {
        public IMethodSymbol FromPropertyMethod { get; set; }

        public IMethodSymbol ToPropertyMethod { get; set; }
    }
}
