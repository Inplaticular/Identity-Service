namespace Inplanticular.IdentityService.WebAPI; 

public static class Program {
	public static void Main(string[] args) {
		var builder = WebApplication.CreateBuilder(args);
		Program.ConfigureServices(builder);
		var app = builder.Build();
		Program.ConfigurePipeline(app);
		app.Run();
	}

	public static void ConfigureServices(WebApplicationBuilder builder) {
		// Add services to the container.

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
	}

	public static void ConfigurePipeline(WebApplication app) {
		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment()) {
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();
	}
}