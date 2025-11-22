using System;

namespace Hospital.Common.Models
{
    public class Bus
    {
        private static readonly Random _random = new Random();

        public Guid Id { get; set; } = Guid.NewGuid();
        public required string NumberPlate { get; set; }
        public int Seats { get; set; }
        public double Mileage { get; set; }

        public static Bus CreateNew()
        {
            return new Bus
            {
                NumberPlate = $"BUS-{_random.Next(1000, 9999)}",
                Seats = _random.Next(20, 60),
                Mileage = Math.Round(_random.NextDouble() * 200_000, 2)
            };
        }

        public override string ToString() => $"{NumberPlate} | Seats: {Seats} | Mileage: {Mileage} km";
    }
}
