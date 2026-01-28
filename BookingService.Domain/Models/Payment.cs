using BookingService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.Models;
public class Payment
{
	#region fields
	public Guid Id { get; set; }
	public decimal Amount { get; set; }
	public DateTime PaymentDate { get; set; } = DateTime.UtcNow; 
	public String TransactionId { get; set; }
	public PaymentMethod PaymentMethod { get; set; }
	public string PaymentGatewayResponse { get; set; }
	public PaymentStatus PaymentStatus { get; set; }
	#endregion

	#region Navigation Properties
	public Guid BookingId { get; set; }
	public Booking Booking { get; set; }
	#endregion
}
