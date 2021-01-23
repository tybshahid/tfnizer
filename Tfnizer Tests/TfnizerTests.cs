using API.Enumerations;
using API.Validations;
using Xunit;

namespace Tests
{
    public class TfnizerTests
    {
        [Fact]
        public void TestValidTfnsUsingDefaultAlgorithm()
        {
            Assert.Equal(ValidationResponse.Valid, new Validator().Validate("648 188 480"));
            Assert.Equal(ValidationResponse.Valid, new Validator().Validate("648 188 499"));
            Assert.Equal(ValidationResponse.Valid, new Validator().Validate("648 188 519"));
            Assert.Equal(ValidationResponse.Valid, new Validator().Validate("37 118 629"));
            Assert.Equal(ValidationResponse.Valid, new Validator().Validate("37 118 660"));
            Assert.Equal(ValidationResponse.Valid, new Validator().Validate("37 118 705"));
        }

        [Fact]
        public void TestInvalidTfnsUsingDefaultAlgorithm()
        {
            Assert.Equal(ValidationResponse.Invalid, new Validator().Validate("123 456 789"));
        }

        [Fact]
        public void TestTfnsUsingMockAlgorithms()
        {
            Assert.Equal(ValidationResponse.Valid, new MockValidatorPass().Validate("123 456 789"));
            Assert.Equal(ValidationResponse.Invalid, new MockValidatorFail().Validate("123 456 789"));
        }
    }
}
