namespace Project.ViewModel
{
    public class DashboardViewModel
    {
        public double SalesRevenue { get; set; }
        public double CostOfGoodsSold { get; set; }
        public double GrossProfit { get; set; }
        public double EstimatedNetProfit { get; set; }
        public double CustomerPaymentsCollected { get; set; }
        public double PurchaseValueRecorded { get; set; }
        public double ExpensesRecorded { get; set; }
        public double PayrollRecorded { get; set; }
        public double StockValueAtKnownCost { get; set; }
        public int SoldLinesWithoutCost { get; set; }
        public int StockLinesWithoutCost { get; set; }

        public List<UnitMovementSummary> UnitMovements { get; set; } = new();
        public List<RecentPaymentRow> RecentPayments { get; set; } = new();
        public List<RecentSaleRow> RecentSales { get; set; } = new();
        public List<RecentExpenseRow> RecentExpenses { get; set; } = new();
    }

    public class UnitMovementSummary
    {
        public string Unit { get; set; } = "";
        public int Purchased { get; set; }
        public int Sold { get; set; }
        public int InStock { get; set; }
    }

    public class RecentPaymentRow
    {
        public DateTime Date { get; set; }
        public string BillNumber { get; set; } = "";
        public string Customer { get; set; } = "";
        public double Amount { get; set; }
    }

    public class RecentSaleRow
    {
        public DateTime Date { get; set; }
        public string BillNumber { get; set; } = "";
        public string Customer { get; set; } = "";
        public string Product { get; set; } = "";
        public int Quantity { get; set; }
        public string Unit { get; set; } = "";
        public double Total { get; set; }
    }

    public class RecentExpenseRow
    {
        public DateTime Date { get; set; }
        public string Item { get; set; } = "";
        public double Amount { get; set; }
        public string? Note { get; set; }
    }
}
