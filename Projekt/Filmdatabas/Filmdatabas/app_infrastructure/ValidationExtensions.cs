using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Filmdatabas.Model;

namespace Filmdatabas
{
    public static class ValidationExtensions
    {
        public static bool Validate(this Title instance, out ICollection<ValidationResult> validationResults)
        {
            var validationContext = new ValidationContext(instance);
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(instance, validationContext, validationResults, true);
        }
    }
}