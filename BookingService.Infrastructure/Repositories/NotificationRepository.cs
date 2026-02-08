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
public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
{
	public NotificationRepository(ApplicationDbContext context) : base(context)
	{
	}

	public async Task<IEnumerable<Notification>> GetByUserIdAsync(Guid userId)
	{
		return await _dbSet
			.Where(n => n.UserId == userId)
			.OrderByDescending(n => n.SentAt)
			.ToListAsync();
	}

	public async Task<int> GetUnreadCountAsync(Guid userId)
	{
		return await _dbSet.CountAsync(n => n.UserId == userId && !n.IsRead);
	}

	public async Task MarkAsReadAsync(Guid id)
	{
		var notification = await _dbSet.FindAsync(id);
		if (notification != null)
		{
			notification.IsRead = true;
			await _context.SaveChangesAsync();
		}
	}

	public async Task MarkAllAsReadAsync(Guid userId)
	{
		var notifications = await _dbSet.Where(n => n.UserId == userId && !n.IsRead).ToListAsync();
		foreach (var notification in notifications)
		{
			notification.IsRead = true;
			
		}
		await _context.SaveChangesAsync();
	}
}
