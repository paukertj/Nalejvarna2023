namespace SourceGeneratorDemo.Core.Services.Validation
{
    internal sealed class ValidationService : IValidationService
    {
        public bool ValidateRange(DateOnly? from, DateOnly? to)
        {
            return 
                from != null && 
                to != null &&
                to > from;
        }
    }
}
