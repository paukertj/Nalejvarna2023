using SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis;
using SourceGeneratorDemo.Generator.Mapping.SyntaxReceivers.Map;
using System.Collections.Generic;

namespace SourceGeneratorDemo.Generator.Mapping.Services.Mapping
{
    internal sealed class MappingService : IMappingService
    {
        private readonly ISemanticAnalysisService _semanticAnalysisService;
        private readonly IMapSyntaxReceiver _mapSyntaxReceiver;

        public MappingService(ISemanticAnalysisService semanticAnalysisService, IMapSyntaxReceiver mapSyntaxReceiver)
        {
            _semanticAnalysisService = semanticAnalysisService;
            _mapSyntaxReceiver = mapSyntaxReceiver;
        }

        public IEnumerable<MappingDescription> GetMappings()
        {
            var genericNameSyntaxes = _mapSyntaxReceiver.GetGenericNameSyntax();

            foreach (var genericNameSyntax in genericNameSyntaxes)
            {
                if (_semanticAnalysisService.IsAutomapperInvocation(genericNameSyntax) == false)
                {
                    continue;
                }

                var target = _semanticAnalysisService.GetMappingTarget(genericNameSyntax);

                if (target == null)
                {
                    continue; // If there is no target, there is nothing to map
                }

                var source = _semanticAnalysisService.GetMappingSource(genericNameSyntax);

                if (source == null)
                {
                    continue; // If there is no source, there is nothing to map
                }

                yield return new MappingDescription
                {
                    Target = target,
                    Source = source
                };
            }
        }
    }
}
