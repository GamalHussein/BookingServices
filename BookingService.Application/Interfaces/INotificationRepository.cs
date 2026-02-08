using BookingService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces;
public interface INotificationRepository : IBaseRpository<Notification>
{
	Task<IEnumerable<Notification>> GetByUserIdAsync(Guid userId);
	Task<int> GetUnreadCountAsync(Guid userId);
	Task MarkAsReadAsync(Guid id);
	Task MarkAllAsReadAsync(Guid userId);
}