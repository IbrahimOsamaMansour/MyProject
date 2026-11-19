using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class UniqueNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
                return null;
            string newCatName = value.ToString()!;
            var context = validationContext.GetService(typeof(ElQasrDbContext)) as ElQasrDbContext;
            if (context == null)
            {
                throw new InvalidOperationException("ElQasrDbContext is not available for category name validation.");
            }

            Category? category = context.Category.FirstOrDefault(c => c.Name == newCatName);
            if (category != null)
            {
                return new ValidationResult("this name is already Exist !");
            }
            return ValidationResult.Success;

        }
    }
}
