using Microsoft.EntityFrameworkCore;

namespace APIForum2
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			// Configure DbContext with the existing database.
			builder.Services.AddDbContext<ApiDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("ForumContextConnection")));

			// Configure CORS
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAllOrigins",
					builder =>
					{
						builder.AllowAnyOrigin()
							   .AllowAnyMethod()
							   .AllowAnyHeader();
					});
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			// Use CORS
			app.UseCors("AllowAllOrigins");

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
