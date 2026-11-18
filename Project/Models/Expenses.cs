using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Project.Models
{
    public class Expenses
    {
        [HiddenInput]
        public int ID { get; set; }
        [Required(ErrorMessage = "Expense Value Is Required")]
        public Double? Value { get; set; }
        [Required (ErrorMessage = "Date is Required")]
        [Display(Name ="Pay Date")]
        public DateTime Expense_Date { get; set; }
        [Display(Name ="Expense Note")] 
        public string? note { get; set; }
        [ForeignKey("employee")]
        public int? EmpID { get; set; }
        public Employee? employee { get; set; }
        [ForeignKey("Itemexpense")]
        [Display(Name ="Item Expenses")]
        public int Item_Ex_ID { get; set; }
        [ValidateNever]
        public virtual Item_Expenses? Itemexpense { get; set; }

    }
}
