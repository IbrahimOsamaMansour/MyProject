using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Project.Models
{
    public class ElQasrDbContext : IdentityDbContext<Employee , IdentityRole<int>, int>
    {
        public ElQasrDbContext()
        {
        }

        public ElQasrDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Emp_Salary> Emp_Salarie { get; set; }
        public DbSet<Emp_Attendance> Emp_Attendance { get; set; }
        //public DbSet<Role_User> Role_User { get; set; }
        public DbSet<Expenses> Expense { get; set; }
        public DbSet<Item_Expenses> item_Expenses { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Company_Phone> Company_Phone { get; set; }
        public DbSet<Payed_quantity> payed_Quantity { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Product_Bill> Product_Bill { get; set; }
        public DbSet<Product_Purchasing> product_Purchasing { get; set; }
        public DbSet<Product_Quantity> product_Quantity { get; set; }
        public DbSet<Purchasing> purchasing { get; set; }
        public DbSet<Purchasing_Quantity> Purchasing_Quantity { get; set; }

        //==========================   11/26
        public DbSet<Workers> workers { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Company_Phone>().HasKey(e => new { e.Phone, e.COMP_ID });
        //    modelBuilder.Entity<Product_Bill>().HasKey(e => new { e.Bill_ID, e.product_ID });
        //    modelBuilder.Entity<Product_Purchasing>().HasKey(e => new { e.Purchasing_Id, e.product_Id });
        //    base.OnModelCreating(modelBuilder);
        //}   32 *200 ,,, 14*100


      
    }
}
