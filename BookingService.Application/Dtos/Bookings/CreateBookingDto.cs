using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Dtos.Bookings;
public class CreateBookingDto
{
	public Guid ServiceId { get; set; }
	public DateTime BookingDate { get; set; }
	public string Notes { get; set; }
}
