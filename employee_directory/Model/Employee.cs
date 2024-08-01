using employee_directory.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employee_directory.Model
{
    public class Employee
    {
        private string firstName = "";
        private string middleName = "";
        private string lastName = "";
        private string gender = "";
        private DateOnly dateOfBirth = new DateOnly();
        public int Id { get; set; }
        public string LastName { get { return lastName; } private set { lastName = value; } }
        public string FirstName { get { return firstName; } private set { firstName = value; } }
        public string MiddleName { get { return middleName; } private set { middleName = value; } }
        public string Gender { get { return gender; } private set { gender = value; } }
        public DateOnly DateOfBirth { get { return dateOfBirth; } private set { dateOfBirth = value; } }

        public Employee(string lastName, string firstName, string middleName, string gender, DateOnly dateOfBirth)
        {
            this.lastName = lastName;
            this.firstName = firstName;
            this.middleName = middleName;
            this.gender = gender;
            this.dateOfBirth = dateOfBirth;
        }

        public void SendDataToDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Employees.AddAsync(this);
                db.SaveChanges();
            }
        }
        public int AgeCalculate() => DateTime.Today.Year - DateOfBirth.Year;
    }
}
