using BookingService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces;
public interface ICategoryRepository: IBaseRpository<Category>
{
	Task<Category> GetByIdWithServicesAsync(Guid id);
	Task<IEnumerable<Category>> GetAllActiveAsync();
	Task<bool> ExistsByNameAsync(string name);
}
