using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace SourceGeneratorDemo.Generator.Mapping.SyntaxReceivers
{
    internal abstract class BaseSyntaxReceiver<TSyntaxNode>
        where TSyntaxNode : SimpleNameSyntax
    {
        private readonly List<TSyntaxNode> _identifiers;
        private readonly string _identifier;

        protected BaseSyntaxReceiver(string identifier)
        {
            _identifiers = new List<TSyntaxNode>();
            _identifier = identifier;
        }

        public IReadOnlyList<TSyntaxNode> GetSyntaxNode()
        {
            return _identifiers;
        }

        public virtual void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is not TSyntaxNode genericNameSyntax)
            {
                return;
            }

            if (genericNameSyntax.Identifier.ValueText == _identifier)
            {
                _identifiers.Add(genericNameSyntax);
            }
        }
    }
}
