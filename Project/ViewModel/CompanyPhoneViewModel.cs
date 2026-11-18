using Project.Models;
using System.ComponentModel.DataAnnotations;

namespace Project.ViewModel
{
    public class CompanyPhoneViewModel
    {
        List<Company> companies = new List<Company>();
        List<Company_Phone> company_Phones = new List<Company_Phone>();
        public string Name { get; set; }
        public string? OldPhone { get; set; }
        public string? Phone { get; set; }
        public string? PersonContact { get; set; }
        public int COMP_ID { get; set; }
        
    }
}
