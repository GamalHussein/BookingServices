using BookingService.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
	public void Configure(EntityTypeBuilder<Booking> builder)
	{
		builder.HasKey(b => b.Id);

		builder.Property(b => b.BookingDate)
			.IsRequired();

		builder.Property(b => b.Status)
			.IsRequired()
			.HasConversion<int>();

		builder.Property(b => b.TotalPrice)
			.IsRequired()
			.HasPrecision(18, 2);

		builder.Property(b => b.Notes)
			.HasMaxLength(1000);

		builder.Property(b => b.CreatedAt)
			.IsRequired();

		builder.HasOne(b => b.Customer)
			.WithMany(u => u.Bookings)
			.HasForeignKey(b => b.CustomerId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(b => b.Service)
			.WithMany(s => s.Bookings)
			.HasForeignKey(b => b.ServiceId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(b => b.Payment)
			.WithOne(p => p.Booking)
			.HasForeignKey<Payment>(p => p.BookingId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasIndex(b => b.CustomerId);
		builder.HasIndex(b => b.ServiceId);
		builder.HasIndex(b => b.Status);
		builder.HasIndex(b => b.BookingDate);
	}
}
