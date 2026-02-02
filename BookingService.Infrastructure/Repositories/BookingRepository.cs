using BookingService.Application.Interfaces;
using BookingService.Domain.Enums;
using BookingService.Domain.Models;
using BookingService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Infrastructure.Repositories;
public class BookingRepository : BaseRepository<Booking>, IBookingRepository
{
	public BookingRepository(ApplicationDbContext context) : base(context)
	{
	}

	public async Task<IEnumerable<Booking>> GetByCustomerAsync(Guid customerId)
	{
		return await _context.Bookings
			   .Include(b => b.Customer)
			   .Include(b => b.Service)
				   .ThenInclude(s => s.Provider)
			   .Include(b => b.Payment)
			   .Where(b => b.CustomerId == customerId)
			   .AsNoTracking()
			   .OrderByDescending(b => b.BookingDate)
			   .ToListAsync();
	}

	public async Task<Booking> GetByIdWithDetailsAsync(Guid id)
	{
		return await _context.Bookings
				.Include(b => b.Customer)
				.Include(b => b.Service)
					.ThenInclude(s => s.Category)
				.Include(b => b.Service)
					.ThenInclude(s => s.Provider)
				.Include(b => b.Payment)
				.AsNoTracking()
				.FirstOrDefaultAsync(b => b.Id == id);
	}

	public async Task<IEnumerable<Booking>> GetByProviderAsync(Guid providerId)
	{
		return await _context.Bookings
				.Include(b => b.Customer)
				.Include(b => b.Service)
				.Include(b => b.Payment)
				.Where(b => b.Service.ProviderId == providerId)
				.AsNoTracking()
				.OrderByDescending(b => b.BookingDate)
				.ToListAsync();
	}

	public async Task<IEnumerable<Booking>> GetByStatusAsync(BookingStatus status)
	{
		return await _context.Bookings
			   .Include(b => b.Customer)
			   .Include(b => b.Service)
			   .Where(b => b.Status == status)
			   .AsNoTracking()
			   .OrderByDescending(b => b.BookingDate)
			   .ToListAsync();
	}
}
