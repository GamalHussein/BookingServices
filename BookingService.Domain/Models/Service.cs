using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.Models;
public class Service
{
	#region Fields
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public int DurationInMinutes { get; set; }
	public bool IsActive { get; set; } = true;
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime? UpdatedAt { get; set; }
	#endregion


	#region Navigation Properties
	public Guid ProviderId { get; set; }
	public Guid CategoryId { get; set; }

	public ApplicationUser Provider { get; set; }
	public Category Category { get; set; }
	public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
	#endregion

}
