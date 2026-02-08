using BookingService.Application.Dtos.Notification;
using BookingService.Application.Interfaces;
using BookingService.Domain.Enums;
using BookingService.Domain.Models;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Services;
public class NotificationService : INotificationService
{
	private readonly INotificationRepository _notificationRepository;
	private readonly IMapper _mapper;

	public NotificationService(INotificationRepository notificationRepository, IMapper mapper)
	{
		_notificationRepository = notificationRepository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<NotificationDto>> GetMyNotificationsAsync(Guid userId)
	{
		var notifications = await _notificationRepository.GetByUserIdAsync(userId);
		return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
	}

	public async Task<int> GetUnreadCountAsync(Guid userId)
	{
		return await _notificationRepository.GetUnreadCountAsync(userId);
	}

	public async Task MarkAsReadAsync(Guid id)
	{
		await _notificationRepository.MarkAsReadAsync(id);
	}

	public async Task MarkAllAsReadAsync(Guid userId)
	{
		await _notificationRepository.MarkAllAsReadAsync(userId);
	}

	// Helper: إرسال إشعار
	public async Task SendAsync(Guid userId, string title, string message)
	{
		var notification = new Notification
		{
			UserId = userId,
			Title = title,
			Message = message,
			Type = NotificationType.General,
			IsRead = false
		};

		await _notificationRepository.CreateAsync(notification);
	}
}
