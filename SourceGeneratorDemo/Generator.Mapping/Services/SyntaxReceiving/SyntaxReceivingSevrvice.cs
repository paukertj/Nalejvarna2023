using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace SourceGeneratorDemo.Generator.Mapping.Services.SyntaxReceiving
{
    internal class SyntaxReceivingSevrvice : ISyntaxReceivingSevrvice
    {
        private readonly Dictionary<Type, ISyntaxReceiver> _receivers = new Dictionary<Type, ISyntaxReceiver>();

        public T GetSyntaxReceiver<T>() 
            where T : ISyntaxReceiver
        {
            if (_receivers.TryGetValue(typeof(T), out var receiver) == false)
            {
                throw new Exception($"There is no receiver for '{typeof(T)}'");
            }

            return (T)receiver;
        }

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            foreach (var receiver in _receivers.Values)
            {
                receiver.OnVisitSyntaxNode(syntaxNode);
            }
        }

        public void RegisterSyntaxReceiver<TService, TImplementation>()
            where TService : ISyntaxReceiver
            where TImplementation : TService, new()
        {
            _receivers.Add(typeof(TService), new TImplementation());
        }
    }
}
