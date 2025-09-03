using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Encodings.Web; 

namespace PusulaCase
{
    public class FilterEmployeesClass
    {
        public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary,
                                                           DateTime HireDate)> employees)
        {
            if (employees == null || !employees.Any())
            {
                return JsonSerializer.Serialize(new List<object>());
            }

            var filteredEmployees = employees
                .Where(e => e.Age >= 25 && e.Age <= 40 
                && e.Department == "IT" || e.Department == "Finance" 
                && e.Salary >= 5000 && e.Salary <= 9000 
                && e.HireDate > new DateTime(2017, 1, 1));

            var names = filteredEmployees.OrderByDescending(e => e.Name.Length).
                                          ThenBy(e => e.Name).
                                          Select(e => e.Name).
                                          ToList();

            decimal totalSalary = filteredEmployees.Sum(e => e.Salary);
            decimal averageSalary = filteredEmployees.Any() ? filteredEmployees.Average(e => e.Salary) : 0;
            decimal maxSalary = filteredEmployees.Any() ? filteredEmployees.Max(e => e.Salary) : 0;
            decimal minSalary = filteredEmployees.Any() ? filteredEmployees.Min(e => e.Salary) : 0;
            int count = filteredEmployees.Count();

            var result = new
            {
                Names = names,
                TotalSalary = totalSalary,
                AverageSalary = averageSalary % 1 == 0 ? ((int)averageSalary).ToString() : averageSalary.ToString("F2"),
                MinSalary = minSalary,
                MaxSalary = maxSalary,
                Count = count
            };

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping 
            };

            return JsonSerializer.Serialize(result, options);
        }
    }
}
