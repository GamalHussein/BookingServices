using BookingService.Application.Dtos.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Validators.User;
public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
{
	public ChangePasswordDtoValidator()
	{
		RuleFor(x => x.CurrentPassword)
			.NotEmpty().WithMessage("كلمة المرور الحالية مطلوبة");

		RuleFor(x => x.NewPassword)
			.NotEmpty().WithMessage("كلمة المرور الجديدة مطلوبة")
			.MinimumLength(6).WithMessage("كلمة المرور يجب أن تكون 6 أحرف على الأقل")
			.Matches(@"[A-Z]").WithMessage("كلمة المرور يجب أن تحتوي على حرف كبير")
			.Matches(@"[a-z]").WithMessage("كلمة المرور يجب أن تحتوي على حرف صغير")
			.Matches(@"[0-9]").WithMessage("كلمة المرور يجب أن تحتوي على رقم");

		RuleFor(x => x.ConfirmPassword)
			.Equal(x => x.NewPassword).WithMessage("كلمة المرور غير متطابقة");
	}
}
