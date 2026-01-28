using BookingService.Application.Dtos.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Validators.User;
public class LoginDtoValidator:AbstractValidator<LoginDto>
{
	public LoginDtoValidator()
	{
		RuleFor(x => x.Email)
			.NotEmpty().WithMessage("البريد الإلكتروني مطلوب")
			.EmailAddress().WithMessage("البريد الإلكتروني غير صحيح");

		RuleFor(x => x.Password)
			.NotEmpty().WithMessage("كلمة المرور مطلوبة");
	}
}
