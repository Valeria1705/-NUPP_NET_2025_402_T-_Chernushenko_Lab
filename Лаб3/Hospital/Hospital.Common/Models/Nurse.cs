using System;

namespace Hospital.Common.Models
{
    public class Nurse : Staff
    {
        public string Rank { get; set; }
        public int PatientsAssigned { get; set; }
        public bool IsOnDuty { get; set; }

        public Nurse(string firstName, string lastName, DateTime birthDate, string employeeNumber,
            DateTime hireDate, string department, string rank)
            : base(firstName, lastName, birthDate, employeeNumber, hireDate, department)
        {
            Rank = rank;
            PatientsAssigned = 0;
            IsOnDuty = false;
        }

        public void AssignPatient()
        {
            PatientsAssigned++;
        }

        public void ToggleDuty()
        {
            IsOnDuty = !IsOnDuty;
        }

        public override string GetRole() => $"Nurse ({Rank})";

        public override string ToString()
        {
            return $"{FullName()} - {Rank} Nurse (OnDuty: {IsOnDuty})";
        }
    }
}