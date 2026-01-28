using BookingService.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BookingService.Infrastructure.Data;
public class ApplicationDbContext:
	IdentityDbContext<ApplicationUser,IdentityRole<Guid>, Guid>
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	public DbSet<Category> Categories { get; set; }
	public DbSet<Service> Services { get; set; }
	public DbSet<Booking> Bookings { get; set; }
	public DbSet<Payment> Payments { get; set; }
	public DbSet<Notification> Notifications { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		// Apply all configurations from current assembly
		builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		
	}

	
	}
