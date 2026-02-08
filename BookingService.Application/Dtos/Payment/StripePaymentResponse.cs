namespace BookingService.Application.Dtos.Payment;

public class StripePaymentResponse
{
	public string ClientSecret { get; set; }
	public string PaymentIntentId { get; set; }
	public decimal Amount { get; set; }
}
