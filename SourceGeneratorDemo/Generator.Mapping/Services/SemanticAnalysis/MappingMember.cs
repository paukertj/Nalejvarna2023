using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis
{
    public sealed record MappingMember
    {
        public IEnumerable<IMethodSymbol> PropertyMethod { get; set; }

        public MethodKind PropertyMethodsKind { get; set; }

        public ITypeSymbol Type { get; set; }
    }
}
