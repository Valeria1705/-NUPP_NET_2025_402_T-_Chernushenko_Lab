using System;

namespace Hospital.Infrastructure.Models
{
    public class AppointmentModel
    {
        public Guid Id { get; set; }

        public Guid PatientId { get; set; }
        public PatientModel Patient { get; set; } = null!;

        public Guid DoctorId { get; set; }
        public DoctorModel Doctor { get; set; } = null!;

        public DateTime Time { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
