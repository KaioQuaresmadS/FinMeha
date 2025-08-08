using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;

namespace FinMeha.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // ===================================================================
            // PLANO B: COLOQUE SUA CONNECTION STRING DE DESENVOLVIMENTO AQUI
            // ===================================================================
            var connectionString = "Server=P2345420-2\\SQLEXPRESS;Database=FinMehaDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";

            // Certifique-se de que a string acima está correta!

            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}