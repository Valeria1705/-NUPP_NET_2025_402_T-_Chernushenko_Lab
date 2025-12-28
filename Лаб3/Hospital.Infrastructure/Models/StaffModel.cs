using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Infrastructure.Models
{
    public abstract class StaffModel : PersonModel
    {
        [MaxLength(30)]
        public string EmployeeNumber { get; set; } = string.Empty;

        public DateTime HireDate { get; set; }

        [MaxLength(80)]
        public string Department { get; set; } = string.Empty;
    }
}
