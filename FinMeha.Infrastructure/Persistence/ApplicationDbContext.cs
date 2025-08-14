using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinMeha.Domain.Entities;
using System.Reflection;

namespace FinMeha.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        //public DbSet<Domain.Entities.Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Configurações de mapeamento para as entidades do Domain
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
