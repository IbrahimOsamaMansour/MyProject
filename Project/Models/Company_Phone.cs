using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Company_Phone
    {
        [Key]

        public int id { get; set; }

        [ForeignKey("Company")]
        [HiddenInput]
        public int COMP_ID { get; set; }
        [RegularExpression(@"^(\+201|01|00201)[0-2,5]{1}[0-9]{8}", ErrorMessage = "invalid Phone")]
        [UniquePhone]
        public string? Phone { get; set; }

        public virtual Company? Company { get; set; }
    }
}
