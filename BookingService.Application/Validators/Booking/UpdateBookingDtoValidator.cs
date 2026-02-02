using BookingService.Application.Dtos.Bookings;
using FluentValidation;

namespace BookingService.Application.Validators.Booking;

public class UpdateBookingDtoValidator : AbstractValidator<UpdateBookingDto>
{
	public UpdateBookingDtoValidator()
	{
		RuleFor(x => x.BookingDate)
			.NotEmpty().WithMessage("تاريخ الحجز مطلوب")
			.GreaterThan(DateTime.UtcNow).WithMessage("تاريخ الحجز يجب أن يكون في المستقبل");

		RuleFor(x => x.Notes)
			.MaximumLength(1000).WithMessage("الملاحظات يجب ألا تزيد عن 1000 حرف");
	}
}
