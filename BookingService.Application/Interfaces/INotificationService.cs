using BookingService.Application.Dtos.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces;
public interface INotificationService
{
	Task<IEnumerable<NotificationDto>> GetMyNotificationsAsync(Guid userId);
	Task<int> GetUnreadCountAsync(Guid userId);
	Task MarkAsReadAsync(Guid id);
	Task MarkAllAsReadAsync(Guid userId);

	// Helper methods
	Task SendAsync(Guid userId, string title, string message);
}