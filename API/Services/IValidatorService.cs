using API.Enumerations;

namespace API.Services
{
    public interface IValidatorService
    {
        ValidationType Name { get; }
        ValidationResponse Validate(string tfn);
        bool IsLinked(string tfn);
    }
}
