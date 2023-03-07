using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceGeneratorDemo.Generator.Mapping.Extensions;
using SourceGeneratorDemo.Generator.Mapping.Services.SemanticAnalysis;
using SourceGeneratorDemo.Generator.Mapping.SyntaxReceivers.CreateMap;
using SourceGeneratorDemo.Generator.Mapping.SyntaxReceivers.Map;
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

        public IEnumerable<NewMappingDescription> GetMapCalls()
        {
            var syntaxNodes = _mapSyntaxReceiver.GetSyntaxNode();

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

        public IEnumerable<ExistingMappingDescription> GetDefinedMaps()
        {
            var syntaxNodes = _createMapSyntaxReceiver.GetSyntaxNode();

            foreach (var syntaxNode in syntaxNodes)
            {
                // identifierNameSyntax should be node that represents the Mapper, so f.e. in this case 'CreateMap<TSource, TTraget>(o)'
                var createMapInvocationExpressionSyntax = syntaxNode
                    .FirstParentOfTypeAndSelf<InvocationExpressionSyntax>();

                if (_semanticAnalysisService.IsAutomapperInvocation(createMapInvocationExpressionSyntax) == false)
                {
                    continue; // False alarm, this is some call of 'CreateMap' method that not belongs to Automapper
                }

                var existingMappingMapDescription = _semanticAnalysisService.GetExistingMapping(syntaxNode);

                if (existingMappingMapDescription.Source == null || existingMappingMapDescription.Target == null)
                {
                    continue;
                }

                var createMapExpressionSyntax = syntaxNode
                    .FirstParentOfTypeAndSelf<ExpressionStatementSyntax>() // This is like a 'top statement' for 'CreateMap' invoke chain
                    .DescendantNodes()
                    .OfType<ArgumentListSyntax>() // This is all arguments, there will be a lot of them, I'll find the interesting later on
                    .Where(a => a.Arguments.Count == 2) // CAUTION: This is very naive, here I'll say, that if this has two arguments it is "ForMember" which could not be correct
                    .ToList();

                if (createMapExpressionSyntax.Count <= 0)
                {
                    continue;
                }

                var alreadyExistingMappings = createMapExpressionSyntax.Count > 0
                    ? GetMappingMembersFromArgumentList(createMapExpressionSyntax)
                    : Enumerable.Empty<ExistingPropertyMappingMember>();

                yield return new ExistingMappingDescription
                {
                    AlreadyExistingMappings = alreadyExistingMappings,
                    Source = existingMappingMapDescription.Source,
                    Target = existingMappingMapDescription.Target
                };
            }
        }

        private IEnumerable<ExistingPropertyMappingMember> GetMappingMembersFromArgumentList(List<ArgumentListSyntax> argumentListSyntax)
        {
            foreach (var argument in argumentListSyntax)
            {
                if (argument.Arguments == null || argument.Arguments.Count != 2)
                {
                    continue;
                }

                var to = GetPropertyName(argument.Arguments[0]);
                var from = GetPropertyName(argument.Arguments[1]);

                yield return new ExistingPropertyMappingMember
                {
                    FromPropertyName = from,
                    ToPropertyName = to,
                };
            }
        }

        private string GetPropertyName(ArgumentSyntax argument)
        {
            var last = argument
                .DescendantNodes()
                .OfType<IdentifierNameSyntax>()
                .LastOrDefault();

            if (last == null)
            {
                return null;
            }

            return last.Identifier.ValueText;
        }
    }
}
