using BookingService.Application.Dtos.Notification;
using BookingService.Application.Dtos.Payment;
using BookingService.Domain.Models;
using Mapster;

namespace BookingService.Application.Mapping;

public class NotificationMappingConfigration : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<Notification, NotificationDto>();
			
	}
}
