using Project.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.ViewModel
{
    public class WorkerAttendeViewModel
    {

        public int? ID { get; set; }
        public Double? OverTime { get; set; }
        public DateTime Date { get; set; }
        public bool Attendance { get; set; }

        public int workerID { get; set; }

        public Workers? workers { get; set; }
        public List<Workers>? workerlist { get; set; }
        //public List<Emp_Attendance> emp_Attendances { get; set; }

    }
}
