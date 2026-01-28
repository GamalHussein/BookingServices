using BookingService.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Infrastructure.Configurations;
public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
	public void Configure(EntityTypeBuilder<ApplicationUser> builder)
	{
		builder.Property(u => u.FirstName)
			.IsRequired()
			.HasMaxLength(100);
		builder.Property(u => u.LastName)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(u => u.UserType)
			.IsRequired()
			.HasConversion<int>();

		builder.Property(u => u.CreatedAt)
			.IsRequired()
			.HasDefaultValueSql("GETUTCDATE()");

		// Relationships
		builder.HasMany(u => u.Bookings)
			.WithOne(b => b.Customer)
			.HasForeignKey(b => b.CustomerId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasMany(u => u.Services)
			.WithOne(s => s.Provider)
			.HasForeignKey(s => s.ProviderId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasMany(u => u.Notifications)
			.WithOne(n => n.User)
			.HasForeignKey(n => n.UserId)
			.OnDelete(DeleteBehavior.Cascade);

		// Indexes
		builder.HasIndex(u => u.Email).IsUnique();
		builder.HasIndex(u => u.PhoneNumber);
	}
}
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
	public void Configure(EntityTypeBuilder<Payment> builder)
	{
		builder.HasKey(p => p.Id);

		builder.Property(p => p.Amount)
			.IsRequired()
			.HasPrecision(18, 2);

		builder.Property(p => p.PaymentMethod)
			.IsRequired()
			.HasConversion<int>();

		builder.Property(p => p.PaymentStatus)
			.IsRequired()
			.HasConversion<int>();

		builder.Property(p => p.TransactionId)
			.HasMaxLength(200);

		builder.Property(p => p.PaymentGatewayResponse)
			.HasMaxLength(2000);

		builder.Property(p => p.PaymentDate)
			.IsRequired();

		builder.HasOne(p => p.Booking)
			.WithOne(b => b.Payment)
			.HasForeignKey<Payment>(p => p.BookingId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasIndex(p => p.BookingId).IsUnique();
		builder.HasIndex(p => p.TransactionId);
		builder.HasIndex(p => p.PaymentStatus);
	}
}
