using SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis;

namespace SourceGeneratorDemo.Generator.Mapping.Services.Mapping
{
    public sealed record NewMappingDescription
    {
        public NewMappingMember Source { get; set; }

        public NewMappingMember Target { get; set; }
    }
}
