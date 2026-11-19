using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class UniquePhoneAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
                return null;
            string newCatphone = value.ToString()!;
            var context = validationContext.GetService(typeof(ElQasrDbContext)) as ElQasrDbContext;
            if (context == null)
            {
                throw new InvalidOperationException("ElQasrDbContext is not available for phone validation.");
            }

            Company_Phone? companyPhone = context.Company_Phone.FirstOrDefault(c => c.Phone == newCatphone);
            if (companyPhone != null)
            {
                return new ValidationResult("this phone is already Exist !");
            }
            return ValidationResult.Success;

        }
    }
}
