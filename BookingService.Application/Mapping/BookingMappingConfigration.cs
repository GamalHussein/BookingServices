using BookingService.Application.Dtos.Bookings;
using BookingService.Domain.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Mapping;
public class BookingMappingConfigration : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<CreateBookingDto, Booking>()
			.Ignore(e => e.Id)
			.Ignore(e => e.CreatedAt)
			.Ignore(e => e.UpdatedAt)
			.Ignore(e => e.CancelledAt)
			.Ignore(e => e.CompletedAt)
			.Ignore(e => e.Status)
			.Ignore(e => e.CustomerId)
			.Ignore(e => e.Customer)
			.Ignore(e => e.Service)
			.Ignore(e => e.TotalPrice)
			.Ignore(e => e.Payment);

		config.NewConfig<UpdateBookingDto, Booking>()
			.Ignore(e => e.Id)
			.Ignore(e => e.CreatedAt)
			.Ignore(e => e.UpdatedAt)
			.Ignore(e => e.CancelledAt)
			.Ignore(e => e.CompletedAt)
			.Ignore(e => e.Status)
			.Ignore(e => e.CustomerId)
			.Ignore(e => e.Customer)
			.Ignore(e => e.Service)
			.Ignore(e => e.TotalPrice)
			.Ignore(e => e.Payment);

		config.NewConfig<Booking, BookingDto>()
			.Map(dest => dest.ServiceName, src => src.Service.Name)
			.Map(dest => dest.CustomerName, src => src.Customer.FirstName+src.Customer.LastName)
			.Map(dest => dest.CustomerPhone, src => src.Customer.PhoneNumber)
			.Map(dest => dest.Status, src => src.Status.ToString())
			.Map(dest=>dest.ServiceDuration,src=>src.Service.DurationInMinutes);

		config.NewConfig<Booking, BookingDetailsDto>()
			.Map(e => e.Status, d => d.Status.ToString())
			.Map(e => e.Customer, d => new CustomerDto
			{
				Id = d.Customer.Id,
				FullName = d.Customer.FirstName + d.Customer.LastName,
				Email = d.Customer.Email,
				PhoneNumber = d.Customer.PhoneNumber
			})
			.Map(e => e.Service, d => d.Service)
			.Map(e=>e.Payment,src=>new PaymentDto
			{
				Id = src.Payment.Id,
				Amount = src.Payment.Amount,
				PaymentMethod = src.Payment.PaymentMethod.ToString(),
				Status = src.Payment.PaymentStatus.ToString(),
				PaidAt = src.Payment.PaymentDate,
			});





	}
}
