using BookingService.Application.Dtos.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces;
public interface IPaymentService
{
	Task<StripePaymentResponse> CreatePaymentAsync(Guid customerId, CreatePaymentDto dto);
	Task<PaymentDto> ConfirmPaymentAsync(Guid bookingId, string paymentIntentId);
	Task<bool> RefundPaymentAsync(Guid paymentId);
}