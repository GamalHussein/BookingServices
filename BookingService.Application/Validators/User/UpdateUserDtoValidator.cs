using BookingService.Application.Dtos.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Validators.User;
public class UpdateUserDtoValidator: AbstractValidator<UpdateUserDto>
{
	public UpdateUserDtoValidator()
	{
		RuleFor(x => x.FirstName)
			.NotEmpty().WithMessage("الاسم الاول مطلوب")
			.MinimumLength(3).WithMessage("الاسم يجب أن يكون 3 أحرف على الأقل")
			.MaximumLength(100).WithMessage("الاسم يجب ألا يزيد عن 100 حرف");
		RuleFor(x => x.LastName)
			.NotEmpty().WithMessage("الاسم الاخير مطلوب")
			.MinimumLength(3).WithMessage("الاسم يجب أن يكون 3 أحرف على الأقل")
			.MaximumLength(100).WithMessage("الاسم يجب ألا يزيد عن 100 حرف");

		RuleFor(x => x.PhoneNumber)
			.NotEmpty().WithMessage("رقم الهاتف مطلوب")
			.Matches(@"^01[0-2,5]{1}[0-9]{8}$").WithMessage("رقم الهاتف غير صحيح");
	}
}
