using BookingService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingService.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
	private readonly INotificationService _notificationService;

	public NotificationController(INotificationService notificationService)
	{
		_notificationService = notificationService;
	}

	[HttpGet]
	public async Task<IActionResult> GetMyNotifications()
	{
		try
		{
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var notifications = await _notificationService.GetMyNotificationsAsync(userId);
			return Ok(new { success = true, data = notifications });
		}
		catch (Exception ex)
		{
			return BadRequest(new { success = false, message = ex.Message });
		}
	}

	[HttpGet("unread-count")]
	public async Task<IActionResult> GetUnreadCount()
	{
		try
		{
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var count = await _notificationService.GetUnreadCountAsync(userId);
			return Ok(new { success = true, count });
		}
		catch (Exception ex)
		{
			return BadRequest(new { success = false, message = ex.Message });
		}
	}

	[HttpPut("{id}/read")]
	public async Task<IActionResult> MarkAsRead(Guid id)
	{
		try
		{
			await _notificationService.MarkAsReadAsync(id);
			return Ok(new { success = true });
		}
		catch (Exception ex)
		{
			return BadRequest(new { success = false, message = ex.Message });
		}
	}

	[HttpPut("read-all")]
	public async Task<IActionResult> MarkAllAsRead()
	{
		try
		{
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			await _notificationService.MarkAllAsReadAsync(userId);
			return Ok(new { success = true });
		}
		catch (Exception ex)
		{
			return BadRequest(new { success = false, message = ex.Message });
		}
	}
}
