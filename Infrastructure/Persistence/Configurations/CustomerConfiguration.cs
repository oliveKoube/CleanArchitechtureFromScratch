using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedNever()
                .HasConversion(
                 id => id.Value,
                 value => new CustomerId(value));

            builder.Property(p => p.Name)
                .HasMaxLength(100);

            builder.Property(p => p.Email)
                .HasMaxLength(255);

            builder.HasIndex(p => p.Email)
                .IsUnique();
        }
    }
}
