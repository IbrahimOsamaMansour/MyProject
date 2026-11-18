using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class UniqueSSNWorkersAttribute : ValidationAttribute
    {
        
            protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            {

                if (value == null)
                    return null;
                string newCatName = value.ToString();
                ElQasrDbContext context = new ElQasrDbContext();
                Workers cat = context.workers.FirstOrDefault(c => c.SSN == newCatName);
                if (cat != null)
                {
                    return new ValidationResult("this SSN is already Exist !");
                }
                return ValidationResult.Success;

            }
        
    }
}
