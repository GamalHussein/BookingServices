using BookingService.Application.Dtos.Services;
using BookingService.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Mapping;
public class ServiceMappingConfigration : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		// CreateServiceDto -> Service
		config.NewConfig<CreateServiceDto, Service>()
			.Ignore(e => e.Id)
			.Ignore(e => e.CreatedAt)
			.Ignore(e => e.UpdatedAt)
			.Ignore(e => e.Category)
			.Ignore(e => e.Provider)
			.Ignore(e => e.Bookings);

		// UpdateServiceDto -> Service
		config.NewConfig<UpdateServiceDto, Service>()
			.Ignore(e => e.Id)
			.Ignore(e => e.CreatedAt)
			.Map(e => e.UpdatedAt, c => DateTime.UtcNow)
			.Ignore(e=>e.ProviderId)
			.Ignore(e => e.Category)
			.Ignore(e => e.Provider)
			.Ignore(e => e.Bookings);
		// Service -> ServiceDetailsDto
		config.NewConfig<Service, ServiceDetailsDto>()
			.Map(dest => dest.Category, src => src.Category)
			.Map(dest => dest.Provider, src => new ProviderDto
			{
				Id = src.Provider.Id,
				FullName =$"{ src.Provider.FirstName} {src.Provider.LastName}",
				Email = src.Provider.Email,
				PhoneNumber = src.Provider.PhoneNumber
			});
		// Service -> ServiceDto
		config.NewConfig<Service, ServiceDto>()
			.Map(dest => dest.CategoryName, src => src.Category.Name)
			.Map(dest => dest.ProviderName, src => $"{src.Provider.FirstName} {src.Provider.LastName}");
	}
}
