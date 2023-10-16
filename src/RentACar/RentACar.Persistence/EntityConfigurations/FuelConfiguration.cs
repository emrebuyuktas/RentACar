using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Domain.Entities;

namespace RentACar.Persistence.EntityConfigurations;

public class FuelConfiguration : IEntityTypeConfiguration<Fuel>
{
    public void Configure(EntityTypeBuilder<Fuel> builder)
    {
        builder.ToTable("Fuels");

        builder.Property(f => f.Id).HasColumnName("Id").IsRequired();
        builder.Property(f => f.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
        builder.Property(f => f.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(f => f.DeletedDate).HasColumnName("DeletedDate");
        builder.Property(f => f.UpdatedDate).HasColumnName("UpdatedDate");

        builder.HasIndex(f => f.Name, name: "UK_FUELS_NAME").IsUnique();

        builder.HasMany(f => f.Models);

        builder.HasQueryFilter(f => !f.DeletedDate.HasValue);
    }
}
