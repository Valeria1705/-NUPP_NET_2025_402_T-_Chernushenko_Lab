using System;

namespace Hospital.Common.Models
{
    public class Patient : Person
    {
        public string MedicalRecordNumber { get; set; }
        public string InsuranceCompany { get; set; }
        public string BloodType { get; set; }
        public bool IsAdmitted { get; set; }

        public Patient(string firstName, string lastName, DateTime birthDate, string medicalRecordNumber)
            : base(firstName, lastName, birthDate)
        {
            MedicalRecordNumber = medicalRecordNumber;
            InsuranceCompany = DefaultInsuranceCompany();
            BloodType = "Unknown";
            IsAdmitted = false;
        }

        public static string DefaultInsuranceCompany() => "National Health";

        public void Admit()
        {
            IsAdmitted = true;
        }

        public void Discharge()
        {
            IsAdmitted = false;
        }

        public override string ToString()
        {
            return $"{FullName()} (MRN: {MedicalRecordNumber}, Blood: {BloodType}, Admitted: {IsAdmitted})";
        }
    }
}