using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using API.Enumerations;
using API.Services;

namespace API.Validations
{
    public class Validator : IValidatorService
    {
        public ValidationType Name { get; }
        public Validator()
        {
            this.Name = ValidationType.Default;
        }
        public ValidationResponse Validate(string tfn)
        {
            tfn = string.Concat(tfn.Where(c => !char.IsWhiteSpace(c)));
            var length = tfn.Length;

            if ((length != 8 && length != 9) || !tfn.All(char.IsDigit))
                return ValidationResponse.Invalid;

            var digits = tfn.ToCharArray().Select(p => Convert.ToInt32(p.ToString())).ToArray();
            var sum = 0;

            // 9-digit Validation
            if (length == 9)
            {
                sum = (digits[0] * 10)
                    + (digits[1] * 7)
                    + (digits[2] * 8)
                    + (digits[3] * 4)
                    + (digits[4] * 6)
                    + (digits[5] * 3)
                    + (digits[6] * 5)
                    + (digits[7] * 2)
                    + (digits[8] * 1);
            }
            // 8-digit Validation
            else
            {
                sum = (digits[0] * 10)
                    + (digits[1] * 7)
                    + (digits[2] * 8)
                    + (digits[3] * 4)
                    + (digits[4] * 6)
                    + (digits[5] * 3)
                    + (digits[6] * 5)
                    + (digits[7] * 1);
            }

            if (sum % 11 == 0)
                return ValidationResponse.Valid;
            else
                return ValidationResponse.Invalid;
        }
    }
}
