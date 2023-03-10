using SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis;
using System.Collections.Generic;

namespace SourceGeneratorDemo.Generator.Mapping.Services.Mapping
{
    internal sealed record ExistingMappingDescription
    {
        public NewMappingMember Source { get; set; }

        public NewMappingMember Target { get; set; }

        public IEnumerable<ExistingPropertyMappingMember> AlreadyExistingMappings { get; set; }
    }
}
