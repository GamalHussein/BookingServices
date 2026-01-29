using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Dtos.Services;
public class CreateServiceDto
{
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public int DurationMinutes { get; set; }
	public Guid CategoryId { get; set; }
	public Guid ProviderId { get; set; }
}
