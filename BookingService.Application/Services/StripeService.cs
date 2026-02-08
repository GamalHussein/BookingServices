using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Services;
public class StripeService
{
	public StripeService(IConfiguration configuration)
	{
		StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
	}

	public async Task<(string clientSecret, string paymentIntentId)> CreatePaymentIntentAsync(
		decimal amount,
		string customerEmail)
	{
		var options = new PaymentIntentCreateOptions
		{
			Amount = (long)(amount * 100), // Convert to cents
			Currency = "egp",
			ReceiptEmail = customerEmail,
			AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
			{
				Enabled = true,
			},
		};

		var service = new PaymentIntentService();
		var paymentIntent = await service.CreateAsync(options);

		return (paymentIntent.ClientSecret, paymentIntent.Id);
	}

	public async Task<bool> VerifyPaymentAsync(string paymentIntentId)
	{
		var service = new PaymentIntentService();
		var paymentIntent = await service.GetAsync(paymentIntentId);
		return paymentIntent.Status == "succeeded";
	}

	public async Task<bool> RefundPaymentAsync(string paymentIntentId, decimal amount)
	{
		var options = new RefundCreateOptions
		{
			PaymentIntent = paymentIntentId,
			Amount = (long)(amount * 100),
		};

		var service = new RefundService();
		var refund = await service.CreateAsync(options);
		return refund.Status == "succeeded";
	}
}