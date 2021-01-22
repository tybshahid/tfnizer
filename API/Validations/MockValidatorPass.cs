using API.Enumerations;
using API.Services;

namespace API.Validations
{
    public class MockValidatorPass : IValidatorService
    {
        public ValidationType Name { get; }
        public MockValidatorPass()
        {
            this.Name = ValidationType.MockPass;
        }
        public ValidationResponse Validate(string tfn)
        {
            return ValidationResponse.Valid;
        }
    }
}
