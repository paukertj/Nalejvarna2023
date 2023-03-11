using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceGeneratorDemo.Generator.Mapping.Extensions;
using SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis;
using SourceGeneratorDemo.Generator.Mapping.SyntaxReceivers.CreateMap;
using SourceGeneratorDemo.Generator.Mapping.SyntaxReceivers.Map;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SourceGeneratorDemo.Generator.Mapping.Services.Mapping
{
    internal sealed class MappingService : IMappingService
    {
        private readonly ISemanticAnalysisService _semanticAnalysisService;
        private readonly IMapSyntaxReceiver _mapSyntaxReceiver;
        private readonly ICreateMapSyntaxReceiver _createMapSyntaxReceiver;

        public MappingService(
            ISemanticAnalysisService semanticAnalysisService,
            IMapSyntaxReceiver mapSyntaxReceiver,
            ICreateMapSyntaxReceiver createMapSyntaxReceiver)
        {
            _semanticAnalysisService = semanticAnalysisService;
            _mapSyntaxReceiver = mapSyntaxReceiver;
            _createMapSyntaxReceiver = createMapSyntaxReceiver;
        }

        public IEnumerable<NewMappingDescription> GetMappingsToGenerate()
        {
            var syntaxNodes = _mapSyntaxReceiver.GetSyntaxNode();
            var existingMappings = GetExitingMappings();

            foreach (var syntaxNode in syntaxNodes)
            {
                var memberAccessExpression = syntaxNode.FirstParentOfTypeAndSelf<MemberAccessExpressionSyntax>();

                // identifierNameSyntax should be node that represents the Mapper, so f.e. in this case '_mapper.Map<object>(o)' this should '_mapper'
                var identifierNameSyntax = memberAccessExpression?
                    .DescendantNodes()?
                    .OfType<IdentifierNameSyntax>()?
                    .FirstOrDefault();

                if (_semanticAnalysisService.IsAutomapperInvocation(identifierNameSyntax) == false)
                {
                    continue; // False alarm, this is some call of 'Map' method that not belongs to Automapper
                }

                var target = _semanticAnalysisService.GetNewMappingTarget(memberAccessExpression);

                if (target == null)
                {
                    continue; // If there is no target, there is nothing to map
                }

                var invocationExpression = syntaxNode.FirstParentOfTypeAndSelf<InvocationExpressionSyntax>();

                var source = _semanticAnalysisService.GetNewMappingSource(invocationExpression);

                if (source == null)
                {
                    continue; // If there is no source, there is nothing to map
                }

                yield return new NewMappingDescription
                {
                    Target = target,
                    Source = source
                };
            }
        }

        private IEnumerable<ExistingMappingDescription> GetExitingMappings()
        {
            var syntaxNodes = _createMapSyntaxReceiver.GetSyntaxNode();

            foreach (var syntaxNode in syntaxNodes)
            {
                // identifierNameSyntax should be node that represents the Mapper, so f.e. in this case 'CreateMap<TSource, TTraget>(o)'
                var invocationExpressionSyntax = syntaxNode
                    .FirstParentOfTypeAndSelf<InvocationExpressionSyntax>();

                if (_semanticAnalysisService.IsAutomapperInvocation(invocationExpressionSyntax) == false)
                {
                    continue; // False alarm, this is some call of 'CreateMap' method that not belongs to Automapper
                }

                var existingMappingMapDescription = _semanticAnalysisService.GetExistingMapping(syntaxNode);

                if (existingMappingMapDescription.Source == null || existingMappingMapDescription.Target == null)
                {
                    continue;
                }

                // TODO: Analyze ForMember calls

                yield return new ExistingMappingDescription() ;
            }
        }
    }
}
