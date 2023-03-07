using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace SourceGeneratorDemo.Generator.Mapping.SyntaxReceivers.Map
{
    internal interface IMapSyntaxReceiver : ISyntaxReceiver
    {
        IReadOnlyList<GenericNameSyntax> GetSyntaxNode();
    }
}
