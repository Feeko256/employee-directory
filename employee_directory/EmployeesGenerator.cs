using employee_directory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace employee_directory
{
    public class EmployeesGenerator
    {
        const int employeesCount = 1000000;


        List<string> maleLastNames = new List<string> { "Ivanov", "Petrov", "Sidorov", "Kuznetsov", "Smirnov" };
        List<string> maleFirstNames = new List<string> { "Alexey", "Petr", "Nikolai", "Egor", "Oleg" };
        List<string> maleMiddleNames = new List<string> { "Ivanovich", "Sergeevich", "Andreevich" };

        List<string> femaleLastNames = new List<string> { "Ivanova", "Petrova", "Sidorova", "Kuznetsova", "Smirnova", "Fedotova", "Fedorova" };
        List<string> femaleFirstNames = new List<string> { "Maria", "Anna", "Kristina", "Nina", "Olga" };
        List<string> femaleMiddleNames = new List<string> { "Ivanovna", "Petrovna", "Andreevna" };


        List<string> lastNamesF = new List<string> { "Fedorov", "Frolov", "Filippov", "Fedotov", "Fadeev" };
        List<string> firstNamesF = new List<string> { "Mikhail", "Vladimir", "Sergey", "Andrey", "Dmitry" };
        List<string> middleNamesF = new List<string> { "Ivanovich", "Sergeevich", "Alexeevich", "Egorovich", "Andreevich" };

        List<string> genders = new List<string> { "Male", "Female" };
        public List<Employee> GenerateEmployees()
        {
            Random random = new Random();
            var employees = new List<Employee>();
            for (int i = 0; i < 100; i++)
            {
                employees.Add(
                    new Employee(
                    lastNamesF[random.Next(lastNamesF.Count)],
                    firstNamesF[random.Next(firstNamesF.Count)],
                    middleNamesF[random.Next(middleNamesF.Count)],
                    "Male",
                    GenerateDateOfBirth(random)
                    ));
            }
            for (int i = 0; i < employeesCount - 100; i++)
            {
                var gender = GenerateGender(random);
                if (gender == "Male")
                {
                    employees.Add(
                       new Employee(
                       maleLastNames[random.Next(maleLastNames.Count)],
                       maleFirstNames[random.Next(maleFirstNames.Count)],
                       maleMiddleNames[random.Next(maleMiddleNames.Count)],
                       gender,
                       GenerateDateOfBirth(random)
                   ));
                }
                else
                {
                    employees.Add(
                       new Employee(
                       femaleLastNames[random.Next(femaleLastNames.Count)],
                       femaleFirstNames[random.Next(femaleFirstNames.Count)],
                       femaleMiddleNames[random.Next(femaleMiddleNames.Count)],
                       gender,
                       GenerateDateOfBirth(random)
                   ));
                }

            }
            return employees.OrderBy(item => random.Next()).ToList();
        }
        private string GenerateGender(Random rand)
        {
            return genders[rand.Next(genders.Count)];
        }
        private DateOnly GenerateDateOfBirth(Random rand)
        {
            var start = new DateOnly(1924, 1, 1);
            var end = DateOnly.FromDateTime(DateTime.Today);
            var year = (end.ToDateTime(new TimeOnly(0, 0)) - start.ToDateTime(new TimeOnly(0, 0))).Days;
            var nStart = start.AddDays(rand.Next(year));
            return nStart;
        }
    }
}
