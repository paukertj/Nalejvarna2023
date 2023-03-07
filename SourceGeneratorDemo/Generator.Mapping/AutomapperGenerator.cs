using Microsoft.CodeAnalysis;
using System;
using System.Diagnostics;

namespace SourceGeneratorDemo.Generator.Mapping
{
    [Generator]
    public class AutomapperGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            //throw new NotImplementedException();
        }

        public void Initialize(GeneratorInitializationContext context)
        {
           // Debugger.Launch();
        }
    }
}
