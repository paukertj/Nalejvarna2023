using Microsoft.CodeAnalysis;

namespace SourceGeneratorDemo.Generator.Mapping.Services.SyntaxReceiving
{
    internal interface ISyntaxReceivingSevrvice : ISyntaxReceiver
    {
        void RegisterSyntaxReceiver<TService, TImplementation>()
            where TService : ISyntaxReceiver
            where TImplementation : TService, new();

        TService GetSyntaxReceiver<TService>()
            where TService : ISyntaxReceiver;
    }
}
