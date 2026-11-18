using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class UniqueBillNClientNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
                return null;
            string cBillName = value.ToString();
            ElQasrDbContext context = new ElQasrDbContext();
            Bill BillName = context.Bill.FirstOrDefault(c => c.Bill_Num == cBillName);
            if (BillName != null)
            {
                return new ValidationResult("this Name is already Exist !");
            }
            return ValidationResult.Success;

        }
    }
}
