using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Emp_Attendance
    {
        public int ID { get; set; }
        public Double? OverTime { get; set; }
        public DateTime Date { get; set; }
        public bool Attendance { get; set; }

        [ForeignKey("Workers")]
        public int workerID { get; set; }
        public virtual Workers Workers { get; set; }

       
    }
}
