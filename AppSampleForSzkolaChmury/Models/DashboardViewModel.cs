using System.Collections.Generic;

namespace AppSampleForSzkolaChmury.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<Expense> LastExpenses { get; set; }
        public IEnumerable<(string category, decimal value)> Report { get; set; }
    }
}