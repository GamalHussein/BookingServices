using BookingService.Api.Helpers;
using BookingService.Application.Dtos.Auth;
using BookingService.Application.Interfaces;
using BookingService.Domain.Enums;
using BookingService.Domain.Models;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Services;
public class AuthService(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			IUserRepository userRepository,
			IMapper mapper,
			JwtHelper jwtHelper) : IAuthService
{
	private readonly UserManager<ApplicationUser> UserManager = userManager;
	private readonly SignInManager<ApplicationUser> SignInManager = signInManager;
	private readonly IUserRepository UserRepository = userRepository;
	private readonly IMapper Mapper = mapper;
	private readonly JwtHelper JwtHelper = jwtHelper;

	public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
	{
		var Exist=await UserManager.FindByEmailAsync(registerDto.Email);
		if (Exist != null)
		{
			throw new Exception("البريد الإلكتروني مستخدم بالفعل");
		}

		var user=Mapper.Map<ApplicationUser>(registerDto);

		var result = await UserManager.CreateAsync(user, registerDto.Password);
		if (!result.Succeeded)
		{
			var errors = string.Join(", ", result.Errors.Select(e => e.Description));
			throw new Exception($"فشل في إنشاء المستخدم: {errors}");
		}
		
		var roleName=registerDto.UserType==1 ? "Customer" : "ServiceProvider";
		await UserManager.AddToRoleAsync(user, roleName);

		var token = JwtHelper.GenerateToken(user);
		var expiration = JwtHelper.GetTokenExpiration();
		var response = mapper.Map<AuthResponseDto>(user);
		response.Token = token;
		response.TokenExpiration = expiration;
		return response;
	}
	public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
	{

		if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
		{
			throw new Exception("البريد الإلكتروني وكلمة المرور مطلوبان");
		}

		var user =await UserManager.FindByEmailAsync(loginDto.Email);
		if (user == null)
		{
			throw new Exception("المستخدم غير موجود");
		}
		var result = await SignInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
		if (!result.Succeeded)
		{
			throw new Exception("كلمة المرور غير صحيحة");
		}

		var token = JwtHelper.GenerateToken(user);
		var expiration = JwtHelper.GetTokenExpiration();
		var response = Mapper.Map<AuthResponseDto>(user);
		response.Token = token;
		response.TokenExpiration = expiration;

		return response;

	}

	public async Task<UserDto> GetProfileAsync(Guid userId)
	{
		var user = await UserRepository.GetByIdAsync(userId);
		if (user == null)
		{
			throw new Exception("المستخدم غير موجود");
		}
		var userDto = Mapper.Map<UserDto>(user);
		return userDto;
	}
	public async Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordDto changePasswordDto)
	{
		var user = await UserRepository.GetByIdAsync(userId);

		if (user == null)
			throw new Exception("المستخدم غير موجود");


		var result = UserManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword).Result;

		if (!result.Succeeded)
		{
			var errors = string.Join(", ", result.Errors.Select(e => e.Description));
			throw new Exception($"فشل في تغيير كلمة المرور: {errors}");
		}
		return true;



	}

	public async Task<bool> UpdateProfileAsync(Guid userId, UpdateUserDto updateDto)
	{
		var user =await UserRepository.GetByIdAsync(userId);
		if (user == null)
			throw new Exception("المستخدم غير موجود");

		Mapper.Map(updateDto, user);
		return await UserRepository.UpdateAsync(user);

	}
}
