namespace SourceGeneratorDemo.Core.Services.Validation
{
    internal interface IValidationService
    {
        bool CannotBeInFuture(DateOnly? day);
    }
}
