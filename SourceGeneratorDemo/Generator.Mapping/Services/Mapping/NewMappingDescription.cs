using SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis;

namespace SourceGeneratorDemo.Generator.Mapping.Services.Mapping
{
    public sealed class NewMappingDescription
    {
        public NewMappingMember Source { get; set; }

        public NewMappingMember Target { get; set; }

        private string _identifier = null;

        public string GetIdentifier()
        {
            if (_identifier == null)
            {
                _identifier = Source?.Type?.ToDisplayString() + ',' + Target?.Type?.ToDisplayString();
            }

            return _identifier;
        }
    }
}
