using Microsoft.CodeAnalysis;
using SourceGeneratorDemo.Generator.Mapping.Services.Mapping;
using SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis;
using SourceGeneratorDemo.Generator.Mapping.SyntaxReceivers.Map;
using System.Diagnostics;

namespace SourceGeneratorDemo.Generator.Mapping
{
    [Generator]
    public class AutomapperGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var semanticAnalysisService = new SemanticAnalysisService(context);

            var syntaxReceiver = (IMapSyntaxReceiver)context.SyntaxReceiver;

            var mappingService = new MappingService(semanticAnalysisService, syntaxReceiver);

            mappingService.GetMappings();
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            Debugger.Launch();

            context.RegisterForSyntaxNotifications(() => new MapSyntaxReceiver());
        }
    }
}
