﻿using System;
using System.Collections.Generic;
using System.Linq;
using API.Enumerations;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TFNController : BaseApiController
    {
        private readonly IValidatorService _validator;

        public TFNController(IEnumerable<IValidatorService> validator)
        {
            _validator = validator.SingleOrDefault(p => p.Name == ValidationType.Default);
        }

        [HttpGet("{tfn}")]
        public ActionResult Get(string tfn)
        {
            return Ok(Enum.GetName(_validator.Validate(tfn)));
        }
    }
}
