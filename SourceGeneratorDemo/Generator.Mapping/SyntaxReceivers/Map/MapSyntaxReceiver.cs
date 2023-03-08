using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace SourceGeneratorDemo.Generator.Mapping.SyntaxReceivers.Map
{
    internal class MapSyntaxReceiver : IMapSyntaxReceiver
    {
        private readonly List<GenericNameSyntax> _mapperIdentifiers = new List<GenericNameSyntax>();

        public IReadOnlyList<GenericNameSyntax> GetGenericNameSyntax()
        {
            return _mapperIdentifiers;
        }

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is not GenericNameSyntax genericNameSyntax)
            {
                return;
            }

            if (genericNameSyntax.Identifier.ValueText == "Map")
            {
                _mapperIdentifiers.Add(genericNameSyntax);
            }
        }
    }
}
