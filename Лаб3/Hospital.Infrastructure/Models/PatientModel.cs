using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Infrastructure.Models
{
    public class PatientModel : PersonModel
    {
        [MaxLength(30)]
        public string MedicalRecordNumber { get; set; } = string.Empty;

        [MaxLength(120)]
        public string InsuranceCompany { get; set; } = "National Health";

        [MaxLength(10)]
        public string BloodType { get; set; } = "Unknown";

        public bool IsAdmitted { get; set; }

        // 1-to-many: Patient -> Appointments
        public List<AppointmentModel> Appointments { get; set; } = new();

        // 1-to-1: Patient -> PatientCard
        public PatientCardModel? Card { get; set; }

        // many-to-many via join entity
        public List<DoctorPatientModel> DoctorPatients { get; set; } = new();
    }
}
