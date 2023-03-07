using Microsoft.CodeAnalysis;
using SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis;
using System.Collections.Generic;

namespace SourceGeneratorDemo.Generator.Mapping.Services.Mapping
{
    internal sealed class ExistingMappingDescription
    {
        public ITypeSymbol Source { get; set; }

        public ITypeSymbol Target { get; set; }

        public IEnumerable<ExistingPropertyMappingMember> AlreadyExistingMappings { get; set; }

        private string _identifier = null;

        public string GetIdentifier()
        {
            if (_identifier == null)
            {
                _identifier = Source?.ToDisplayString() + ',' + Target?.ToDisplayString();
            }

            return _identifier;
        }
    }
}
