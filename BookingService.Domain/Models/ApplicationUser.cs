using BookingService.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace BookingService.Domain.Models;

public class ApplicationUser:IdentityUser<Guid>
{
	#region Fields
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public UserType UserType { get; set; }
	public DateTime CreatedAt { get; set; }
	#endregion

	#region Navigation Properties
	public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
	public ICollection<Service> Services { get; set; } = new List<Service>();
	public ICollection<Notification> Notifications { get; set; }= new List<Notification>();
	#endregion
}
