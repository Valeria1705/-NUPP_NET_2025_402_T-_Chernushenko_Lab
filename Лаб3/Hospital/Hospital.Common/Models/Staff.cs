using System;

namespace Hospital.Common.Models
{
    public abstract class Staff : Person
    {
        public string EmployeeNumber { get; set; }
        public DateTime HireDate { get; set; }
        public string Department { get; set; }
        protected Staff(string firstName, string lastName, DateTime birthDate, string employeeNumber, DateTime hireDate, string department)
            : base(firstName, lastName, birthDate)
        {
            EmployeeNumber = employeeNumber;
            HireDate = hireDate;
            Department = department;
        }

        public virtual string GetRole() => "Staff";

        public string GetEmploymentLength() => GetEmploymentLength(DateTime.Now);
        public string GetEmploymentLength(DateTime asOf)
        {
            var span = asOf - HireDate;
            return $"{(int)(span.TotalDays / 365.25)} years";
        }
    }
}