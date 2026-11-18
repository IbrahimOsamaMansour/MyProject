using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class UniqueitemExpNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if(value==null)
                return null;
            string newCatName =value.ToString();
            ElQasrDbContext context = new();
          Item_Expenses itemExp= context.item_Expenses.FirstOrDefault(i=>i.Name==newCatName);
            if (itemExp != null)
            {
                return new ValidationResult("this name is already Exist !");
            }
            return ValidationResult.Success;

        }
    }
}
