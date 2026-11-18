using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class UniquePhoneAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if(value==null)
                return null;
            string newCatphone = value.ToString();
            ElQasrDbContext context = new ElQasrDbContext();
          Company_Phone cat= context.Company_Phone.FirstOrDefault(c=>c.Phone== newCatphone);
            if (cat!=null)
            {
                return new ValidationResult("this phone is already Exist !");
            }
            return ValidationResult.Success;

        }
    }
}
