using Business.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Data.Configurations;
public class TransportConfiguration : IEntityTypeConfiguration<Transport>
{
    public void Configure(EntityTypeBuilder<Transport> builder)
    {
        builder.Property(e => e.FlightCarrier)
        .HasMaxLength(30)
        .IsRequired();

        builder.Property(e => e.FlightNumber)
        .HasMaxLength(30)
        .IsRequired();
    }
}