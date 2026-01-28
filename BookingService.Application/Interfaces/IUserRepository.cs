using BookingService.Domain.Enums;
using BookingService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces;
public interface IUserRepository
{
	Task<ApplicationUser> GetByIdAsync(Guid id);
	Task<ApplicationUser> GetByEmailAsync(string email);
	Task<IEnumerable<ApplicationUser>> GetAllAsync();
	Task<IEnumerable<ApplicationUser>> GetByUserTypeAsync(UserType userType);
	Task<bool> UpdateAsync(ApplicationUser user);
	Task<bool> DeleteAsync(Guid id);
}
