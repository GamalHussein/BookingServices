using BookingService.Application.Interfaces;
using BookingService.Domain.Models;
using BookingService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Infrastructure.Repositories;
public class ServiceRepository : BaseRepository<Service>, IServiceRepository
{
	public ServiceRepository(ApplicationDbContext context) : base(context)
	{
	}

	public async Task<IEnumerable<Service>> GetAllActiveAsync()
	{
		return await _dbSet
			.AsNoTracking()
			.Include(s => s.Category)
			.Include(s => s.Provider)
			.Where(s => s.IsActive)
			.ToListAsync();
	}

	public async Task<IEnumerable<Service>> GetByCategoryAsync(Guid categoryId)
	{
		return await _dbSet
			.AsNoTracking()
			.Include(s => s.Category)
			.Include(s => s.Provider)
			.Where(s => s.CategoryId == categoryId)
			.ToListAsync();
	}

	public async Task<Service> GetByIdWithDetailsAsync(Guid id)
	{
		return await _dbSet
				.Include(s => s.Category)
				.Include(s => s.Provider)
				.Include(s => s.Bookings)
				.AsNoTracking()
				.FirstOrDefaultAsync(s => s.Id == id);
	}

	public async Task<IEnumerable<Service>> GetByProviderAsync(Guid providerId)
	{
		return await _dbSet
				.Include(s => s.Category)
				.Include(s => s.Provider)
				.Where(s => s.ProviderId == providerId)
				.AsNoTracking()
				.OrderByDescending(s => s.CreatedAt)
				.ToListAsync();
	}

	public override async Task<Service> GetByIdAsync(Guid id)
	{
		return await _dbSet
			.Include(s => s.Category)
			.Include(s => s.Provider)
			.AsNoTracking()
			.FirstOrDefaultAsync(s => s.Id == id);
	}

	public override async Task<IEnumerable<Service>> GetAllAsync()
	{
		return await _dbSet
			.Include(s => s.Category)
			.Include(s => s.Provider)
			.AsNoTracking()
			.OrderByDescending(s => s.CreatedAt)
			.ToListAsync();
	}

	public override async Task<Service> CreateAsync(Service service)
	{
		var created = await base.CreateAsync(service);
		return await GetByIdAsync(created.Id);
	}
}
