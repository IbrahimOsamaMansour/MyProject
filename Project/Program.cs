using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Project.Models;
using Project.Repository;
using Project.Services;
using Purchasing = Project.Models.Purchasing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<ElQasrDbContext>(option =>
//                {
//                    option.UseSqlServer(builder.Configuration.GetConnectionString("myDB"));

//                });


//to register service of injection DbContext in classer
builder.Services.AddDbContext<ElQasrDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("myDB")
    )); 

// register for servic for identity of Microsoft
builder.Services.AddIdentity<Employee, IdentityRole<int>>(
                                                        Options=>Options.Password.RequireDigit = true
                                                        ).AddEntityFrameworkStores<ElQasrDbContext>();

//to add custome service and  register it in service to allow injection IPurchasing in controller
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyPhoneRepository, CompanyPhonoRepository>();
builder.Services.AddScoped<IPurchasingRepo, PurchasingRepo>();
//builder.Services.AddScoped<ICompanyRepo, CompanyRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<ISubCategoryRepo, SubCategoryRepo>();
builder.Services.AddScoped<IPurchasingQuantity, PurchasingQuantityRepo>(); 
builder.Services.AddScoped<IProductPurchasing,ProductPurchasingRepo>(); 
builder.Services.AddScoped<IProductRepo, ProductRepo>(); 
builder.Services.AddScoped<IProduct_Quantity_Repo, Product_Quantity_Repo>();
builder.Services.AddScoped<IBillRepo, BillRepo>();
builder.Services.AddScoped<IProductBillRepo, ProductBillRepo>();
builder.Services.AddScoped<IPayedquantityRepo, PayedquantityRepo>();
builder.Services.AddScoped<IitemexpensessRepo, itemExpensessRepo>();
builder.Services.AddScoped<IExpensesRepo, ExpensesRepo>();
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
builder.Services.AddScoped<IWorkersRepo, WorkersRepo>();
builder.Services.AddScoped<IEmpAttendanceRepo, EmpAttendanceRepo>();
builder.Services.AddScoped<IEmpSalaryRepo, EmpSalaryRepo>();

var app = builder.Build();

// Configure t he HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();// to make filter authorize check cookie
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Bills}/{action=Index}/{id?}");

app.Run();
