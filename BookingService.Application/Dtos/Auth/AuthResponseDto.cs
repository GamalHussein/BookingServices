using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Dtos.Auth;
public class AuthResponseDto
{
	public Guid UserId { get; set; }
	public string FirstName { get; set; }= string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;
	public string UserType { get; set; }
	public string Token { get; set; } = string.Empty;
	public DateTime TokenExpiration { get; set; }
}
