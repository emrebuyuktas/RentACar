using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Domain.Entities;

namespace RentACar.Persistence.EntityConfigurations;

public class TransmissionConfiguration : IEntityTypeConfiguration<Transmission>
{
    public void Configure(EntityTypeBuilder<Transmission> builder)
    {
        builder.ToTable("Transmissions");

        builder.Property(t => t.Id).HasColumnName("Id").IsRequired();
        builder.Property(t => t.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
        builder.Property(t => t.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(t => t.DeletedDate).HasColumnName("DeletedDate");
        builder.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");

        builder.HasIndex(t => t.Name, name: "UK_FUELS_NAME").IsUnique();

        builder.HasMany(t => t.Models);

        builder.HasQueryFilter(t => !t.DeletedDate.HasValue);
    }
}
