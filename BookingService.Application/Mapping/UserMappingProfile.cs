using BookingService.Application.Dtos.Auth;
using BookingService.Domain.Enums;
using BookingService.Domain.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Mapping;
public class UserMappingProfile : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<RegisterDto,ApplicationUser>()
			.Map(dest => dest.UserName, src => src.Email)
			.Map(dest => dest.UserType, src => (UserType)src.UserType);

		config.NewConfig<ApplicationUser, UserDto>()
			.Map(dest => dest.UserType, src => src.UserType.ToString());

		config.NewConfig<ApplicationUser, AuthResponseDto>()
			.Map(dest=>dest.UserId,src=>src.Id)
			.Map(dest => dest.UserType, src => src.UserType.ToString());

		config.NewConfig<UpdateUserDto, ApplicationUser>()
			.Ignore(dest => dest.Id)
			.Ignore(dest => dest.Email)
			.Ignore(dest => dest.UserType);
	}
}
