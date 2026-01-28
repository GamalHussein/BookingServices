using BookingService.Application.Dtos.Category;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Validators.Category;
public class UpdateCategoryValidator: AbstractValidator<UpdateCategoryDto>
{
	public UpdateCategoryValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("اسم الفئة مطلوب")
			.MinimumLength(2).WithMessage("اسم الفئة يجب أن يكون حرفين على الأقل")
			.MaximumLength(100).WithMessage("اسم الفئة يجب ألا يزيد عن 100 حرف");
		RuleFor(x => x.Description)
			.MaximumLength(500).WithMessage("الوصف يجب ألا يزيد عن 500 حرف");
	}
}
