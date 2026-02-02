using BookingService.Application.Dtos.Bookings;
using BookingService.Application.Helpers;
using BookingService.Application.Interfaces;
using BookingService.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingService.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BookingController(IBookingService bookingService) : ControllerBase
{
	private readonly IBookingService _bookingService = bookingService;


	[Authorize(Roles = "Admin")]
	[HttpGet]
	public async Task<IActionResult> GetAllAsync()
	{
		try
		{
			var bookings = await _bookingService.GetAllAsync();
			return Ok(new GenralResponse<IEnumerable<BookingDto>>()
			{
				IsSuccess = true,
				Message = "تم جلب جميع الحجوزات بنجاح",
				Data = bookings,
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<IEnumerable<BookingDto>>()
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null,
			});
		}
	}

	[Authorize(Roles = "Customer")]
	[HttpGet("my-bookings")]
	public async Task<IActionResult> GetMyBookingsAsync()
	{
		try
		{
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var bookings = await _bookingService.GetMyBookingsAsync(userId);
			return Ok(new GenralResponse<IEnumerable<BookingDto>>()
			{
				IsSuccess = true,
				Message = "تم جلب حجوزاتك بنجاح",
				Data = bookings,
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<IEnumerable<BookingDto>>()
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null,
			});
		}
	}

	[Authorize(Roles = "ServiceProvider")]
	[HttpGet("provider-bookings")]
	public async Task<IActionResult> GetProviderBookingsAsync()
	{
		try
		{
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var bookings = await _bookingService.GetProviderBookingsAsync(userId);
			return Ok(new GenralResponse<IEnumerable<BookingDto>>()
			{
				IsSuccess = true,
				Message = "تم جلب حجوزاتك بنجاح",
				Data = bookings,
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<IEnumerable<BookingDto>>()
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null,
			});
		}
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetByIdAsync(Guid id)
	{
		try
		{
			var booking = await _bookingService.GetByIdWithDetailsAsync(id);
			return Ok(new GenralResponse<BookingDetailsDto>()
			{
				IsSuccess = true,
				Message = "تم جلب تفاصيل الحجز بنجاح",
				Data = booking,
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<BookingDetailsDto>()
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null,
			});
		}
	}

	[HttpGet("{id}/details")]
	public async Task<IActionResult> GetByIdWithDetailsAsync(Guid id)
	{
		try
		{
			var booking = await _bookingService.GetByIdWithDetailsAsync(id);
			return Ok(new GenralResponse<BookingDetailsDto>()
			{
				IsSuccess = true,
				Message = "تم جلب تفاصيل الحجز بنجاح",
				Data = booking,
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<BookingDetailsDto>()
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null,
			});
		}
	}

	[Authorize(Roles = "Customer")]
	[HttpPost]
	public async Task<IActionResult> CreateAsync([FromBody] CreateBookingDto createDto)
	{
		try
		{
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var booking = await _bookingService.CreateAsync(userId, createDto);
			return Ok(new GenralResponse<BookingDto>()
			{
				IsSuccess = true,
				Message = "تم إنشاء الحجز بنجاح",
				Data = booking,
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<BookingDto>()
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null,
			});
		}
	}

	[Authorize(Roles = "Customer")]
	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateBookingDto updateDto)
	{
		try
		{
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var result = await _bookingService.UpdateAsync(id, userId, updateDto);
			if (result)
			{
				return Ok(new GenralResponse<bool>()
				{
					IsSuccess = true,
					Message = "تم تحديث الحجز بنجاح",
					Data = true,
				});
			}
			else
			{
				return NotFound(new GenralResponse<bool>()
				{
					IsSuccess = false,
					Message = "الحجز غير موجود",
					Data = false,
				});
			}
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<bool>()
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = false,
			});
		}
	}

	[HttpPost("{id}/cancel")]
	public async Task<IActionResult> CancelAsync(Guid id, [FromBody] CancelBookingDto cancelDto)
	{
		try
		{
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var result = await _bookingService.CancelAsync(id, userId, cancelDto);
			if (result)
			{
				return Ok(new GenralResponse<bool>()
				{
					IsSuccess = true,
					Message = "تم إلغاء الحجز بنجاح",
					Data = true,
				});
			}
			else
			{
				return NotFound(new GenralResponse<bool>()
				{
					IsSuccess = false,
					Message = "الحجز غير موجود",
					Data = false,
				});
			}
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<bool>()
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = false,
			});
		}
	}

	[Authorize(Roles = "ServiceProvider")]
	[HttpPost("{id}/confirm")]
	public async Task<IActionResult> ConfirmAsync(Guid id)
	{
		try
		{
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var result = await _bookingService.ConfirmAsync(id, userId);
			if (result)
			{
				return Ok(new GenralResponse<bool>()
				{
					IsSuccess = true,
					Message = "تم تأكيد الحجز بنجاح",
					Data = true,
				});
			}
			else
			{
				return NotFound(new GenralResponse<bool>()
				{
					IsSuccess = false,
					Message = "الحجز غير موجود",
					Data = false,
				});
			}
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<bool>()
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = false,
			});
		}
	}

	[Authorize(Roles = "ServiceProvider")]
	[HttpPost("{id}/complete")]
	public async Task<IActionResult> CompleteAsync(Guid id)
	{
		try
		{
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var result = await _bookingService.CompleteAsync(id, userId);
			if (result)
			{
				return Ok(new GenralResponse<bool>()
				{
					IsSuccess = true,
					Message = "تم إكمال الحجز بنجاح",
					Data = true,
				});
			}
			else
			{
				return NotFound(new GenralResponse<bool>()
				{
					IsSuccess = false,
					Message = "الحجز غير موجود",
					Data = false,
				});
			}
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<bool>()
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = false,
			});
		}
	}
}
