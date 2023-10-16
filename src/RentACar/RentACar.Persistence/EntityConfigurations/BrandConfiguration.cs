using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Domain.Entities;

namespace RentACar.Persistence.EntityConfigurations;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Brands");

        builder.Property(b=>b.Id).HasColumnName("Id").IsRequired();
        builder.Property(b=>b.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
        builder.Property(b=>b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");
        builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");

        builder.HasIndex(b => b.Name,name:"UK_BRANDS_NAME").IsUnique();

        builder.HasMany(b => b.Models);

        builder.HasQueryFilter(b => !b.DeletedDate.HasValue);
    }
}