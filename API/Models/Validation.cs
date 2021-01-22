using System;
using API.Enumerations;

namespace API.Models
{
    public class Validation
    {
         public int Id { get; set; }
         public string Tfn { get; set; }
         public ValidationResponse ValidationResult { get; set; }
         public DateTime CreatedOn { get; set; }

    }
}