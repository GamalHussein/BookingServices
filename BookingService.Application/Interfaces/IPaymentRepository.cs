using BookingService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces;
public interface IPaymentRepository: IBaseRpository<Payment>
{
	Task<Payment> GetByBookingIdAsync(Guid bookingId);
}
