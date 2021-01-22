using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Enumerations;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class TFNController : BaseApiController
    {
        private readonly IValidatorService _validator;

        public TFNController(IValidatorService validator)
        {
            _validator = validator;
        }

        [HttpGet("{tfn}")]
        public ActionResult Get(string tfn)
        {
            return Ok(Enum.GetName(_validator.Validate(tfn)));
        }
    }
}
