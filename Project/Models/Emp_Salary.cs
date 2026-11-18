using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Emp_Salary
    {
        public int ID { get; set; }
        public Double? TotalSalary { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int AbsentDays { get; set; }
        [ForeignKey("Worker")]
        public int WorkerID { get; set; }
        public virtual Workers Worker { get; set; }
    }
}
