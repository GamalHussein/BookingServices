using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Dtos.Auth;
public class LoginDto
{
	public String Email { get; set; }
	public String Password { get; set; }
}
