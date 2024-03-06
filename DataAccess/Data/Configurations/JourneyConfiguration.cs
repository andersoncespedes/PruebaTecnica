using Business.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Data.Configurations;
public class JourneyConfiguration : IEntityTypeConfiguration<Journey>
{
    public void Configure(EntityTypeBuilder<Journey> builder)
    {
        builder.Property(e => e.Origin)
        .HasMaxLength(30)
        .IsRequired();

        builder.Property(e => e.Price)
        .HasPrecision(15, 2)
        .IsRequired();

        builder.Property(e => e.Destination)
        .HasMaxLength(30)
        .IsRequired();

        builder.HasMany(e => e.Flights)
        .WithMany(e => e.Journies)
        .UsingEntity<JourneyFlight>(

            jp => jp.HasOne(e => e.Flight)
            .WithMany(e => e.JourneyFlights)
            .HasForeignKey(e => e.IdFlightFK),

            jp => jp.HasOne(e => e.Journey)
            .WithMany(e => e.JourneyFlights)
            .HasForeignKey(e => e.IdJourneyFK),
            entity =>
            {
                entity.HasKey(e => new { e.IdJourneyFK, e.IdFlightFK });
            }
        );
    }
}