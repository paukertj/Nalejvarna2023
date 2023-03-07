using Microsoft.CodeAnalysis;
using System;

namespace SourceGeneratorDemo.Generator.Mapping.Extensions
{
    internal static class ExceptionExtensions
    {
        internal static DiagnosticDescriptor GetDiagnosticDescriptor(this Exception exception)
        {
            return new DiagnosticDescriptor(
                    "AM0001",
                    "Unhandled error occured during autoconverting",
                    exception.Message,
                    "Automaper.Generator",
                    DiagnosticSeverity.Error,
                    true);
        }
    }
}
