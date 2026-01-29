using BookingService.Application.Dtos.Services;
using BookingService.Application.Interfaces;
using BookingService.Domain.Models;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Services;
public class ServiceService(
	IServiceRepository serviceRepository,
	IMapper mapper,
	ICategoryRepository categoryRepository,
	IUserRepository userRepository
	) : IServiceService
{
	private readonly IServiceRepository ServiceRepository = serviceRepository;
	private readonly IMapper Mapper = mapper;
	private readonly ICategoryRepository CategoryRepository = categoryRepository;
	private readonly IUserRepository UserRepository = userRepository;

	public async Task<ServiceDto> CreateAsync(CreateServiceDto createDto)
	{
		var categoryExists = await CategoryRepository.ExistsByIdAsync(createDto.CategoryId);
		if (!categoryExists)
		{
			throw new Exception("الفئة غير موجودة");
		}
		var providerExists = await UserRepository.GetByIdAsync(createDto.ProviderId);
		if (providerExists is null)
		{
			throw new Exception("مزود الخدمة غير موجود");
		}
		var service = Mapper.Map<Service>(createDto);
		var createdService = await ServiceRepository.CreateAsync(service);
		var serviceDto = Mapper.Map<ServiceDto>(createdService);
		return serviceDto;

	}

	public async Task<bool> DeleteAsync(Guid id)
	{
		var serviceExists = await ServiceRepository.ExistsByIdAsync(id);
		if (!serviceExists)
		{
			return false;
		}
		 return await ServiceRepository.DeleteAsync(id);
	

	}

	public async Task<IEnumerable<ServiceDto>> GetAllActiveAsync()
	{
		var services = await ServiceRepository.GetAllActiveAsync();
		var serviceDtos = Mapper.Map<IEnumerable<ServiceDto>>(services);
		return serviceDtos;

	}

	public async Task<IEnumerable<ServiceDto>> GetAllAsync()
	{
		var services = await ServiceRepository.GetAllAsync();
		var serviceDtos = Mapper.Map<IEnumerable<ServiceDto>>(services);
		return serviceDtos;
	}

	public async Task<IEnumerable<ServiceDto>> GetByCategoryAsync(Guid categoryId)
	{
		var services = await ServiceRepository.GetByCategoryAsync(categoryId);
		if (services is null)
		{
			throw new Exception("الفئة غير موجودة");
		}
		var serviceDtos = Mapper.Map<IEnumerable<ServiceDto>>(services);
		return serviceDtos;
	}

	public async Task<ServiceDto> GetByIdAsync(Guid id)
	{
		var service = await ServiceRepository.GetByIdAsync(id);
		if (service is null)
		{
			throw new Exception("الفئة غير موجودة");
		}
		var serviceDto = Mapper.Map<ServiceDto>(service);
		return serviceDto;
	}

	public async Task<ServiceDetailsDto> GetByIdWithDetailsAsync(Guid id)
	{
		var service =await ServiceRepository.GetByIdWithDetailsAsync(id);
		if (service is null)
		{
			throw new Exception("الفئة غير موجودة");
		}
		var serviceDto = Mapper.Map<ServiceDetailsDto>(service);
		return serviceDto;
	}

	public async Task<IEnumerable<ServiceDto>> GetByProviderAsync(Guid providerId)
	{
		var providerExists = await UserRepository.GetByIdAsync(providerId);
		if (providerExists is null)
		{
			throw new Exception("مزود الخدمة غير موجود");
		}
		var services = await ServiceRepository.GetByProviderAsync(providerId);
		var serviceDtos = Mapper.Map<IEnumerable<ServiceDto>>(services);
		return serviceDtos;
	}

	public async Task<bool> UpdateAsync(Guid id, UpdateServiceDto updateDto)
	{
		var categoryExists = await CategoryRepository.ExistsByIdAsync(updateDto.CategoryId);
		if (categoryExists == null)
		{
			throw new Exception("الفئة غير موجودة");
		}
		var existingService = await ServiceRepository.GetByIdAsync(id);
		if (existingService == null)
		{
			throw new Exception("الخدمة غير موجودة");
		}
		Mapper.Map(updateDto, existingService);
		return await ServiceRepository.UpdateAsync(existingService);

	}
}
