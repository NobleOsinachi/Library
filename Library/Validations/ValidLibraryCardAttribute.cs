using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Library.Data;

namespace Library.Validations
{
    public class ValidLibraryCardAttribute : ValidationAttribute
    {
        private readonly LibraryContext _context;
        public ValidLibraryCardAttribute()
        {
            _context = new LibraryContext();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Models.Patron x = _context.Patrons.Where(c => c.LibraryCard.Id == (int)value)
                            //.FirstOrDefault();
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