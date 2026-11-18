using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Item_Expenses
    {
        public int ID { get; set; }
        [MaxLength(100)]
        [MinLength(2)]
        [UniqueitemExpName]
        public string Name { get; set; }
        public virtual List<Expenses> Expenses { get; set; }
        public Item_Expenses()
        {
            Expenses = new List<Expenses>();
        }
    }
}
