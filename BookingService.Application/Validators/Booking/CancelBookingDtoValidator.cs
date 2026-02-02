using BookingService.Application.Dtos.Bookings;
using FluentValidation;

namespace BookingService.Application.Validators.Booking;

public class CancelBookingDtoValidator : AbstractValidator<CancelBookingDto>
{
	public CancelBookingDtoValidator()
	{
		RuleFor(x => x.CancellationReason)
			.NotEmpty().WithMessage("سبب الإلغاء مطلوب")
			.MinimumLength(5).WithMessage("سبب الإلغاء يجب أن يكون 5 أحرف على الأقل")
			.MaximumLength(500).WithMessage("سبب الإلغاء يجب ألا يزيد عن 500 حرف");
	}
}
