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
            string BillNum = value.ToString();
            ElQasrDbContext context = new ElQasrDbContext();
            Bill catchBillnum = context.Bill.FirstOrDefault(c => c.Bill_Num == BillNum);
            if (catchBillnum != null)
            {
                return new ValidationResult("this Bill Number is already Exist !");
            }
            return ValidationResult.Success;

        }
    }
}