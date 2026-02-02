using BookingService.Application.Dtos.Bookings;
using BookingService.Application.Interfaces;
using BookingService.Domain.Enums;
using BookingService.Domain.Models;
using MapsterMapper;

namespace BookingService.Application.Services;
public class BookingServices(
	IBookingRepository bookingRepository,
	IServiceRepository serviceRepository,
	IMapper mapper
	) : IBookingService
{
	private readonly IBookingRepository BookingRepository = bookingRepository;
	private readonly IServiceRepository ServiceRepository = serviceRepository;
	private readonly IMapper Mapper = mapper;

	public async Task<bool> CancelAsync(Guid id, Guid userId, CancelBookingDto cancelDto)
	{
		var booking = await BookingRepository.GetByIdAsync(id);
		if (booking is null)
		{
		
			throw new Exception("الحجز غير موجود");
		}
		if (booking.CustomerId != userId)
		{
			throw new Exception("ليس لديك إذن لإلغاء هذا الحجز");
		}
		if (booking.Status == BookingStatus.Completed || booking.Status == BookingStatus.Cancelled)
		{
			throw new Exception("لا يمكن إلغاء الحجز في الحالة الحالية");
		}
		booking.Status = BookingStatus.Cancelled;
		booking.CreatedAt = DateTime.UtcNow;
		booking.Notes= $"{booking.Notes}\nسبب الإلغاء: {cancelDto.CancellationReason}";
		return await BookingRepository.UpdateAsync(booking);

	}

	public async Task<bool> CompleteAsync(Guid id, Guid providerId)
	{
		var booking =await BookingRepository.GetByIdAsync(id);
		if (booking is null)
		{
			throw new Exception("الحجز غير موجود");
		}
		var service = await ServiceRepository.GetByIdAsync(booking.ServiceId);
		if (service is null || service.ProviderId != providerId)
		{
			throw new Exception("ليس لديك إذن لإكمال هذا الحجز");
		}
		if (booking.Status != BookingStatus.Confirmed)
		{
			throw new Exception("لا يمكن إكمال الحجز في الحالة الحالية");
		}
		booking.Status = BookingStatus.Completed;
		booking.CompletedAt = DateTime.UtcNow;
		return await BookingRepository.UpdateAsync(booking);
	}

	public async Task<bool> ConfirmAsync(Guid id, Guid providerId)
	{
		var booking =await BookingRepository.GetByIdAsync(id);
		if (booking is null)
		{
			throw new Exception("الحجز غير موجود");
		}
		var service = await ServiceRepository.GetByIdAsync(booking.ServiceId);
		if (service is null || service.ProviderId != providerId)
		{
			throw new Exception("ليس لديك إذن لتأكيد هذا الحجز");
		}
		if (booking.Status != BookingStatus.Pending)
		{
			throw new Exception("لا يمكن تأكيد الحجز في الحالة الحالية");
		}
		booking.Status = BookingStatus.Confirmed;
		return await BookingRepository.UpdateAsync(booking);
	}

	public async Task<BookingDto> CreateAsync(Guid customerId, CreateBookingDto createDto)
	{
		// Check if service exists
		var service = await ServiceRepository.GetByIdAsync(createDto.ServiceId);
		if (service == null)
		{
			throw new Exception("الخدمة غير موجودة");
		}

		// Check if service is active
		if (!service.IsActive)
		{
			throw new Exception("الخدمة غير متاحة حالياً");
		}

		var booking = Mapper.Map<Booking>(createDto);
		booking.CustomerId = customerId;
		booking.Status = BookingStatus.Pending;
		booking.CreatedAt = DateTime.UtcNow;
		booking.TotalPrice = service.Price;
		var createdBooking = await BookingRepository.CreateAsync(booking);
		var bookingDto = Mapper.Map<BookingDto>(createdBooking);
		return bookingDto;
	}

	public async Task<IEnumerable<BookingDto>> GetAllAsync()
	{
		var bookings =await BookingRepository.GetAllAsync();
		var bookingDtos = Mapper.Map<IEnumerable<BookingDto>>(bookings);
		return bookingDtos;
	}

	public async Task<BookingDto> GetByIdAsync(Guid id)
	{
		var booking =await BookingRepository.GetByIdAsync(id);
		if (booking is null)
		{
			throw new Exception("الحجز غير موجود");
		}
		var bookingDto = Mapper.Map<BookingDto>(booking);
		return bookingDto;
	}

	public async Task<BookingDetailsDto> GetByIdWithDetailsAsync(Guid id)
	{
		var booking =await BookingRepository.GetByIdWithDetailsAsync(id);
		if (booking is null)
		{
			throw new Exception("الحجز غير موجود");
		}
		var bookingDto = Mapper.Map<BookingDetailsDto>(booking);
		return bookingDto;
	}

	public async Task<IEnumerable<BookingDto>> GetMyBookingsAsync(Guid customerId)
	{
		var bookings = await BookingRepository.GetByCustomerAsync(customerId);
		if (bookings is null)
		{
			throw new Exception("لا توجد حجوزات لهذا العميل");
		}
		var bookingDtos = Mapper.Map<IEnumerable<BookingDto>>(bookings);
		return bookingDtos;
	}

	public async Task<IEnumerable<BookingDto>> GetProviderBookingsAsync(Guid providerId)
	{
		var bookings =await BookingRepository.GetByProviderAsync(providerId);
		if (bookings is null)
		{
			throw new Exception("لا توجد حجوزات لهذا المزود");
		}
		var bookingDtos = Mapper.Map<IEnumerable<BookingDto>>(bookings);
		return bookingDtos;
	}

	public async Task<bool> UpdateAsync(Guid id, Guid customerId, UpdateBookingDto updateDto)
	{
		var bookingTask =await BookingRepository.GetByIdAsync(id);
		if (bookingTask is null)
		{
			throw new Exception("الحجز غير موجود");
		}
		if (bookingTask.CustomerId != customerId)
		{
			throw new Exception("ليس لديك إذن لتحديث هذا الحجز");
		}
		if (bookingTask.Status != BookingStatus.Pending)
		{
			throw new Exception("لا يمكن تحديث الحجز في الحالة الحالية");
		}
		bookingTask.Notes = updateDto.Notes;
		bookingTask.BookingDate = updateDto.BookingDate;
		return await BookingRepository.UpdateAsync(bookingTask);

	}
}
