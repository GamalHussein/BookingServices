using BookingService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces;
public interface IServiceRepository
{
	Task<Service> GetByIdWithDetailsAsync(Guid id);
	Task<IEnumerable<Service>> GetAllActiveAsync();
	Task<IEnumerable<Service>> GetByCategoryAsync(Guid categoryId);
	Task<IEnumerable<Service>> GetByProviderAsync(Guid providerId);
}
