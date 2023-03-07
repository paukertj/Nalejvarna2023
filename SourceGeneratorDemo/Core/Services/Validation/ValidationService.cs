namespace SourceGeneratorDemo.Core.Services.Validation
{
    internal sealed class ValidationService : IValidationService
    {
        public bool CannotBeInFuture(DateOnly? day)
        {
            var now = DateTimeOffset.Now.Date;

            return
                day != null &&
                day <= new DateOnly(now.Year, now.Month, now.Day);
        }
    }
}
