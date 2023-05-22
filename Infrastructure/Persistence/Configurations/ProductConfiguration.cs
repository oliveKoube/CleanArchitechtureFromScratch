using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                id => id.Value,
                value => new ProductId(value));

            builder.Property(x => x.Name)
                .HasMaxLength(100);

            builder.Property(x => x.Sku)
                .HasConversion(
                sku => sku.Value,
                value => Sku.Create(value)!);

            builder.OwnsOne(p => p.Price, priceBuilder =>
            {
                priceBuilder.Property(p => p.Currency).HasMaxLength(3);

            });
        }
    }
}
