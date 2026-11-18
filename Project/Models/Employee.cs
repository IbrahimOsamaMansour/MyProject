using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Employee:IdentityUser<int>
    {
        //public int ID { get; set; }
        //public string? Username { get; set; }
        //public string? Pass { get; set; }
       // public string Name { get; set; }

        //public string? Phone { get; set; }
        public double? NetSalary { get; set; }

        //[ForeignKey("RoleUser")]
        //public int? RoleID { get; set; }
       // public virtual Role_User RoleUser { get; set; }

        //public virtual List<Emp_Salary> EmpSalaries { get; set; }
        //public virtual List<Emp_Attendance> EmpAttendances { get; set; }
        public virtual List<Purchasing> Purchasings { get; set; }
        public virtual List<Expenses> Expenses { get; set; }
        public virtual List<Bill> Bills { get; set; }
        public virtual List<Product> Products { get; set; }
        
        public Employee()
        {
            //EmpAttendances = new List<Emp_Attendance>();
            //EmpSalaries = new List<Emp_Salary>();
            Purchasings = new List<Purchasing>();
            Expenses = new List<Expenses>();
            Bills = new List<Bill>();
            Products = new List<Product>();
        }

    }
}
