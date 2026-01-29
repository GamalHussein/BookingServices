using BookingService.Application.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Dtos.Services;
public class ServiceDetailsDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public int DurationMinutes { get; set; }
	public bool IsActive { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }

	// Category Details
	public CategoryDto Category { get; set; }

	// Provider Details
	public ProviderDto Provider { get; set; }
}
