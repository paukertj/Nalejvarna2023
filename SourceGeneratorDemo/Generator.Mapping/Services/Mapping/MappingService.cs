using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceGeneratorDemo.Generator.Mapping.Extensions;
using SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis;
using SourceGeneratorDemo.Generator.Mapping.SyntaxReceivers.CreateMap;
using SourceGeneratorDemo.Generator.Mapping.SyntaxReceivers.Map;
using System;
using System.Collections.Generic;

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

                if (_semanticAnalysisService.IsAutomapperInvocation(memberAccessExpression) == false)
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
                var memberAccessExpression = syntaxNode.FirstParentOfTypeAndSelf<InvocationExpressionSyntax>();

                if (_semanticAnalysisService.IsAutomapperInvocation(memberAccessExpression) == false)
                {
                    continue; // False alarm, this is some call of 'CreateMap' method that not belongs to Automapper
                }

                var target = _semanticAnalysisService.GetExistingMappingTarget(syntaxNode);

                if (target == null)
                {
                    continue; // If there is no target, there is nothing to map
                }

                var source = _semanticAnalysisService.GetExistingMappingSource(syntaxNode);

                if (source == null)
                {
                    continue; // If there is no source, there is nothing to map
                }

            }

            return null;
        }
    }
}
