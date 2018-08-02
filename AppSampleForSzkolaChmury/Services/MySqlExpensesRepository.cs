using System;
using System.Collections.Generic;
using System.Linq;
using AppSampleForSzkolaChmury.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AppSampleForSzkolaChmury.Services
{
    public class MySqlExpensesRepository : IExpensesRepository
    {
        private readonly IConfiguration _configuration;

        public MySqlExpensesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private MySqlConnection ConnectionFactory()
        {
            return new MySqlConnection(_configuration.GetConnectionString("ExpensesDatabase"));
        }

        public void Add(Expense expense)
        {
            using (var connection = ConnectionFactory())
            {
                connection.Execute("INSERT INTO expenses (date, amount, category) VALUES (@date, @amount, @category)",
                    new
                    {
                        date = expense.Date,
                        amount = expense.Amount,
                        category = expense.Category
                    });
            }
        }

        public IEnumerable<Expense> GetLast(int limit = 10)
        {
            using (var connection = ConnectionFactory())
            {
                return connection.Query<Expense>("SELECT id, date, amount, category FROM expenses ORDER BY date DESC LIMIT @limit",
                    new { limit = limit });
            }
        }

        public IEnumerable<(string category, decimal amount)> GetReport()
        {
            using (var connection = ConnectionFactory())
            {
                return connection.Query<(string category, decimal amount)>("SELECT category, SUM(amount) FROM expenses GROUP BY category");
            }
        }
    }
}