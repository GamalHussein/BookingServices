namespace BookingService.Application.Dtos.Payment;

public class PaymentDto
{
	public Guid Id { get; set; }
	public Guid BookingId { get; set; }
	public decimal Amount { get; set; }
	public string Status { get; set; }
	public string TransactionId { get; set; }
	public DateTime? PaidAt { get; set; }
	public DateTime CreatedAt { get; set; }
}
