using Microsoft.CodeAnalysis;

namespace SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis
{
    internal sealed record ExistingPropertyMappingMember
    {
        public string FromPropertyName { get; set; }

        public string ToPropertyName { get; set; }
    }
}
