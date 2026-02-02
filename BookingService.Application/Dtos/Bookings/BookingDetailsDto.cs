using BookingService.Application.Dtos.Services;

namespace BookingService.Application.Dtos.Bookings;

public class BookingDetailsDto
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

	// Customer Details
	public CustomerDto Customer { get; set; }

	// Service Details
	public ServiceDto Service { get; set; }

	// Payment Details (if exists)
	public PaymentDto Payment { get; set; }
}
