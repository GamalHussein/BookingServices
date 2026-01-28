using BookingService.Application.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces;
public interface IAuthService
{
	Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
	Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
	Task<UserDto> GetProfileAsync(Guid userId);
	Task<bool> UpdateProfileAsync(Guid userId, UpdateUserDto updateDto);
	Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordDto changePasswordDto);
}

