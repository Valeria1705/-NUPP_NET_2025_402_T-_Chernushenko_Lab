using Hospital.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }

        public DbSet<PersonModel> People => Set<PersonModel>();
        public DbSet<StaffModel> Staff => Set<StaffModel>();
        public DbSet<DoctorModel> Doctors => Set<DoctorModel>();
        public DbSet<NurseModel> Nurses => Set<NurseModel>();
        public DbSet<PatientModel> Patients => Set<PatientModel>();

        public DbSet<AppointmentModel> Appointments => Set<AppointmentModel>();
        public DbSet<PatientCardModel> PatientCards => Set<PatientCardModel>();
        public DbSet<DoctorPatientModel> DoctorPatients => Set<DoctorPatientModel>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ---------- TPT (Table-per-Type) ----------
            modelBuilder.Entity<PersonModel>().ToTable("People");
            modelBuilder.Entity<StaffModel>().ToTable("Staff");
            modelBuilder.Entity<DoctorModel>().ToTable("Doctors");
            modelBuilder.Entity<NurseModel>().ToTable("Nurses");
            modelBuilder.Entity<PatientModel>().ToTable("Patients");

            // ---------- Keys ----------
            modelBuilder.Entity<PersonModel>().HasKey(x => x.Id);
            modelBuilder.Entity<AppointmentModel>().HasKey(x => x.Id);
            modelBuilder.Entity<PatientCardModel>().HasKey(x => x.Id);

            // ---------- Unique indexes ----------
            modelBuilder.Entity<PatientModel>()
                .HasIndex(x => x.MedicalRecordNumber)
                .IsUnique();

            modelBuilder.Entity<StaffModel>()
                .HasIndex(x => x.EmployeeNumber)
                .IsUnique();

            // ---------- 1-to-many: Patient -> Appointments ----------
            modelBuilder.Entity<AppointmentModel>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // ---------- 1-to-many: Doctor -> Appointments ----------
            modelBuilder.Entity<AppointmentModel>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // ---------- 1-to-1: Patient -> PatientCard ----------
            modelBuilder.Entity<PatientCardModel>()
                .HasOne(c => c.Patient)
                .WithOne(p => p.Card)
                .HasForeignKey<PatientCardModel>(c => c.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PatientCardModel>()
                .HasIndex(c => c.PatientId)
                .IsUnique();

            // ---------- many-to-many via join entity ----------
            modelBuilder.Entity<DoctorPatientModel>()
                .HasKey(x => new { x.DoctorId, x.PatientId });

            modelBuilder.Entity<DoctorPatientModel>()
                .HasOne(x => x.Doctor)
                .WithMany(d => d.DoctorPatients)
                .HasForeignKey(x => x.DoctorId);

            modelBuilder.Entity<DoctorPatientModel>()
                .HasOne(x => x.Patient)
                .WithMany(p => p.DoctorPatients)
                .HasForeignKey(x => x.PatientId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
