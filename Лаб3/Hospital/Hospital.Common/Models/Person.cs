using System;

namespace Hospital.Common.Models
{
    public abstract class Person
    {
        public static int PersonCount;

        static Person()
        {
            PersonCount = 0;
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        protected Person(string firstName, string lastName, DateTime birthDate)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            PersonCount++;
        }
        public string FullName() => $"{FirstName} {LastName}";

        public static int GetPersonCount() => PersonCount;
    }
}