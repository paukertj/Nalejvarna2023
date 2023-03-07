using Microsoft.CodeAnalysis;
using System;

namespace SourceGeneratorDemo.Generator.Mapping.Extensions
{
    internal static class GeneratorExecutionContextExtensions
    {
        internal static void ReportDiagnostic(this GeneratorExecutionContext context, Exception exception)
        {
            exception
                .GetDiagnosticDescriptor()
                .ReportDiagnostic(context);
        }

        private static void ReportDiagnostic(this DiagnosticDescriptor diagnosticDescriptor, GeneratorExecutionContext context)
        {
            var diagnostic = Diagnostic.Create(diagnosticDescriptor, null);

            context.ReportDiagnostic(diagnostic);
        }
    }
}
