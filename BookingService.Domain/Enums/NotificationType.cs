using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.Enums;
public enum NotificationType
{
	BookingCreated = 1,
	BookingConfirmed = 2,
	BookingCancelled = 3,
	BookingCompleted = 4,
	PaymentSuccess = 5,
	PaymentFailed = 6,
	General = 7
}
