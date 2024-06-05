using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static ApiDbContext;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
	private readonly ApiDbContext _context;

	public CategoryController(ApiDbContext context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
	{
		try
		{
			var categories = await _context.Categories.Include(c => c.SubCategories).ToListAsync();
			if (categories == null || categories.Count == 0)
			{
				return NotFound("No categories found."); // Return 404 if no categories found
			}

			return Ok(categories); // Return 200 OK with categories
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Internal server error: {ex.Message}"); // Return 500 Internal Server Error on exception
		}
	}
}
