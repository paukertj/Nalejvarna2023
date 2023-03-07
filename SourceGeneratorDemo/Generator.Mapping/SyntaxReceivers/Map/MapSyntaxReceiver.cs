using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SourceGeneratorDemo.Generator.Mapping.SyntaxReceivers.Map
{
    internal sealed class MapSyntaxReceiver : BaseSyntaxReceiver<GenericNameSyntax>, IMapSyntaxReceiver
    {
        public MapSyntaxReceiver() : base("Map")
        {
        }
    }
}
