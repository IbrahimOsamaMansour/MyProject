using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class UniqueSSNWorkersAttribute : ValidationAttribute
    {
        
            protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            {

                if (value == null)
                    return null;
                string ssn = value.ToString()!;
                var context = validationContext.GetService(typeof(ElQasrDbContext)) as ElQasrDbContext;
                if (context == null)
                {
                    throw new InvalidOperationException("ElQasrDbContext is not available for worker SSN validation.");
                }

                Workers? existingWorker = context.workers.FirstOrDefault(c => c.SSN == ssn);
                if (existingWorker != null)
                {
                    return new ValidationResult("this SSN is already Exist !");
                }
                return ValidationResult.Success;

            }
        
    }
}
