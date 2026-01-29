using BookingService.Application.Dtos.Services;
using BookingService.Application.Helpers;
using BookingService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ServiceController(IServiceService serviceService) : ControllerBase
{
	private readonly IServiceService ServiceService = serviceService;

	[HttpGet("GetAll")]
	public async Task<IActionResult> GetAll()
	{
		try
		{
			var services = await ServiceService.GetAllAsync();
			return Ok(new GenralResponse<IEnumerable<ServiceDto>>
			{
				IsSuccess = true,
				Message = "تم جلب الخدمات بنجاح",
				Data = services
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<IEnumerable<ServiceDto>>
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null
			});
		}
	}

	[HttpGet("GatAllActive")]
	public async Task<IActionResult> GetAllActive()
	{
		try
		{
			var services = await ServiceService.GetAllActiveAsync();
			return Ok(new GenralResponse<IEnumerable<ServiceDto>>
			{
				IsSuccess = true,
				Message = "تم جلب الخدمات النشطة بنجاح",
				Data = services
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<IEnumerable<ServiceDto>>
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null
			});
		}
	}

	[HttpGet("GetById")]
	public async Task<IActionResult> GetById(Guid id)
	{
		try
		{
			var service = await ServiceService.GetByIdAsync(id);
			return Ok(new GenralResponse<ServiceDto>
			{
				IsSuccess = true,
				Message = "تم جلب الخدمة بنجاح",
				Data = service
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<ServiceDto>
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null
			});
		}
	}

	[HttpGet("{id}/details")]
	public async Task<IActionResult> GetByIdWithDetails(Guid id)
	{
		try
		{
			var service = await ServiceService.GetByIdWithDetailsAsync(id);
			return Ok(new GenralResponse<ServiceDetailsDto>
			{
				IsSuccess = true,
				Message = "تم جلب تفاصيل الخدمة بنجاح",
				Data = service
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<ServiceDetailsDto>
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null
			});
		}
	}

	[HttpGet("category/{categoryId}")]
	public async Task<IActionResult> GetByCategory(Guid categoryId)
	{
		try
		{
			var services = await ServiceService.GetByCategoryAsync(categoryId);
			return Ok(new GenralResponse<IEnumerable<ServiceDto>>
			{
				IsSuccess = true,
				Message = "تم جلب الخدمات بنجاح",
				Data = services
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<IEnumerable<ServiceDto>>
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null
			});
		}
	}

	[HttpGet("provider/{providerId}")]
	public async Task<IActionResult> GetByProvider(Guid providerId)
	{
		try
		{
			var services = await ServiceService.GetByProviderAsync(providerId);
			return Ok(new GenralResponse<IEnumerable<ServiceDto>>
			{
				IsSuccess = true,
				Message = "تم جلب الخدمات بنجاح",
				Data = services
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<IEnumerable<ServiceDto>>
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null
			});
		}
	}

	[Authorize(Roles = "ServiceProvider,Admin")]
	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateServiceDto createDto)
	{
		try
		{
			var service = await ServiceService.CreateAsync(createDto);
			return Ok(new GenralResponse<ServiceDto>
			{
				IsSuccess = true,
				Message = "تم إنشاء الخدمة بنجاح",
				Data = service
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<ServiceDto>
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null
			});
		}
	}

	[Authorize(Roles = "ServiceProvider,Admin")]
	[HttpPut("{id}")]
	public async Task<IActionResult> Update(Guid id, [FromBody] UpdateServiceDto updateDto)
	{
		try
		{
			var result = await ServiceService.UpdateAsync(id, updateDto);
			if (!result)
			{
				return NotFound(new GenralResponse<bool>
				{
					IsSuccess = false,
					Message = "الخدمة غير موجودة",
					Data = false
				});
			}
			return Ok(new GenralResponse<bool>
			{
				IsSuccess = true,
				Message = "تم تحديث الخدمة بنجاح",
				Data = true
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<bool>
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = false
			});
		}
	}

	[Authorize(Roles = "ServiceProvider,Admin")]
	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		try
		{
			var result = await ServiceService.DeleteAsync(id);
			if (!result)
			{
				return NotFound(new GenralResponse<bool>
				{
					IsSuccess = false,
					Message = "الخدمة غير موجودة",
					Data = false
				});
			}
			return Ok(new GenralResponse<bool>
			{
				IsSuccess = true,
				Message = "تم حذف الخدمة بنجاح",
				Data = true
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<bool>
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = false
			});
		}
	}
}
