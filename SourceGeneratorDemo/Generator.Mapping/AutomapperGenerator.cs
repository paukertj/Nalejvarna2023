using Microsoft.CodeAnalysis;
using SourceGeneratorDemo.Generator.Mapping.Exceptions;
using SourceGeneratorDemo.Generator.Mapping.Extensions;
using SourceGeneratorDemo.Generator.Mapping.Services.CodeGenerating;
using SourceGeneratorDemo.Generator.Mapping.Services.Mapping;
using SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis;
using SourceGeneratorDemo.Generator.Mapping.Services.SyntaxReceiving;
using SourceGeneratorDemo.Generator.Mapping.SyntaxReceivers.CreateMap;
using SourceGeneratorDemo.Generator.Mapping.SyntaxReceivers.Map;
using System.Diagnostics;

namespace SourceGeneratorDemo.Generator.Mapping
{
    [Generator]
    public class AutomapperGenerator : ISourceGenerator
    {
        private readonly ISyntaxReceivingSevrvice _syntaxReceivingSevrvice;

        public AutomapperGenerator()
        {
            _syntaxReceivingSevrvice = new SyntaxReceivingSevrvice();

            _syntaxReceivingSevrvice.RegisterSyntaxReceiver<IMapSyntaxReceiver, MapSyntaxReceiver>();
            _syntaxReceivingSevrvice.RegisterSyntaxReceiver<ICreateMapSyntaxReceiver, CreateMapSyntaxReceiver>();
        }

        /// <summary>
        /// Called to perform source generation.
        /// </summary>
        /// <param name="context"></param>
        public void Execute(GeneratorExecutionContext context)
        {
            try
            {
                var semanticAnalysisService = new SemanticAnalysisService(context);

                var mapSyntaxReceiver = _syntaxReceivingSevrvice.GetSyntaxReceiver<IMapSyntaxReceiver>();
                var createMapSyntaxReceiver = _syntaxReceivingSevrvice.GetSyntaxReceiver<ICreateMapSyntaxReceiver>();

                var mappingService = new MappingService(semanticAnalysisService, mapSyntaxReceiver, createMapSyntaxReceiver);

                var mapCalls = mappingService.GetMapCalls();

                var codeGeneratingService = new CodeGeneratingService(mappingService);

                string code = codeGeneratingService.GenerateCode(mapCalls);

                context.AddSource("Mapping.g.cs", code);
            }
            catch (InvalidMappingException e)
            {
                context.ReportDiagnostic(e);
            }
        }

        /// <summary>
        /// Called before generation occurs.
        /// </summary>
        /// <param name="context"></param>
        public void Initialize(GeneratorInitializationContext context)
        {
            //Debugger.Launch();

            context.RegisterForSyntaxNotifications(() => _syntaxReceivingSevrvice);
        }
    }
}
