using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class UniqueitemExpNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
                return null;
            string itemName = value.ToString()!;
            var context = validationContext.GetService(typeof(ElQasrDbContext)) as ElQasrDbContext;
            if (context == null)
            {
                throw new InvalidOperationException("ElQasrDbContext is not available for expense item name validation.");
            }

            Item_Expenses? existingItem = context.item_Expenses.FirstOrDefault(i => i.Name == itemName);
            if (existingItem != null)
            {
                return new ValidationResult("this name is already Exist !");
            }
            return ValidationResult.Success;

        }
    }
}
