using System.ComponentModel.DataAnnotations;

namespace Hospital.Infrastructure.Models
{
    public class NurseModel : StaffModel
    {
        [MaxLength(50)]
        public string Rank { get; set; } = string.Empty;

        public int PatientsAssigned { get; set; }
        public bool IsOnDuty { get; set; }
    }
}
