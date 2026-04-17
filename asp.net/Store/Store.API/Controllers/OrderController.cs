using Store.Api.DTOs.Order;
using Store.Api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Store.Services.Interfaces;

namespace Store.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService orderService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IReadOnlyCollection<OrderResponseDto>>> GetAll()
	{
		var result = await orderService.GetAllAsync();
		return Ok(result.Select(x => x.ToResponse()));
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult<OrderResponseDto>> GetById(int id)
	{
		var result = await orderService.GetByIdAsync(id);
		return result is null ? NotFound() : Ok(result.ToResponse());
	}

	[HttpPost]
	public async Task<ActionResult<OrderResponseDto>> Create(OrderRequestDto request)
	{
		try
		{
			var created = await orderService.CreateAsync(request.ToEntity());
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
	public async Task<ActionResult<OrderResponseDto>> Update(int id, OrderRequestDto request)
	{
		try
		{
			var updated = await orderService.UpdateAsync(id, request.ToEntity());
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
			await orderService.DeleteAsync(id);
			return NoContent();
		}
		catch (KeyNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
	}
}