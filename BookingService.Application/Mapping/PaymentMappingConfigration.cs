using BookingService.Application.Dtos.Payment;
using BookingService.Domain.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Mapping;
public class PaymentMappingConfigration : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<Payment,PaymentDto>()
			.Map(dest => dest.Status, src => src.PaymentStatus.ToString())
			.Map(dest => dest.PaidAt, src => src.PaymentDate);
	}
}
