using System;
using System.Linq;
using API.Enumerations;
using API.Services;
using API.Helpers;
using API.Data;
using API.Models;

namespace API.Validations
{
    public class Validator : IValidatorService
    {
        private readonly DataContext _context;
        public Validator(DataContext context)
        {
            _context = context;
        }
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
            {
                return ValidationResponse.Valid;
            }
            else
            {
                // Only storing failed validations for Linked
                _context.Validations.Add(new Validation { Tfn = tfn, ValidationResult = ValidationResponse.Invalid, CreatedOn = DateTime.Now });
                _context.SaveChanges();

                if (IsLinked(tfn))
                    return ValidationResponse.Linked;

                return ValidationResponse.Invalid;
            }
        }
        public bool IsLinked(string tfnA)
        {
            var previousResults = _context
                .Validations
                .Where(p => p.CreatedOn >= DateTime.Now.AddMinutes(-30))
                .OrderByDescending(p => p.CreatedOn).Take(2);

            if (previousResults != null && previousResults.Count() == 2)
            {
                string tfnB = previousResults.ToList()[0].Tfn;
                string tfnC = previousResults.ToList()[1].Tfn;

                if (LongestCommonSubstring.Get(tfnA, tfnB).Length >= 4 && LongestCommonSubstring.Get(tfnB, tfnC).Length >= 4)
                    return true;
            }

            return false;
        }
    }
}
