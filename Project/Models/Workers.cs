using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Workers
    {
        public int ID { get; set; }
        [UniqueNameWorkers]
        public string Name { get; set; }
        [UniqueSSNWorkers]
        public string SSN { get; set; }
        [RegularExpression(@"^(\+201|01|00201)[0-2,5]{1}[0-9]{8}", ErrorMessage = "invalid Phone")]

        public string? Phone { get; set; }
        public double? NetSalary { get; set; }

        public virtual List<Emp_Salary> EmpSalaries { get; set; }
        public virtual List<Emp_Attendance> EmpAttendances { get; set; }

        public Workers()
        {
            EmpAttendances = new List<Emp_Attendance>();
            EmpSalaries = new List<Emp_Salary>();
        }

        //[ForeignKey("RoleUser")]
        //public int? RoleID { get; set; }
        // public virtual Role_User RoleUser { get; set; }
    }
}
