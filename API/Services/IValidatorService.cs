using System;
using API.Enumerations;

namespace API.Services
{
    public interface IValidatorService
    {
        ValidationResponse Validate(string tfn);
    }
}
