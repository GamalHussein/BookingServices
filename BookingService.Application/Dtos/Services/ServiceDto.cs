namespace BookingService.Application.Dtos.Services;

public class ServiceDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public int DurationMinutes { get; set; }
	public bool IsActive { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }

	// Category Info
	public Guid CategoryId { get; set; }
	public string CategoryName { get; set; }

	// Provider Info
	public Guid ProviderId { get; set; }
	public string ProviderName { get; set; }
}