using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class UniqueNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if(value==null)
                return null;
            string newCatName =value.ToString();
            ElQasrDbContext context = new();
          Category cat= context.Category.FirstOrDefault(c=>c.Name==newCatName);
            if (cat!=null)
            {
                return new ValidationResult("this name is already Exist !");
            }
            return ValidationResult.Success;

        }
    }
}
