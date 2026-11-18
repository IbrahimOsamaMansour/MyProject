using Project.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.ViewModel
{
    public class WorkerSalaryViewModel
    {
        public List<Workers>? workerlist { get; set; }



        public int? ID { get; set; }
        public Double? TotalSalary { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int AbsentDays { get; set; }
       
        public int WorkerID { get; set; }
    }
}
