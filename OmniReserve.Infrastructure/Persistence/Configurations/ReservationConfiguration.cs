using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmniReserve.Domain.Entities;

namespace OmniReserve.Infrastructure.Persistence.Configurations;

public class ReservationConfiguration
    : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("Reservations");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.UserId)
            .IsRequired();

        builder.Property(r => r.RoomId)
            .IsRequired();

        builder.HasOne(r => r.Room)
            .WithMany(room => room.Reservations)
            .HasForeignKey(r => r.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.User)
            .WithMany(user => user.Reservations)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(r => r.CheckInDate)
            .HasColumnName("StartDate")
            .IsRequired();

        builder.Property(r => r.CheckOutDate)
            .HasColumnName("EndDate")
            .IsRequired();
    }
}