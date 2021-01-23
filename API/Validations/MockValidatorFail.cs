using API.Enumerations;
using API.Services;

namespace API.Validations
{
    public class MockValidatorFail : IValidatorService
    {
        public ValidationType Name { get; }
        public MockValidatorFail()
        {
            this.Name = ValidationType.MockFail;
        }
        public ValidationResponse Validate(string tfn)
        {
            return ValidationResponse.Invalid;
        }

        public bool IsLinked(string tfn)
        {
            return true;
        }
    }
}
