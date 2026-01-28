using BookingService.Application.Interfaces;
using BookingService.Domain.Enums;
using BookingService.Domain.Models;
using BookingService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure.Repositories;
public class UserRepository(ApplicationDbContext _context) : IUserRepository
{
	private readonly ApplicationDbContext context = _context;


	public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
	{
		return await context.Users
			.OrderByDescending(u => u.CreatedAt)
			.ToListAsync();
	}
	public async Task<ApplicationUser> GetByEmailAsync(string email)
	{
		return await context.Users
			.FirstOrDefaultAsync(u => u.Email == email);
	}
	public async Task<bool> DeleteAsync(Guid id)
	{
		var user = context.Users.Find(id);
		if (user == null)
		{
			return false;
		}

		context.Users.Remove(user);
		return await context.SaveChangesAsync() > 0;

	}

	
	public async Task<ApplicationUser> GetByIdAsync(Guid id)
	{
		return await context.Users
			.FirstOrDefaultAsync(u => u.Id == id);
	}

	public async Task<IEnumerable<ApplicationUser>> GetByUserTypeAsync(UserType userType)
	{
		return await context.Users
			.Where(u => u.UserType == userType)
			.OrderByDescending(u => u.CreatedAt)
			.ToListAsync();
	}

	public async Task<bool> UpdateAsync(ApplicationUser user)
	{
		_context.Users.Update(user);
		return await _context.SaveChangesAsync() > 0;
	}
}
