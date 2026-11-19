using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class UniqueBillNumAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
                return null;
            string billNum = value.ToString()!;
            var context = validationContext.GetService(typeof(ElQasrDbContext)) as ElQasrDbContext;
            if (context == null)
            {
                throw new InvalidOperationException("ElQasrDbContext is not available for bill number validation.");
            }

            Bill? existingBill = context.Bill.FirstOrDefault(c => c.Bill_Num == billNum);
            if (existingBill != null)
            {
                return new ValidationResult("this Bill Number is already Exist !");
            }
            return ValidationResult.Success;

        }
    }
}
