using BookingService.Application.Dtos.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Validators.Services;
public class UpdateServiceDtoValidator: AbstractValidator<UpdateServiceDto>
{
	public UpdateServiceDtoValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("اسم الخدمة مطلوب")
			.MinimumLength(3).WithMessage("اسم الخدمة يجب أن يكون 3 أحرف على الأقل")
			.MaximumLength(200).WithMessage("اسم الخدمة يجب ألا يزيد عن 200 حرف");

		RuleFor(x => x.Description)
			.MaximumLength(1000).WithMessage("الوصف يجب ألا يزيد عن 1000 حرف");

		RuleFor(x => x.Price)
			.NotEmpty().WithMessage("السعر مطلوب")
			.GreaterThan(0).WithMessage("السعر يجب أن يكون أكبر من صفر")
			.LessThanOrEqualTo(100000).WithMessage("السعر يجب ألا يزيد عن 100,000");

		RuleFor(x => x.DurationMinutes)
			.NotEmpty().WithMessage("مدة الخدمة مطلوبة")
			.GreaterThan(0).WithMessage("مدة الخدمة يجب أن تكون أكبر من صفر")
			.LessThanOrEqualTo(1440).WithMessage("مدة الخدمة يجب ألا تزيد عن 24 ساعة");

		RuleFor(x => x.CategoryId)
			.NotEmpty().WithMessage("الفئة مطلوبة");
	}
}
