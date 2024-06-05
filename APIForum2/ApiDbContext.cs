using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Microsoft.Extensions.Logging;

public class ApiDbContext : DbContext
{
	private readonly ILogger<ApiDbContext> _logger;

	public ApiDbContext(DbContextOptions<ApiDbContext> options, ILogger<ApiDbContext> logger) : base(options)
	{
		_logger = logger;
		_logger.LogInformation($"Using connection string: {options.FindExtension<SqlServerOptionsExtension>()?.ConnectionString}");
	}

	public DbSet<Category> Categories { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder) //för att jag inte gjorde namn i plural i databas, EF kör pluralization..
	{
		base.OnModelCreating(modelBuilder);

		
		modelBuilder.Entity<Category>().ToTable("Category"); //så att den letar efter table Category inte categories.

		
		modelBuilder.Entity<SubCategory>().ToTable("SubCategory");
	}

	public class Category
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public List<SubCategory>? SubCategories { get; set; }
	}

	public class SubCategory
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int CategoryId { get; set; }
	}
}
