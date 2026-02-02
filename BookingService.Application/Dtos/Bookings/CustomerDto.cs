namespace BookingService.Application.Dtos.Bookings;

public class CustomerDto
{
	public Guid Id { get; set; }
	public string FullName { get; set; }
	public string Email { get; set; }
	public string PhoneNumber { get; set; }
}
