using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class UniqueNameWorkersAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
                return null;
            string newCatName = value.ToString();
            ElQasrDbContext context = new ElQasrDbContext();
            Workers cat = context.workers.FirstOrDefault(c => c.Name == newCatName);
            if (cat != null)
            {
                return new ValidationResult("this Name is already Exist !");
            }
            return ValidationResult.Success;

        }
    }
}

