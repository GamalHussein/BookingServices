using BookingService.Application.Dtos.Category;
using BookingService.Domain.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Mapping;
public class CategoryMappingProfile : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<CreateCategoryDto, Category>()
			.Ignore(e => e.CreatedAt)
			.Ignore(e => e.UpdatedAt)
			.Ignore(e => e.Id);
			//.Ignore(e=>e.Services);

		config.NewConfig<UpdateCategoryDto, Category>()
			.Ignore(e => e.CreatedAt)
			.Map(e => e.UpdatedAt,c=>DateTime.UtcNow)
			.Ignore(e => e.Id);
		//.Ignore(e=>e.Services);

		config.NewConfig<Category, CategoryWithServicesDto>()
			//.Map(c=>c.Services,d=>d.Services)
			.Map(des=>des.ServicesCount,src=>src.Services.Count);
	}
}
