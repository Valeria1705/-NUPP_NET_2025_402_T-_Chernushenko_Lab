using System;
using System.IO;
using System.Linq;
using Hospital.Common.Models;
using Hospital.Common.Services;
using Hospital.Common.Extensions;

namespace Hospital.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var patientService = new CrudService<Patient>();
            var doctorService = new CrudService<Doctor>();
            var nurseService = new CrudService<Nurse>();
            var appointmentService = new CrudService<Appointment>();

            patientService.OnCreated += (s, p) => Console.WriteLine($"[EVENT] ➕ Новий пацієнт: {p}");
            patientService.OnUpdated += (s, p) => Console.WriteLine($"[EVENT] 🔄 Оновлено пацієнта: {p}");
            patientService.OnRemoved += (s, p) => Console.WriteLine($"[EVENT] ❌ Видалено пацієнта: {p}");

            Console.WriteLine("\n--- Створення об’єктів ---");
            var patient1 = new Patient("Іван", "Іваненко", new DateTime(1985, 5, 12), "MRN-0001") { BloodType = "A+", InsuranceCompany = "GreenHealth" };
            var patient2 = new Patient("Олена", "Шевченко", new DateTime(1990, 11, 1), "MRN-0002") { BloodType = "B-", InsuranceCompany = "MediLife" };
            var patient3 = new Patient("Петро", "Сидоренко", new DateTime(1977, 3, 22), "MRN-0003") { BloodType = "O+" };

            patientService.Create(patient1);
            patientService.Create(patient2);
            patientService.Create(patient3);

            var doc1 = new Doctor("Петро", "Коваленко", new DateTime(1978, 3, 4), "E001", new DateTime(2010, 2, 1),
                                  "Кардіологія", "Кардіолог", 120.0, 15);
            var doc2 = new Doctor("Анна", "Мельник", new DateTime(1982, 7, 20), "E002", new DateTime(2015, 6, 15),
                                  "Неврологія", "Невропатолог", 140.0, 9);
            doctorService.Create(doc1);
            doctorService.Create(doc2);

            var nurse1 = new Nurse("Марта", "Хмелюк", new DateTime(1992, 2, 2), "N100", new DateTime(2019, 1, 10),
                                   "Кардіологія", "Старша медсестра");
            nurseService.Create(nurse1);

            Console.WriteLine("\n--- Усі пацієнти ---");
            foreach (var p in patientService.ReadAll())
                Console.WriteLine(p);

            Console.WriteLine("\n--- Створення прийомів ---");
            var app1 = new Appointment(patient1.Id, doc1.Id, DateTime.Now.AddDays(1), "Плановий огляд");
            var app2 = new Appointment(patient2.Id, doc2.Id, DateTime.Now.AddDays(2), "Консультація щодо головного болю");
            appointmentService.Create(app1);
            appointmentService.Create(app2);

            foreach (var a in appointmentService.ReadAll())
                Console.WriteLine(a);

            Console.WriteLine("\n--- Коротка інформація про пацієнтів ---");
            var shortInfo = patientService.ReadAll().ToShortInfo();
            foreach (var info in shortInfo) Console.WriteLine(info);

            Console.WriteLine("\n--- Фільтрація пацієнтів за страховою компанією GreenHealth ---");
            var filtered = patientService.ReadAll().FilterByInsurance("GreenHealth");
            foreach (var f in filtered) Console.WriteLine(f);

            Console.WriteLine("\n--- Госпіталізація пацієнта ---");
            patient1.Admit();
            patientService.Update(patient1);

            Console.WriteLine("\n--- Виписка пацієнта ---");
            patient1.Discharge();
            patientService.Update(patient1);

            Console.WriteLine("\n--- Робота медсестри ---");
            nurse1.ToggleDuty();
            nurse1.AssignPatient();
            nurse1.AssignPatient();
            Console.WriteLine($"{nurse1.FullName()} — {nurse1.GetRole()} | Пацієнтів закріплено: {nurse1.PatientsAssigned}, Чергує: {nurse1.IsOnDuty}");

            Console.WriteLine("\n--- Робота лікаря ---");
            string diag = doc1.Diagnose(patient2, "Гіпертонія");
            Console.WriteLine(diag);

            Console.WriteLine("\n--- Збереження пацієнтів у файл ---");
            var dataDir = Path.Combine(Directory.GetCurrentDirectory(), "data");
            Directory.CreateDirectory(dataDir);
            var patientFile = Path.Combine(dataDir, "patients.json");
            patientService.Save(patientFile);
            Console.WriteLine($"Файл збережено за шляхом: {patientFile}");

            Console.WriteLine("\n--- Завантаження пацієнтів з файлу ---");
            var patientService2 = new CrudService<Patient>();
            patientService2.Load(patientFile);
            foreach (var p in patientService2.ReadAll())
                Console.WriteLine($"[Завантажено] {p}");

            Console.WriteLine("\n--- Статичні поля ---");
            Console.WriteLine($"Загальна кількість створених осіб: {Person.GetPersonCount()}");

            Console.WriteLine("\n--- Видалення пацієнта Петро Сидоренко ---");
            patientService.Remove(patient3);

            Console.WriteLine("\n═══════════════════════════════════════════════════");
            Console.WriteLine("🏁 Роботу завершено. Усі операції виконано успішно!");
            Console.WriteLine("═══════════════════════════════════════════════════");
            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}
