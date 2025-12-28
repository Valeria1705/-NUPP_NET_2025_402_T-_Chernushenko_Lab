using System;

namespace Hospital.Infrastructure.Models
{
    public class DoctorPatientModel
    {
        public Guid DoctorId { get; set; }
        public DoctorModel Doctor { get; set; } = null!;

        public Guid PatientId { get; set; }
        public PatientModel Patient { get; set; } = null!;

        public DateTime Since { get; set; } = DateTime.UtcNow;
    }
}
