using BookingService.Application.Dtos.Payment;
using BookingService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingService.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
	private readonly IPaymentService _paymentService;
	private readonly IConfiguration _configuration;

	public PaymentController(IPaymentService paymentService, IConfiguration configuration)
	{
		_paymentService = paymentService;
		_configuration = configuration;
	}
	[HttpPost]
	public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto dto)
	{
		try
		{
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var result = await _paymentService.CreatePaymentAsync(userId, dto);

			return Ok(new
			{
				success = true,
				data = new
				{
					clientSecret = result.ClientSecret,
					amount = result.Amount,
					publishableKey = _configuration["Stripe:PublishableKey"]
				}
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new { success = false, message = ex.Message });
		}
	}

	[HttpPost("{bookingId}/confirm")]
	public async Task<IActionResult> ConfirmPayment(Guid bookingId, [FromBody] string paymentIntentId)
	{
		try
		{
			var result = await _paymentService.ConfirmPaymentAsync(bookingId, paymentIntentId);
			return Ok(new { success = true, data = result });
		}
		catch (Exception ex)
		{
			return BadRequest(new { success = false, message = ex.Message });
		}
	}

	[HttpPost("{paymentId}/refund")]
	public async Task<IActionResult> RefundPayment(Guid paymentId)
	{
		try
		{
			var result = await _paymentService.RefundPaymentAsync(paymentId);
			return Ok(new { success = true, message = "تم الاسترجاع بنجاح" });
		}
		catch (Exception ex)
		{
			return BadRequest(new { success = false, message = ex.Message });
		}
	}
}
