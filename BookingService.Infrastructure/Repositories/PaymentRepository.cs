using BookingService.Application.Interfaces;
using BookingService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure.Repositories;
public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
{
	public PaymentRepository(Data.ApplicationDbContext context) : base(context)
	{
	}
	public async Task<Payment> GetByBookingIdAsync(Guid bookingId)
	{
		return await _dbSet
			.Include(p => p.Booking)
				.ThenInclude(b => b.Customer)
			.Include(p => p.Booking)
				.ThenInclude(b => b.Service)
			.FirstOrDefaultAsync(p => p.BookingId == bookingId);
	}
}
