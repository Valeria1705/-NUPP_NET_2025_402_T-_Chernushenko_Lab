using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Infrastructure.Models
{
    public class PatientCardModel
    {
        public Guid Id { get; set; }

        public Guid PatientId { get; set; }
        public PatientModel Patient { get; set; } = null!;

        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(30)]
        public string Phone { get; set; } = string.Empty;

        public string Allergies { get; set; } = string.Empty;
    }
}
