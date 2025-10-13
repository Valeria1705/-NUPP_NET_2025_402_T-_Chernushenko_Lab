using System;

namespace Hospital.Common.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime Time { get; set; }
        public string Notes { get; set; }

        public Appointment(Guid patientId, Guid doctorId, DateTime time, string notes = "")
        {
            Id = Guid.NewGuid();
            PatientId = patientId;
            DoctorId = doctorId;
            Time = time;
            Notes = notes;
        }

        public override string ToString()
        {
            return $"Appointment {Id} - Patient:{PatientId} Doctor:{DoctorId} at {Time}";
        }
    }
}
