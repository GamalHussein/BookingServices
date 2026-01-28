using BookingService.Application.Dtos.Category;
using BookingService.Application.Helpers;
using BookingService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
	private readonly ICategoryService CategoryService = categoryService;

	[HttpGet("GetAll")]
	public async Task<IActionResult> GetAll()
	{
		try
		{
			var categories = await CategoryService.GetAllAsync();
			return Ok(new GenralResponse<IEnumerable<CategoryDto>>
			{
				IsSuccess = true,
				Message = "تم جلب الفئات بنجاح",
				Data = categories
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<IEnumerable<CategoryDto>>
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null
			});

		}
	}

	[HttpGet("Active")]
	public async Task<IActionResult> GetAllActive()
	{
		try
		{
			var categories = await CategoryService.GetAllActiveAsync();
			return Ok(new GenralResponse<IEnumerable<CategoryDto>>
			{
				IsSuccess = true,
				Message = "تم جلب الفئات النشطة بنجاح",
				Data = categories
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<IEnumerable<CategoryDto>>
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null
			});
		}
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(Guid id)
	{
		try
		{
			var category = await CategoryService.GetByIdAsync(id);
			return Ok(new GenralResponse<CategoryDto>
			{
				IsSuccess = true,
				Message = "تم جلب الفئة بنجاح",
				Data = category
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<CategoryDto>
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null
			});
		}
	}

	[HttpGet("{id}/with-services")]
	public async Task<IActionResult> GetByIdWithServices(Guid id)
	{
		try
		{
			var category = await CategoryService.GetByIdWithServicesAsync(id);
			return Ok(new GenralResponse<CategoryWithServicesDto>
			{
				IsSuccess = true,
				Message = "تم جلب الفئة مع الخدمات بنجاح",
				Data = category
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<CategoryWithServicesDto>
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null
			});
		}
	}

	[Authorize(Roles ="Admin")]
	[HttpPost("Create")]
	public async Task<IActionResult> Create([FromBody] CreateCategoryDto createDto)
	{
		try
		{
			var category = await CategoryService.CreateAsync(createDto);
			return Ok(new GenralResponse<CategoryDto>
			{
				IsSuccess = true,
				Message = "تم إنشاء الفئة بنجاح",
				Data = category
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new GenralResponse<CategoryDto>
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null
			});
		}
	}

	[Authorize(Roles = "Admin")]
	[HttpPut("Update/{id}")]
	public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryDto updateDto)
	{
		try
		{
			var result = await CategoryService.UpdateAsync(id, updateDto);
			return Ok(new GenralResponse<bool>
			{
				IsSuccess = result,
				Message = result ? "تم تحديث الفئة بنجاح" : "فشل في تحديث الفئة",
				Data = result
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

	[Authorize(Roles = "Admin")]
	[HttpDelete("Delete/{id}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		try
		{
			var result = await CategoryService.DeleteAsync(id);
			return Ok(new GenralResponse<bool>
			{
				IsSuccess = result,
				Message = result ? "تم حذف الفئة بنجاح" : "فشل في حذف الفئة",
				Data = result
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
