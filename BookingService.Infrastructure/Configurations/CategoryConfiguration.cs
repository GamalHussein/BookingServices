using BookingService.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder.HasKey(c => c.Id);

		builder.Property(c => c.Name)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(c => c.Description)
			.HasMaxLength(500);

		builder.Property(c => c.CreatedAt)
			.IsRequired();

		builder.HasMany(c => c.Services)
			.WithOne(s => s.Category)
			.HasForeignKey(s => s.CategoryId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasIndex(c => c.Name);
	}
}