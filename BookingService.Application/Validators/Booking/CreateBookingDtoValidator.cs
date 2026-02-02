using BookingService.Application.Dtos.Bookings;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Validators.Booking;
public class CreateBookingDtoValidator : AbstractValidator<CreateBookingDto>
{
	public CreateBookingDtoValidator()
	{
		RuleFor(x => x.ServiceId)
			.NotEmpty().WithMessage("الخدمة مطلوبة");

		RuleFor(x => x.BookingDate)
			.NotEmpty().WithMessage("تاريخ الحجز مطلوب")
			.GreaterThan(DateTime.UtcNow).WithMessage("تاريخ الحجز يجب أن يكون في المستقبل");

		RuleFor(x => x.Notes)
			.MaximumLength(1000).WithMessage("الملاحظات يجب ألا تزيد عن 1000 حرف");
	}
}
