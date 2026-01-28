using BookingService.Application.Dtos.Auth;
using BookingService.Application.Helpers;
using BookingService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingService.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService _authService) : ControllerBase
{
	private readonly IAuthService authService= _authService;

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterDto request) 
	{
		try
		{
			var result = await authService.RegisterAsync(request);
			return Ok(new GenralResponse<AuthResponseDto>
			{
				IsSuccess = true,
				Message = "تم التسجيل بنجاح",
				Data = result
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<AuthResponseDto>
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null
			});
		}

	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginDto request)
	{
		try
		{
			var result = await authService.LoginAsync(request);
			return Ok(new GenralResponse<AuthResponseDto>
			{
				IsSuccess = true,
				Message = "تم تسجيل الدخول بنجاح",
				Data = result
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<AuthResponseDto>
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null
			});
		}
	}
	[Authorize]
	[HttpGet("profile")]
	public async Task<IActionResult> GetProfile()
	{
		try
		{
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var result = await _authService.GetProfileAsync(userId);
			return Ok(new GenralResponse<UserDto>
			{
				IsSuccess = true,
				Message = "تم جلب الملف الشخصي بنجاح",
				Data = result
			});
			
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<UserDto>
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null
			});
		}
	}

	[Authorize]
	[HttpPut("profile")]
	public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserDto request)
	{
		try
		{
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var result = await _authService.UpdateProfileAsync(userId, request);
			return Ok(new GenralResponse<bool>
			{
				IsSuccess = result,
				Message =  "تم تحديث الملف الشخصي بنجاح",
				Data = result
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<bool>
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = false
			});
		}
	}

	[Authorize]
	[HttpPut("change-password")]
	public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto request)
	{
		try
		{
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var result = await _authService.ChangePasswordAsync(userId, request);
			return Ok(new GenralResponse<bool>
			{
				IsSuccess = result,
				Message = "تم تغيير كلمة المرور بنجاح",
				Data = result
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<bool>
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = false
			});
		}
	}


}
