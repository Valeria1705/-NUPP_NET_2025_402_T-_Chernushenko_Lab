using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Infrastructure.Models
{
    public class DoctorModel : StaffModel
    {
        [MaxLength(80)]
        public string Specialty { get; set; } = string.Empty;

        public double ConsultationFee { get; set; }
        public int YearsOfExperience { get; set; }

        // 1-to-many: Doctor -> Appointments
        public List<AppointmentModel> Appointments { get; set; } = new();

        // many-to-many via join entity
        public List<DoctorPatientModel> DoctorPatients { get; set; } = new();
    }
}
