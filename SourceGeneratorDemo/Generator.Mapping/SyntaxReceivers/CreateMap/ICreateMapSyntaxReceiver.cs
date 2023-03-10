using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace SourceGeneratorDemo.Generator.Mapping.SyntaxReceivers.CreateMap
{
    internal interface ICreateMapSyntaxReceiver : ISyntaxReceiver
    {
        IReadOnlyList<GenericNameSyntax> GetSyntaxNode();
    }
}
