using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppSampleForSzkolaChmury.Models;

namespace AppSampleForSzkolaChmury.Services
{
    public interface IExpensesRepository
    {
        void Add(Expense expense);
        IEnumerable<Expense> GetLast(int limit = 10);
        IEnumerable<(string category, decimal amount)> GetReport();
    }
}
