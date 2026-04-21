using BlogApp.API.DTO.Mappers;
using BlogApp.API.DTO.Models.Comments;
using BlogApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController(ICommentService service) : ControllerBase
{
	[HttpGet]
	[ProducesResponseType(typeof(IReadOnlyCollection<CommentResponse>), StatusCodes.Status200OK)]
	public async Task<ActionResult<IReadOnlyCollection<CommentResponse>>> GetAll()
	{
		var comments = await service.GetAllAsync();
		return Ok(comments.Select(x => x.ToResponse()).ToArray());
	}

	[HttpGet("{id:guid}")]
	[ProducesResponseType(typeof(CommentResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<CommentResponse>> GetById(Guid id)
	{
		var comment = await service.GetByIdAsync(id);
		if (comment is null)
		{
			return NotFound();
		}

		return Ok(comment.ToResponse());
	}

	[HttpPost]
	[ProducesResponseType(typeof(CommentResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<CommentResponse>> Create([FromBody] CreateCommentRequest request)
	{
		var created = await service.CreateAsync(request.ToEntity());
		var response = created.ToResponse();
		return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
	}

	[HttpPut("{id:guid}")]
	[ProducesResponseType(typeof(CommentResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<CommentResponse>> Update(Guid id, [FromBody] UpdateCommentRequest request)
	{
		var updated = await service.UpdateAsync(id, request.ToEntity());
		return Ok(updated.ToResponse());
	}

	[HttpDelete("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Delete(Guid id)
	{
		await service.DeleteAsync(id);
		return NoContent();
	}
}
