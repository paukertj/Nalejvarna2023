using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SourceGeneratorDemo.Generator.Mapping.SyntaxReceivers.CreateMap
{
    internal sealed class CreateMapSyntaxReceiver : BaseSyntaxReceiver<GenericNameSyntax>, ICreateMapSyntaxReceiver
    {
        public CreateMapSyntaxReceiver() : base("CreateMap")
        {
        }
    }
}
