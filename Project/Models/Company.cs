using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Company
    {
        [Key]
        public int ID { get; set; }
        [StringLength(50)]
        [Required ]
       // [UniqueNameCompany]
        public string Name { get; set; }
          
        [StringLength(50)]
        public string? PersonContact { get; set; }

        public virtual List<Product> products { get; set; } = new List<Product>();
        public virtual List<Company_Phone> company_Phones { get; set; } = new List<Company_Phone>();

        public virtual List<Purchasing> purchasings { get; set; } = new List<Purchasing>();




    }
}
