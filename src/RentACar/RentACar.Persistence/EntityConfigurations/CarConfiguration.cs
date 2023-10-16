﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RentACar.Domain.Entities;

namespace RentACar.Persistence.EntityConfigurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("Cars").HasKey(b => b.Id);

        builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
        builder.Property(b => b.ModelId).HasColumnName("ModelId").IsRequired();
        builder.Property(b => b.Km).HasColumnName("Kilometer").IsRequired();
        builder.Property(b => b.CarState).HasColumnName("State");
        builder.Property(b => b.Year).HasColumnName("ModelYear");

        builder.HasOne(b => b.Model);

        builder.HasQueryFilter(b => !b.DeletedDate.HasValue);
    }
}