using BookingService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.Models;
public class Booking
{
	#region fields
	public Guid Id { get; set; }
	public DateTime BookingDate { get; set; }
	public BookingStatus Status { get; set; } =BookingStatus.Pending;
	public decimal TotalPrice { get; set; }
	public string Notes { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime? UpdatedAt { get; set; }
	public DateTime? CompletedAt { get; set; }
	public DateTime? CancelledAt { get; set; }
	#endregion

	#region Navigation Properties
	public Guid CustomerId { get; set; }
	public Guid ServiceId { get; set; }
	public ApplicationUser Customer { get; set; }
	public Service Service { get; set; }
	public Payment Payment { get; set; }
	#endregion
}
