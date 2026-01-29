using BookingService.Application.Dtos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces;
public interface IServiceService
{
	Task<ServiceDto> GetByIdAsync(Guid id);
	Task<ServiceDetailsDto> GetByIdWithDetailsAsync(Guid id);
	Task<IEnumerable<ServiceDto>> GetAllAsync();
	Task<IEnumerable<ServiceDto>> GetAllActiveAsync();
	Task<IEnumerable<ServiceDto>> GetByCategoryAsync(Guid categoryId);
	Task<IEnumerable<ServiceDto>> GetByProviderAsync(Guid providerId);
	Task<ServiceDto> CreateAsync(CreateServiceDto createDto);
	Task<bool> UpdateAsync(Guid id, UpdateServiceDto updateDto);
	Task<bool> DeleteAsync(Guid id);
}
