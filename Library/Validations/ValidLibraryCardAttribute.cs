using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Library.Services;

namespace Library.Validations
{
    public class ValidLibraryCardAttribute : ValidationAttribute
    {
        private PatronService _patron = new PatronService();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var x = _patron.GetAll()
                .FirstOrDefault(c => c.LibraryCard.Id == Convert.ToInt32(value));

            if (x == null)
            {
                return new ValidationResult($"Patron with the given Library Card Id {value} does not exist.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}