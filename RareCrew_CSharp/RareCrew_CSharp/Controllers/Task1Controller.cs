using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RareCrew_CSharp.Models;
using RareCrew_CSharp.SystemOperations;
using System.Net.Http;

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
            return View(GetEmployees());
        }

        private List<Employee> GetEmployees() 
        {
            GetSO<Employee> getSO = new GetSO<Employee>(_httpClientFactory, _configuration);
            getSO.ExecuteTemplate(new Employee());
            List<Employee> employees = getSO.Result;

            foreach (var employee in employees)
            {
                employee.TotalTimeWorked = (employee.EndTimeUtc - employee.StarTimeUtc).TotalHours;
            }

            var distinctEmployees = employees
            .GroupBy(e => e.EmployeeName)
            .Select(group => new Employee
            {
                EmployeeName = group.Key,
                TotalTimeWorked = group.Sum(e => e.TotalTimeWorked)
            })
            .OrderByDescending(e => e.TotalTimeWorked)
            .ToList();

            return distinctEmployees;
        }
    }
}
