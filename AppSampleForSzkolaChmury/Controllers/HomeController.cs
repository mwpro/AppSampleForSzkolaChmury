using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppSampleForSzkolaChmury.Models;
using AppSampleForSzkolaChmury.Services;

namespace AppSampleForSzkolaChmury.Controllers
{
    public class HomeController : Controller
    {
        private readonly IExpensesRepository _expensesRepository;

        public HomeController(IExpensesRepository expensesRepository)
        {
            _expensesRepository = expensesRepository;
        }

        public IActionResult Index()
        {
            return View(new DashboardViewModel()
            {
                LastExpenses = _expensesRepository.GetLast(),
                Report = _expensesRepository.GetReport()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRandom()
        {
            var categories = new[]
            {
                "food", "fuel", "clothes", "bills"
            };

            _expensesRepository.Add(new Expense()
            {
                Amount = DateTime.Now.Ticks % 100,
                Category = categories[new Random().Next(0, categories.Length)],
                Date = DateTime.UtcNow
            });

            return RedirectToAction(nameof(Index));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
