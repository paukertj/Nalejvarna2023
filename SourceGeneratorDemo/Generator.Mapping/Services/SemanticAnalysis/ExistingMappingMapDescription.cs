using Microsoft.CodeAnalysis;

namespace SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis
{
    public sealed record ExistingMappingMapDescription
    {
        public ITypeSymbol Source { get; set; }

        public ITypeSymbol Target { get; set; }
    }
}
