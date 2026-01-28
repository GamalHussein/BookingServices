using BookingService.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
	public void Configure(EntityTypeBuilder<Service> builder)
	{
		builder.HasKey(s => s.Id);

		builder.Property(s => s.Name)
			.IsRequired()
			.HasMaxLength(200);

		builder.Property(s => s.Description)
			.HasMaxLength(1000);

		builder.Property(s => s.Price)
			.IsRequired()
			.HasPrecision(18, 2);

		builder.Property(s => s.DurationInMinutes)
			.IsRequired();

		builder.Property(s => s.CreatedAt)
			.IsRequired();

		builder.HasOne(s => s.Category)
			.WithMany(c => c.Services)
			.HasForeignKey(s => s.CategoryId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(s => s.Provider)
			.WithMany(u => u.Services)
			.HasForeignKey(s => s.ProviderId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasMany(s => s.Bookings)
			.WithOne(b => b.Service)
			.HasForeignKey(b => b.ServiceId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasIndex(s => s.CategoryId);
		builder.HasIndex(s => s.ProviderId);
		builder.HasIndex(s => s.IsActive);
	}
}