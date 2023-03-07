using SourceGeneratorDemo.Generator.Mapping.Services.Mapping;
using System.Collections.Generic;

namespace SourceGeneratorDemo.Generator.Mapping.Services.CodeGenerating
{
    internal interface ICodeGeneratingService
    {
        string GenerateCode(IEnumerable<NewMappingDescription> mappingDescriptions);
    }
}
