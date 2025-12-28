using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Hospital.Infrastructure
{
    public class HospitalContextFactory : IDesignTimeDbContextFactory<HospitalContext>
    {
        public HospitalContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();

            // SQL Server LocalDB (Windows)
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=HospitalDb;Trusted_Connection=True;TrustServerCertificate=True;");

            return new HospitalContext(optionsBuilder.Options);
        }
    }
}
