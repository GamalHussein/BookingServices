using BookingService.Domain.Enums;
using BookingService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces;
public interface IBookingRepository : IBaseRpository<Booking>
{
	
	Task<Booking> GetByIdWithDetailsAsync(Guid id);
	Task<IEnumerable<Booking>> GetByCustomerAsync(Guid customerId);
	Task<IEnumerable<Booking>> GetByProviderAsync(Guid providerId);
	Task<IEnumerable<Booking>> GetByStatusAsync(BookingStatus status);
	
}
