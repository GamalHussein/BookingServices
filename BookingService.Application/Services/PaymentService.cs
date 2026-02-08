using BookingService.Application.Dtos.Payment;
using BookingService.Application.Interfaces;
using BookingService.Domain.Enums;
using BookingService.Domain.Models;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Services;
public class PaymentService : IPaymentService
{
	private readonly IPaymentRepository _paymentRepository;
	private readonly IBookingRepository _bookingRepository;
	private readonly StripeService _stripeService;
	private readonly INotificationService _notificationService;
	private readonly IMapper _mapper;

	public PaymentService(
		IPaymentRepository paymentRepository,
		IBookingRepository bookingRepository,
		StripeService stripeService,
		INotificationService notificationService,
		IMapper mapper)
	{
		_paymentRepository = paymentRepository;
		_bookingRepository = bookingRepository;
		_stripeService = stripeService;
		_notificationService = notificationService;
		_mapper = mapper;
	}

	public async Task<StripePaymentResponse> CreatePaymentAsync(Guid customerId, CreatePaymentDto dto)
	{
		// Get booking
		var booking = await _bookingRepository.GetByIdAsync(dto.BookingId);
		if (booking == null) throw new Exception("الحجز غير موجود");
		if (booking.CustomerId != customerId) throw new Exception("غير مصرح لك");

		// Check if payment exists
		var existingPayment = await _paymentRepository.GetByBookingIdAsync(dto.BookingId);
		if (existingPayment != null) throw new Exception("يوجد دفع مسبق");

		// Create Stripe Payment Intent
		var (clientSecret, paymentIntentId) = await _stripeService.CreatePaymentIntentAsync(
			booking.TotalPrice,
			booking.Customer.Email
		);

		// Save payment in DB
		var payment = new Payment
		{
			BookingId = dto.BookingId,
			Amount = booking.TotalPrice,
			PaymentStatus = PaymentStatus.Pending,
			TransactionId = paymentIntentId,
			PaymentMethod = PaymentMethod.CreditCard
		};

		await _paymentRepository.CreateAsync(payment);

		return new StripePaymentResponse
		{
			ClientSecret = clientSecret,
			PaymentIntentId = paymentIntentId,
			Amount = booking.TotalPrice
		};
	}

	public async Task<PaymentDto> ConfirmPaymentAsync(Guid bookingId, string paymentIntentId)
	{
		var payment = await _paymentRepository.GetByBookingIdAsync(bookingId);
		if (payment == null) throw new Exception("الدفعة غير موجودة");

		// Verify with Stripe
		var isSuccess = await _stripeService.VerifyPaymentAsync(paymentIntentId);

		if (isSuccess)
		{
			payment.PaymentStatus = PaymentStatus.Success;
			payment.PaymentDate = DateTime.UtcNow;
			await _notificationService.SendAsync(
				payment.Booking.CustomerId,
				"تم الدفع بنجاح",
				$"تم دفع {payment.Amount} جنيه بنجاح"
			);
		}
		else
		{
			payment.PaymentStatus = PaymentStatus.Failed;

			await _notificationService.SendAsync(
				payment.Booking.CustomerId,
				"فشل الدفع",
				"فشلت عملية الدفع، يرجى المحاولة مرة أخرى"
			);
		}

		await _paymentRepository.UpdateAsync(payment);
		return _mapper.Map<PaymentDto>(payment);
	}

	public async Task<bool> RefundPaymentAsync(Guid paymentId)
	{
		var payment = await _paymentRepository.GetByIdAsync(paymentId);
		if (payment == null) throw new Exception("الدفعة غير موجودة");
		if (payment.PaymentStatus != PaymentStatus.Success) throw new Exception("لا يمكن استرجاع هذا الدفع");

		var refunded = await _stripeService.RefundPaymentAsync(payment.TransactionId, payment.Amount);

		if (refunded)
		{
			payment.PaymentStatus = PaymentStatus.Refunded;
			await _paymentRepository.UpdateAsync(payment);
		}

		return refunded;
	}
}
