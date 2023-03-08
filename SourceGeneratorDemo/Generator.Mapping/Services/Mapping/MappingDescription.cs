using SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis;

namespace SourceGeneratorDemo.Generator.Mapping.Services.Mapping
{
    public sealed record MappingDescription
    {
        public MappingMember Source { get; set; }

        public MappingMember Target { get; set; }
    }
}
