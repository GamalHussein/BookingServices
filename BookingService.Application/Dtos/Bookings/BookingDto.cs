namespace BookingService.Application.Dtos.Bookings;

public class BookingDto
{
	public Guid Id { get; set; }
	public DateTime BookingDate { get; set; }
	public string Status { get; set; }
	public decimal TotalPrice { get; set; }
	public string Notes { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public DateTime? CompletedAt { get; set; }
	public DateTime? CancelledAt { get; set; }

	// Customer Info
	public Guid CustomerId { get; set; }
	public string CustomerName { get; set; }
	public string CustomerPhone { get; set; }

	// Service Info
	public Guid ServiceId { get; set; }
	public string ServiceName { get; set; }
	public int ServiceDuration { get; set; }
}
