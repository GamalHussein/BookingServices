using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Dtos.Auth;
public class RegisterDto
{
	public String FirstName { get; set; } = String.Empty;
	public String LastName { get; set; } = String.Empty;
	public String Email { get; set; } = String.Empty;
	public String PhoneNumber { get; set; } = String.Empty;
	public String Password { get; set; } = String.Empty;
	public String ConfirmPassword { get; set; } = String.Empty;
	public int UserType { get; set; }
}
