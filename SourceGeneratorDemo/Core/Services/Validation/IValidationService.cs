namespace SourceGeneratorDemo.Core.Services.Validation
{
    internal interface IValidationService
    {
        bool ValidateRange(DateOnly? from, DateOnly? to);
    }
}
