using Store.Api.DTOs.Product;
using Store.Api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Store.Services.Interfaces;

namespace Store.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IReadOnlyCollection<ProductResponseDto>>> GetAll()
	{
		var result = await productService.GetAllAsync();
		return Ok(result.Select(x => x.ToResponse()));
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult<ProductResponseDto>> GetById(int id)
	{
		var result = await productService.GetByIdAsync(id);
		return result is null ? NotFound() : Ok(result.ToResponse());
	}

	[HttpPost]
	public async Task<ActionResult<ProductResponseDto>> Create(ProductRequestDto request)
	{
		try
		{
			var created = await productService.CreateAsync(request.ToEntity());
			return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToResponse());
		}
		catch (ArgumentException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (KeyNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
	}

	[HttpPut("{id:int}")]
	public async Task<ActionResult<ProductResponseDto>> Update(int id, ProductRequestDto request)
	{
		try
		{
			var updated = await productService.UpdateAsync(id, request.ToEntity());
			return Ok(updated.ToResponse());
		}
		catch (ArgumentException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (KeyNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		try
		{
			await productService.DeleteAsync(id);
			return NoContent();
		}
		catch (KeyNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (InvalidOperationException ex)
		{
			return Conflict(ex.Message);
		}
	}
}