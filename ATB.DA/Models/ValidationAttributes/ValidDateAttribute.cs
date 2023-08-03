using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.DA.Models.ValidationAttributes
{
    public class ValidDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (((DateTime)value)! > DateTime.Now)
                return ValidationResult.Success;

            return new ValidationResult(ErrorMessage);
        }
    }
}
