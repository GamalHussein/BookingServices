using BookingService.Application.Dtos.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces;
public interface IBookingService
{
	Task<BookingDto> GetByIdAsync(Guid id);
	Task<BookingDetailsDto> GetByIdWithDetailsAsync(Guid id);
	Task<IEnumerable<BookingDto>> GetAllAsync();
	Task<IEnumerable<BookingDto>> GetMyBookingsAsync(Guid customerId);
	Task<IEnumerable<BookingDto>> GetProviderBookingsAsync(Guid providerId);
	Task<BookingDto> CreateAsync(Guid customerId, CreateBookingDto createDto);
	Task<bool> UpdateAsync(Guid id, Guid customerId, UpdateBookingDto updateDto);
	Task<bool> CancelAsync(Guid id, Guid userId, CancelBookingDto cancelDto);
	Task<bool> ConfirmAsync(Guid id, Guid providerId);
	Task<bool> CompleteAsync(Guid id, Guid providerId);
}