using employee_directory.DB;
using employee_directory.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace employee_directory
{
    public class Program : DbOperations
    {
        static void Main(string[] args)
        {

            //examples
            //p 2 "Smirnov Alexey Ivanovich" 1985-03-21 Male
            //p 2 "Kuznetsova Maria Ivanovna" 1992-08-15 Female
            //p 2 "Petrov Nikolai Sergeevich" 1978-11-30 Male
            //p 2 "Sokolova Anna Petrovna" 2000-01-05 Female
            //p 2 "Volkov Dmitry Andreevich" 1995-07-19 Male
            //  while (true)
            //  {
            /*Console.WriteLine(new string('-', 82));
              Console.WriteLine("Write \"p 1\" to initialize the database");
              Console.WriteLine("Write \"p 2 \"Ivanov Petr Sergeevich\" 2009-07-12 Male\" to add data to the database");
              Console.WriteLine("Write \"p 3\" to view employees in the database");
              Console.WriteLine("Write \"p 4\" to generate empoyees");
              Console.WriteLine("Write \"p 5\" to view employees in the database with last name starts with \"F\"");
              Console.WriteLine("Write \"p 7\" to exit");
              Console.WriteLine(new string('-', 82));*/

            try
            {
                Console.Clear();
                //  var input = Console.ReadLine();

                var input = "";
                if (args.Length == 0)
                {
                    Console.WriteLine("Hello, world!");
                }

                foreach (var a in args)
                {
                    input += a;
                }

                string[] parts = Regex.Split(input, "\"([^\"]*)\"|\\s+");
                parts = Array.FindAll(parts, s => !string.IsNullOrEmpty(s));
                string myApp = parts[0];
                string number = parts[1];
                string[] fullName = parts.Length > 2 ? parts[2].Split(' ') : Array.Empty<string>();
                string lastName = fullName.Length > 0 ? fullName[0] : string.Empty;
                string firstName = fullName.Length > 0 ? fullName[1] : string.Empty;
                string middleName = fullName.Length > 0 ? fullName[2] : string.Empty;
                DateOnly dateOfBirth = parts.Length > 2 ? DateOnly.Parse(parts[3]) : default;
                string gender = parts.Length > 2 ? parts[4] : string.Empty;

                switch (myApp + " " + number)
                {
                    case "myApp 1":
                        {
                            Initialize();
                            break;
                        }
                    case "myApp 2":
                        {
                            var emp = new Employee(
                                lastName,
                                firstName,
                                middleName,
                                gender,
                                dateOfBirth);
                            emp.SendDataToDB();
                            break;
                        }
                    case "myApp 3":
                        {
                            Console.Clear();
                            Return(ReturnEmployees());
                            break;
                        }
                    case "myApp 4":
                        {
                            try
                            {
                                Console.Clear();
                                Console.WriteLine("Employee generation and addition to the database are in progress. Please wait...");
                                Stopwatch stopwatch = new Stopwatch();
                                stopwatch.Start();
                                EmployeesGenerator empgen = new EmployeesGenerator();
                                stopwatch.Stop();

                                var generationTime = stopwatch.Elapsed;
                                var count = empgen.GenerateEmployees().Count(i => i.Gender == "Male");

                                stopwatch.Start();
                                Insert(empgen.GenerateEmployees());
                                stopwatch.Stop();
                                var insertingTime = stopwatch.Elapsed;
                                Console.Clear();
                                Console.WriteLine("The process has been successfully completed.");
                                Console.WriteLine("Males count:" + count);
                                Console.WriteLine("Females count:" + (empgen.GenerateEmployees().Count - count));
                                Console.WriteLine("Generation time " + generationTime);
                                Console.WriteLine("Inserting time " + insertingTime);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"{ex.Message}");
                            }


                            break;
                        }
                    case "myApp 5":
                        {
                            Console.Clear();
                            Return(ReturnEmployeesLastNameStartsWithF());
                            break;
                        }
                    /*  case "p 7":
                          {
                              return;
                          }*/
                    default:
                        {
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            //  }
        }
        private static void Return(List<Employee> employeesList)
        {
            Console.Clear();
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("{0, -7} | {1,-12} | {2,-12} | {3,-12} | {4,-10} | {5,-8} | {6,-3}",
"Id", "Last Name", "First Name", "Middle Name", "Date", "Gender", "Age");
            Console.WriteLine(new string('-', 82));
            stopwatch.Start();
            stopwatch.Stop();
            var returningTime = stopwatch.Elapsed;
            stopwatch.Start();
            var sortedEmployees = employeesList.OrderBy(i => i.LastName).ThenBy(i => i.FirstName).ThenBy(i => i.MiddleName);
            foreach (var e in sortedEmployees)
            {
                Console.WriteLine("{0, -7} | {1,-12} | {2,-12} | {3,-12} | {4,-10} | {5,-8} | {6,-3}",
                    e.Id, e.LastName, e.FirstName, e.MiddleName, e.DateOfBirth, e.Gender, e.AgeCalculate());
            }
            stopwatch.Stop();
            Console.WriteLine(new string('-', 82));
            var printTime = stopwatch.Elapsed;
            Console.WriteLine("Males count: " + employeesList.Count(i => i.Gender == "Male"));
            Console.WriteLine("Females count: " + employeesList.Count(i => i.Gender == "Female"));
            Console.WriteLine("Returning Time: " + returningTime);
            Console.WriteLine("Printg Time: " + printTime);
            Console.WriteLine("Count: " + employeesList.Count);
        }
    }
}