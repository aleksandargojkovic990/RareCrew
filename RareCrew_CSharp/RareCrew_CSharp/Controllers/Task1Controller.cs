using Microsoft.AspNetCore.Mvc;
using RareCrew_CSharp.Models;
using RareCrew_CSharp.SystemOperations;

namespace RareCrew_CSharp.Controllers
{
    public class Task1Controller : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private IConfiguration _configuration;

        public Task1Controller(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public IActionResult Task1()
        {
            return View(SortEmployees());
        }

        private List<Employee> SortEmployees()
        {
            List<Employee> employees = GetEmployees();

            double totalTimeWorked = 0;
            foreach (var employee in employees)
            {
                employee.TotalTimeWorked = (employee.EndTimeUtc - employee.StarTimeUtc).TotalHours;
                totalTimeWorked += employee.TotalTimeWorked;
            }

            double ttw = 0;
            var distinctEmployees = employees.GroupBy(e => e.EmployeeName)
                                                .Select(group => new Employee
                                                {
                                                    EmployeeName = group.Key,
                                                    TotalTimeWorked = ttw = group.Sum(e => e.TotalTimeWorked),
                                                    TotalTimeWorkedPercentage = (ttw / totalTimeWorked) * 100
                                                })
                                                .OrderByDescending(e => e.TotalTimeWorked)
                                                .ToList();

            return distinctEmployees;
        }

        private List<Employee> GetEmployees()
        {
            GetSO<Employee> getSO = new GetSO<Employee>(_httpClientFactory, _configuration);
            getSO.ExecuteTemplate(new Employee());
            return getSO.Result;
        }
    }
}
