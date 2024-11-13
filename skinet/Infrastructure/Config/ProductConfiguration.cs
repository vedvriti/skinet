using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{//Tell store context class where this product configuration is located
    public void Configure(EntityTypeBuilder<Product> builder)
    {
       builder.Property(x => x.price).HasColumnType("decimal(18,2)");
       builder.Property(x => x.Name).IsRequired();
    }
}
