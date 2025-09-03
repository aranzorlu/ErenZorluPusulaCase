using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PusulaCase
{
    public class FilterPeople
    {
        public static string FilterPeopleFromXml(string xmlData)
        {
            if (string.IsNullOrWhiteSpace(xmlData))
            {
                     return JsonSerializer.Serialize(new List<object>());

            }

            var doc = XDocument.Parse(xmlData);

            var people = doc.Descendants("Person")
                            .Select(p => new
                            {
                                name = (string)p.Element("Name"),
                                age = (int)p.Element("Age"),
                                deparment = (string)p.Element("Department"),
                                salary = (decimal)p.Element("Salary"),
                                hireDate = DateTime.Parse((string)p.Element("HireDate"))

                            })
                            .Where(p => p.age > 30 && p.deparment == "IT" && p.salary > 5000 && p.hireDate < new DateTime(2019, 1, 1))
                            .OrderBy(p => p.name)
                            .ToList();


            var names = people.Select(p => p.name).ToList();

            decimal totalSalary = people.Sum(p => p.salary);

            decimal averageSalary = people.Count > 0 ? totalSalary / people.Count : 0;

            decimal maxSalary = people.Count > 0 ? people.Max(p => p.salary): 0;

            int count = people.Count;

            var result = new
            {
                Names = names,
                TotalSalary = totalSalary,
                AverageSalary = averageSalary,
                MaxSalary = maxSalary,
                Count = count
               
            };

            return JsonSerializer.Serialize(result);
        }
    }
}
