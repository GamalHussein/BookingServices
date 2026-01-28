using BookingService.Application.Dtos.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Validators.User;
public class RegisterDtoValidator:AbstractValidator<RegisterDto>
{
	public RegisterDtoValidator()
	{
		RuleFor(x => x.FirstName)
			.NotEmpty().WithMessage("الاسم الاول مطلوب")
			.MinimumLength(3).WithMessage("الاسم يجب أن يكون 3 أحرف على الأقل")
			.MaximumLength(100).WithMessage("الاسم يجب ألا يزيد عن 100 حرف");
		RuleFor(x => x.LastName)
			.NotEmpty().WithMessage("الاسم الاخير مطلوب")
			.MinimumLength(3).WithMessage("الاسم يجب أن يكون 3 أحرف على الأقل")
			.MaximumLength(100).WithMessage("الاسم يجب ألا يزيد عن 100 حرف");

		RuleFor(x => x.Email)
			.NotEmpty().WithMessage("البريد الإلكتروني مطلوب")
			.EmailAddress().WithMessage("البريد الإلكتروني غير صحيح");

		RuleFor(x => x.PhoneNumber)
			.NotEmpty().WithMessage("رقم الهاتف مطلوب")
			.Matches(@"^01[0-2,5]{1}[0-9]{8}$").WithMessage("رقم الهاتف غير صحيح");

		RuleFor(x => x.Password)
			.NotEmpty().WithMessage("كلمة المرور مطلوبة")
			.MinimumLength(6).WithMessage("كلمة المرور يجب أن تكون 6 أحرف على الأقل")
			.Matches(@"[A-Z]").WithMessage("كلمة المرور يجب أن تحتوي على حرف كبير")
			.Matches(@"[a-z]").WithMessage("كلمة المرور يجب أن تحتوي على حرف صغير")
			.Matches(@"[0-9]").WithMessage("كلمة المرور يجب أن تحتوي على رقم");

		RuleFor(x => x.ConfirmPassword)
			.Equal(x => x.Password).WithMessage("كلمة المرور غير متطابقة");

		RuleFor(x => x.UserType)
				.InclusiveBetween(1, 2).WithMessage("نوع المستخدم غير صحيح");

	}
}
