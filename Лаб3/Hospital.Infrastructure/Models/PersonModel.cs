using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Infrastructure.Models
{
    public abstract class PersonModel
    {
        public Guid Id { get; set; }

        [MaxLength(80)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(80)]
        public string LastName { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }
    }
}
