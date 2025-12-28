using System;

namespace Hospital.Common.Models
{
    public class Doctor : Staff
    {
        public string Specialty { get; set; }
        public double ConsultationFee { get; set; }
        public int YearsOfExperience { get; set; }

        public Doctor(string firstName, string lastName, DateTime birthDate, string employeeNumber,
            DateTime hireDate, string department, string specialty, double fee, int years)
            : base(firstName, lastName, birthDate, employeeNumber, hireDate, department)
        {
            Specialty = specialty;
            ConsultationFee = fee;
            YearsOfExperience = years;
        }

        public override string GetRole() => $"Doctor ({Specialty})";

        public string Diagnose(Patient p, string diagnosis)
        {
            return $"{FullName()} diagnosed {p.FullName()} with '{diagnosis}'";
        }

        public override string ToString()
        {
            return $"{FullName()} - {Specialty} (Exp: {YearsOfExperience} yrs)";
        }
    }
}