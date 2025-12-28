using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hospital.Common.Models;
using Hospital.Common.Services;

namespace Hospital.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var dataDir = Path.Combine(Directory.GetCurrentDirectory(), "data");
            Directory.CreateDirectory(dataDir);
            var busFile = Path.Combine(dataDir, "buses.json");

            var busService = new CrudServiceAsync<Bus>(busFile);
            int totalBuses = 1000;

            Console.WriteLine("\n--- Паралельне створення Bus ---");

            await Parallel.ForEachAsync(Enumerable.Range(0, totalBuses), async (i, ct) =>
            {
                var bus = Bus.CreateNew();
                await busService.CreateAsync(bus);
            });

            var allBuses = await busService.ReadAllAsync();
            Console.WriteLine("\n--- Статистика Bus ---");
            Console.WriteLine($"Seats: Min={allBuses.Min(b => b.Seats)}, Max={allBuses.Max(b => b.Seats)}, Avg={allBuses.Average(b => b.Seats):F2}");
            Console.WriteLine($"Mileage: Min={allBuses.Min(b => b.Mileage)}, Max={allBuses.Max(b => b.Mileage)}, Avg={allBuses.Average(b => b.Mileage):F2}");

            await busService.SaveAsync();
            Console.WriteLine($"\nЗбережено {totalBuses} об’єктів у файл {busFile}");

            Console.WriteLine("\n--- Демонстрація Lock, Semaphore, AutoResetEvent ---");

            object locker = new object();
            SemaphoreSlim semaphore = new SemaphoreSlim(3);
            AutoResetEvent autoEvent = new AutoResetEvent(false);

            await Parallel.ForEachAsync(Enumerable.Range(0, 10), async (i, ct) =>
            {
                lock (locker)
                {
                    Console.WriteLine($"Lock: Потік {i} працює");
                }

                await semaphore.WaitAsync();
                try
                {
                    Console.WriteLine($"Semaphore: Потік {i} працює");
                    await Task.Delay(50);
                }
                finally
                {
                    semaphore.Release();
                }

                Console.WriteLine($"AutoResetEvent: Потік {i} сигналізує");
                autoEvent.Set();
            });

            Console.WriteLine("\n🏁 Роботу завершено!");
            Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}