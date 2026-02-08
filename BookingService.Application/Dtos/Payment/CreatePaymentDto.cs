using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Dtos.Payment;
public class CreatePaymentDto
{
	public Guid BookingId { get; set; }
}
