using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class UniqueNameWorkersAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
                return null;
            string workerName = value.ToString()!;
            var context = validationContext.GetService(typeof(ElQasrDbContext)) as ElQasrDbContext;
            if (context == null)
            {
                throw new InvalidOperationException("ElQasrDbContext is not available for worker name validation.");
            }

            Workers? existingWorker = context.workers.FirstOrDefault(c => c.Name == workerName);
            if (existingWorker != null)
            {
                return new ValidationResult("this Name is already Exist !");
            }
            return ValidationResult.Success;

        }
    }
}

