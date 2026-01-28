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
public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
	public CategoryRepository(ApplicationDbContext context) : base(context)
	{
	}

	public async Task<bool> ExistsByNameAsync(string name)
	{
		var exists = await _dbSet
			.AsNoTracking()
			.AnyAsync(c => c.Name.ToLower() == name.ToLower());
		return exists;

	}

	public async Task<IEnumerable<Category>> GetAllActiveAsync()
	{
		return await _dbSet
			.AsNoTracking()
			.Where(c => c.IsActive).ToListAsync();
	}

	public async Task<Category> GetByIdWithServicesAsync(Guid id)
	{
		return await _dbSet
			.AsNoTracking()
			.Include(c => c.Services)
			.FirstOrDefaultAsync(c => c.Id == id);
	}
}
