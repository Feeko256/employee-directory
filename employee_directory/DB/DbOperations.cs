using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using employee_directory.Model;
using Microsoft.EntityFrameworkCore;

namespace employee_directory.DB
{
    public class DbOperations
    {
        public static void Initialize()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
        public static List<Employee> ReturnEmployees()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Employees.ToList();
            }
        }
        public static List<Employee> ReturnEmployeesLastNameStartsWithF()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Employees.Where(i => i.LastName.StartsWith("F") && i.Gender == "Male").ToList();
            }
        }
        public static void Insert(List<Employee> employees)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Employees.AddRange(employees);
                db.SaveChanges();
            }
        }
    }
}
