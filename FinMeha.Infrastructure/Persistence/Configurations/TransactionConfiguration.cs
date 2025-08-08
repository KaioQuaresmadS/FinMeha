using FinMeha.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinMeha.Infrastructure.Persistence.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Domain.Entities.Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(t => t.Amount)
                        .HasPrecision(18, 2)
                        .IsRequired();

            builder.Property(t => t.Amount)
                        .HasMaxLength(200)
                        .IsRequired();
        }
    }
}
