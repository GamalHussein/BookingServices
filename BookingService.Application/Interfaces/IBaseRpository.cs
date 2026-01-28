using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces;
public interface IBaseRpository<T> where T : class
{
	Task<T> GetByIdAsync(Guid id);
	Task<IEnumerable<T>> GetAllAsync();
	Task<T> CreateAsync(T entity);
	Task<bool> UpdateAsync(T entity);
	Task<bool> DeleteAsync(Guid id);
	Task<bool> ExistsByIdAsync(Guid id);
}
