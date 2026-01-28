using BookingService.Application.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces;
public interface ICategoryService
{
	Task<CategoryDto> GetByIdAsync(Guid id);
	Task<CategoryWithServicesDto> GetByIdWithServicesAsync(Guid id);
	Task<IEnumerable<CategoryDto>> GetAllAsync();
	Task<IEnumerable<CategoryDto>> GetAllActiveAsync();
	Task<CategoryDto> CreateAsync(CreateCategoryDto createDto);
	Task<bool> UpdateAsync(Guid id, UpdateCategoryDto updateDto);
	Task<bool> DeleteAsync(Guid id);
}
