using Microsoft.CodeAnalysis;

namespace SourceGeneratorDemo.Generator.Mapping.Extensions
{
    internal static class SyntaxNodeExtensions
    {
        internal static T FirstParentOfTypeAndSelf<T>(this SyntaxNode syntaxNode)
            where T : SyntaxNode
        {
            if (syntaxNode == null)
            {
                return null;
            }

            if (syntaxNode is T t)
            {
                return t;
            }

            return FirstParentOfTypeAndSelf<T>(syntaxNode.Parent);
        }
    }
}
