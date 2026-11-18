using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project.Controllers;
using Project.Models;
using Project.Repository;

namespace QaserProjectTest
{
    [TestClass]
    public class SubCategRepoTest
    {
        ElQasrDbContext db = new ElQasrDbContext();
        SubCategoryRepo Repo;
        public SubCategRepoTest()
        {
            Repo = new SubCategoryRepo(db);
        }


        //[TestMethod]
        //public void TestMethod1()
        //{
        //    var db = new ElQasrDbContext();
        //    var controller = new CategoriesController(new CategoryRepo(db),new SubCategoryRepo(db),new ProductRepo(db),new PurchasingRepo(db));
        //    var result = controller.Details(1) as ViewResult;
        //    Assert.AreEqual("Details", result.ViewName);
        //}
        //[TestMethod]
        //public void TestMethod5()
        //{
        //    var db = new ElQasrDbContext();
        //    var controller = new CategoriesController(new CategoryRepo(db), new SubCategoryRepo(db), new ProductRepo(db), new PurchasingRepo(db));
        //    var result = controller.Test() as ViewResult;
        //    Assert.AreEqual("Test", result.ViewName);
        //}
        //[TestMethod]
        //public void TestMethod2()
        //{
        //    var db = new Project.Models.ElQasrDbContext();
        //    var controller = new CategoriesController(new CategoryRepo(db), new SubCategoryRepo(db), new ProductRepo(db), new PurchasingRepo(db));
        //    var result = controller.Delete(1) as ViewResult;
        //    Assert.AreEqual("Delete", result.ViewName);
        //}
        //[TestMethod]
        //public void TestMethod3()
        //{
        //    var db = new Project.Models.ElQasrDbContext();
        //    var controller = new CategoriesController(new CategoryRepo(db), new SubCategoryRepo(db), new ProductRepo(db), new PurchasingRepo(db));
        //    var result = controller.Create() as ViewResult;
        //    Assert.AreEqual("Create", result.ViewName);
        //}
        //[TestMethod]
        //public void test4()
        //{
        //    var db = new ElQasrDbContext();
        //    var Repo = new CategoryRepo(db);
        //    var ActualResult = Repo.GetAll();
        //    var ExpectedResult = db.Category.Where(x => x.Suc_Categ_Id == null).ToList();
        //    Assert.AreEqual(ExpectedResult, ActualResult);
        //}
        [TestMethod]
        public void EditUnreachedSubCategoryTest()
        {
            Assert.AreEqual(-1, Repo.EditSubCategory(100, new Category() { Name = "cat2" }));
        }
        [TestMethod]
        public void AddSubCategoryTestWithNull()
        {
            Assert.AreEqual(-1, Repo.AddSubCategory(null));
        }
        [TestMethod]
        public void DeleteUnreachedSubCategory()
        {
            Assert.AreEqual(-1, Repo.DeleteSubCategory(100));
        }
    }
    [TestClass]
    public class BillRepoTest
    {
        ElQasrDbContext db = new ElQasrDbContext();
        BillRepo Repo;
        public BillRepoTest()
        {
            Repo = new BillRepo(db);
        }
        [TestMethod]
        public void DeleteUnreachedBill()
        {
            Assert.AreEqual(-1, Repo.Delete((int.MaxValue - 1)));
        }
        [TestMethod]
        public void UpdateBillStateForUnreachedBill()
        {
            Assert.AreEqual(-1, Repo.UpdateBillState(int.MaxValue - 1));
        }
    }
}