using BookingService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.Models;
public class Notification
{
	#region fields
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string Message { get; set; }
	public NotificationType Type { get; set; }
	public DateTime SentAt { get; set; } = DateTime.UtcNow;
	public bool IsRead { get; set; } = false;
	#endregion

	#region Navigation Properties
	public Guid UserId { get; set; }
	public ApplicationUser User { get; set; }
	#endregion
}
