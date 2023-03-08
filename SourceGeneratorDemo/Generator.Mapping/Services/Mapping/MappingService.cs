using SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis;
using SourceGeneratorDemo.Generator.Mapping.SyntaxReceivers.Map;

namespace SourceGeneratorDemo.Generator.Mapping.Services.Mapping
{
    internal class MappingService : IMappingService
    {
        private readonly ISemanticAnalysisService _semanticAnalysisService;
        private readonly IMapSyntaxReceiver _mapSyntaxReceiver;

        public MappingService(ISemanticAnalysisService semanticAnalysisService, IMapSyntaxReceiver mapSyntaxReceiver)
        {
            _semanticAnalysisService = semanticAnalysisService;
            _mapSyntaxReceiver = mapSyntaxReceiver;
        }

        public void GetMappings()
        {
            var genericNameSyntaxes = _mapSyntaxReceiver.GetGenericNameSyntax();

            foreach (var genericNameSyntax in genericNameSyntaxes)
            {
                if (_semanticAnalysisService.IsAutomapperInvocation(genericNameSyntax) == false)
                {
                    continue;
                }


            }
        }
    }
}
