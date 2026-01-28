using BookingService.Application.Dtos.Category;
using BookingService.Application.Interfaces;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Services;
public class CategoryService(ICategoryRepository categoryRepository , IMapper mapper) : ICategoryService
{
	private readonly ICategoryRepository CategoryRepository = categoryRepository;
	private readonly IMapper Mapper = mapper;

	public async Task<CategoryDto> CreateAsync(CreateCategoryDto createDto)
	{
		var IsExist = await CategoryRepository.ExistsByNameAsync(createDto.Name);
		if (IsExist)
		{
			throw new Exception("الفئة موجودة بالفعل");
		}
		var category = Mapper.Map<Domain.Models.Category>(createDto);
		var result = CategoryRepository.CreateAsync(category);
		return await Mapper.Map<Task<CategoryDto>>(result);
	}

	public async Task<bool> DeleteAsync(Guid id)
	{
		var category =await CategoryRepository.GetByIdAsync(id);
		if (category == null)
		{
			throw new Exception("الفئة غير موجودة");
		}
		return await CategoryRepository.DeleteAsync(id);

	}

	public async Task<IEnumerable<CategoryDto>> GetAllActiveAsync()
	{
		var categories = await CategoryRepository.GetAllActiveAsync();
		return await Mapper.Map<Task<IEnumerable<CategoryDto>>>(categories);
	}

	public async Task<IEnumerable<CategoryDto>> GetAllAsync()
	{
		var categories = await CategoryRepository.GetAllAsync();
		return await Mapper.Map<Task<IEnumerable<CategoryDto>>>(categories);
	}

	public async Task<CategoryDto> GetByIdAsync(Guid id)
	{
		var category = await CategoryRepository.GetByIdAsync(id);
		if (category == null)
		{
			throw new Exception("الفئة غير موجودة");
		}
		return  Mapper.Map<CategoryDto>(category);
	}

	public async Task<CategoryWithServicesDto> GetByIdWithServicesAsync(Guid id)
	{
		var category = await CategoryRepository.GetByIdWithServicesAsync(id);
		if (category == null)
		{
			throw new Exception("الفئة غير موجودة");
		}
		return Mapper.Map<CategoryWithServicesDto>(category);
	}

	public async Task<bool> UpdateAsync(Guid id, UpdateCategoryDto updateDto)
	{
		var category = await CategoryRepository.GetByIdAsync(id);
		if (category == null)
		{
			throw new Exception("الفئة غير موجودة");
		}
		if (category.Name.ToLower() != updateDto.Name.ToLower())
		{
			var nameExists = await CategoryRepository.ExistsByNameAsync(updateDto.Name);
			if (nameExists)
			{
				throw new Exception("اسم الفئة موجود بالفعل");
			}
		}
		Mapper.Map(updateDto, category);
		return await CategoryRepository.UpdateAsync(category);
	}
}
