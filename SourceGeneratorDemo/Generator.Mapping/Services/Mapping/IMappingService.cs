using System.Collections.Generic;

namespace SourceGeneratorDemo.Generator.Mapping.Services.Mapping
{
    internal interface IMappingService
    {
        IEnumerable<MappingDescription> GetMappings();
    }
}
