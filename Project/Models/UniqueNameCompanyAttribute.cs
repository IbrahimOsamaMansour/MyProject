using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class UniqueNameCompanyAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if(value==null)
                return null;
            string newCatName =value.ToString();
            ElQasrDbContext context = new ElQasrDbContext();
          Company cat= context.Company.FirstOrDefault(c=>c.Name==newCatName);
            if (cat!=null)
            {
                return new ValidationResult("this Name is already Exist !");
            }
            return ValidationResult.Success;

        }
    }
}
