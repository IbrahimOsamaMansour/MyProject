using Microsoft.EntityFrameworkCore;
//using Project.Migrations;
using Project.Models;

namespace Project.Repository
{
	public class ProductRepo : IProductRepo
	{
		private readonly ElQasrDbContext context;

		public ProductRepo(ElQasrDbContext context )
		{
			this.context = context;
		}

        public List<Product> getAllBuCategoryID(int id) => context.Product.Where(x=>x.Categ_Id == id).ToList();
        public List<Product> getAllBySubCategoryID(int id) => context.Product.Where(x=>x.subCateg_id == id).ToList();

        public List<Product> getAll() => context.Product.Include(x => x.category).Include(x => x.subcategory).Include(x => x.company).Include(x => x.Product_Quantities).ToList();

		public Product getById(int id) => context.Product.Include(x => x.company).Include(x => x.category).Include(x => x.subcategory).Include(x=>x.Product_Quantities).FirstOrDefault(x => x.Id == id);
        //============================== 11 / 24  ProductRepo
        public List<Product> getAllCompanyID(int id) => context.Product.Where(x => x.Comp_Id == id).ToList();
      

        public int Insert(Product product)
		{
			try
			{
				context.Product.Add(product);
				context.SaveChanges();
				return product.Id;
			}
			catch
			{
				return 0;
			}
		}

		public int update(Product editperchasing)
		{
            Product oldproduct = getById(editperchasing.Id);
            if (oldproduct == null) { return -1; }
            else
            {
                try
                {
                    oldproduct.Name = editperchasing.Name;
                    oldproduct.Number = editperchasing.Number;
                    oldproduct.ModelName = editperchasing.ModelName;
                    oldproduct.Description = editperchasing.Description;
                    oldproduct.subCateg_id = editperchasing.subCateg_id;
                    oldproduct.Categ_Id = editperchasing.Categ_Id;
                    oldproduct.Comp_Id = editperchasing.Comp_Id;
					
                    //oldpurchasing.Emp_id = editperchasing.Emp_id;
                   context.SaveChanges();
                    return 1;
                }
                catch { return 0; }
            }
        }

		public int delete(int id)
		{
			Product old = getById(id);
			if (old == null) { return -1; }
			try
			{
				context.Product.Remove(old);
				context.SaveChanges();
				return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int updateReservedbooking(int quantity, int productId)
        {
            Product oldproduct = getById(productId);
            if (oldproduct == null) { return -1; }
            else
            {
                try
                {
                    oldproduct.ReservedQuantity+= quantity;
                    context.SaveChanges();
                    return 1;
                }
                catch { return 0; }
            }
        }

        public int updateReservedSelling(int quantity, int productId)
        {
            Product oldproduct = getById(productId);
            if (oldproduct == null) { return -1; }
            else
            {
                try
                {
                    oldproduct.ReservedQuantity -= quantity;
                    context.SaveChanges();
                    return 1;
                }
                catch { return 0; }
            }
        }
    }
}
