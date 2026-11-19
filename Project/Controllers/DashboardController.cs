using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.ViewModel;

namespace Project.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ElQasrDbContext db;

        public DashboardController(ElQasrDbContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel();

            var stockLines = await db.product_Quantity.AsNoTracking().ToListAsync();
            var unitCosts = stockLines
                .Where(x => x.Prod_Id.HasValue && !string.IsNullOrWhiteSpace(x.UnitQuantity))
                .GroupBy(x => (ProductId: x.Prod_Id!.Value, Unit: x.UnitQuantity!))
                .ToDictionary(
                    group => group.Key,
                    group => new
                    {
                        TotalQuantity = group.Sum(x => x.ValueQuantity),
                        KnownCostQuantity = group.Where(x => x.UnitCost.HasValue).Sum(x => x.ValueQuantity),
                        KnownCostValue = group.Where(x => x.UnitCost.HasValue)
                            .Sum(x => x.UnitCost!.Value * x.ValueQuantity)
                    });

            var soldLines = await db.Product_Bill
                .AsNoTracking()
                .Where(x => x.Resv_Q_State)
                .Include(x => x.product)
                .Include(x => x.bill)
                .ToListAsync();

            foreach (var line in soldLines)
            {
                var quantity = line.Quantity ?? 0;
                model.SalesRevenue += (line.unit_Price ?? 0) * quantity;

                var costKey = (line.product_ID, line.unit_Quantity ?? string.Empty);
                if (unitCosts.TryGetValue(costKey, out var cost) &&
                    cost.TotalQuantity > 0 && cost.KnownCostQuantity == cost.TotalQuantity)
                    model.CostOfGoodsSold += (cost.KnownCostValue / cost.TotalQuantity) * quantity;
                else
                    model.SoldLinesWithoutCost++;
            }

            var payments = await db.payed_Quantity
                .AsNoTracking()
                .Include(x => x.bill)
                .OrderByDescending(x => x.Date_Payed)
                .ToListAsync();
            model.CustomerPaymentsCollected = payments.Sum(x => (double)x.value);
            model.RecentPayments = payments.Take(8).Select(x => new RecentPaymentRow
            {
                Date = x.Date_Payed,
                BillNumber = x.bill?.Bill_Num ?? "—",
                Customer = x.bill?.Client_Name ?? "—",
                Amount = x.value
            }).ToList();

            var purchases = await db.purchasing
                .AsNoTracking()
                .Include(x => x.Purchasing_Quantities)
                .ToListAsync();
            model.PurchaseValueRecorded = purchases.Sum(x => x.TotalPrice ?? 0);

            var expenses = await db.Expense
                .AsNoTracking()
                .Include(x => x.Itemexpense)
                .OrderByDescending(x => x.Expense_Date)
                .ToListAsync();
            model.ExpensesRecorded = expenses.Sum(x => x.Value ?? 0);
            model.RecentExpenses = expenses.Take(8).Select(x => new RecentExpenseRow
            {
                Date = x.Expense_Date,
                Item = x.Itemexpense?.Name ?? "—",
                Amount = x.Value ?? 0,
                Note = x.note
            }).ToList();

            model.PayrollRecorded = await db.Emp_Salarie
                .AsNoTracking()
                .SumAsync(x => (double?)(x.TotalSalary ?? 0)) ?? 0;

            model.GrossProfit = model.SalesRevenue - model.CostOfGoodsSold;
            model.EstimatedNetProfit = model.GrossProfit - model.ExpensesRecorded - model.PayrollRecorded;

            var purchasesByUnit = purchases
                .SelectMany(x => x.Purchasing_Quantities)
                .GroupBy(x => string.IsNullOrWhiteSpace(x.UnitQuantity) ? "غير محدد" : x.UnitQuantity!)
                .ToDictionary(x => x.Key, x => x.Sum(y => y.ValueQuantity));
            var salesByUnit = soldLines
                .GroupBy(x => string.IsNullOrWhiteSpace(x.unit_Quantity) ? "غير محدد" : x.unit_Quantity!)
                .ToDictionary(x => x.Key, x => x.Sum(y => y.Quantity ?? 0));
            var stockByUnit = stockLines
                .GroupBy(x => string.IsNullOrWhiteSpace(x.UnitQuantity) ? "غير محدد" : x.UnitQuantity!)
                .ToDictionary(x => x.Key, x => x.Sum(y => y.ValueQuantity));

            model.StockValueAtKnownCost = stockLines
                .Where(x => x.UnitCost.HasValue)
                .Sum(x => x.UnitCost!.Value * x.ValueQuantity);
            model.StockLinesWithoutCost = stockLines.Count(x => !x.UnitCost.HasValue);

            model.UnitMovements = purchasesByUnit.Keys
                .Union(salesByUnit.Keys)
                .Union(stockByUnit.Keys)
                .OrderBy(x => x)
                .Select(unit => new UnitMovementSummary
                {
                    Unit = unit,
                    Purchased = purchasesByUnit.GetValueOrDefault(unit),
                    Sold = salesByUnit.GetValueOrDefault(unit),
                    InStock = stockByUnit.GetValueOrDefault(unit)
                })
                .ToList();

            model.RecentSales = soldLines
                .OrderByDescending(x => x.Sell_Date ?? x.Book_Date)
                .Take(8)
                .Select(x => new RecentSaleRow
                {
                    Date = x.Sell_Date ?? x.Book_Date ?? DateTime.MinValue,
                    BillNumber = x.bill?.Bill_Num ?? "—",
                    Customer = x.bill?.Client_Name ?? "—",
                    Product = x.product?.ModelName ?? "—",
                    Quantity = x.Quantity ?? 0,
                    Unit = x.unit_Quantity ?? "—",
                    Total = (x.unit_Price ?? 0) * (x.Quantity ?? 0)
                })
                .ToList();

            return View(model);
        }
    }
}
