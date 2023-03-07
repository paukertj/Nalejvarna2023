using Microsoft.CodeAnalysis;

namespace SourceGeneratorDemo.Generator.Mapping.Services.CodeGenerating
{
    internal sealed record PropertyDescription
    {
        public string Name { get; }

        public string FullName { get; }

        public PropertyDescription(IMethodSymbol methodSymbol)
        {
            var displayString = methodSymbol.ToDisplayString();
            int lastIndex = displayString.LastIndexOf('.');

            if (lastIndex <= 0)
            {
                FullName = displayString;
                Name = displayString;
            }
            else
            {
                FullName = displayString.Substring(0, lastIndex);
                int fullNameIndex = FullName.LastIndexOf('.');
                Name = fullNameIndex > 0 
                    ? FullName.Substring(fullNameIndex + 1, FullName.Length - fullNameIndex - 1)
                    : FullName;
            }
        }
    }
}
