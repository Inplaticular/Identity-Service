using Inplanticular.IdentityService.Core.V1.Services.Authentication;
using Inplanticular.IdentityService.Infrastructure.V1.Database;
using Inplanticular.IdentityService.Infrastructure.V1.Services.Authentication;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inplanticular.IdentityService.WebAPI; 

public static class Program {
	public static void Main(string[] args) {
		var builder = WebApplication.CreateBuilder(args);
		Program.ConfigureServices(builder);
		var app = builder.Build();

		using (var scope = app.Services.CreateScope()) {
			var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
			
			if (context.Database.GetPendingMigrations().Any())
				context.Database.Migrate();
		}

		Program.ConfigurePipeline(app);
		app.Run();
	}

	private static void ConfigureServices(WebApplicationBuilder builder) {
		builder.Configuration.AddEnvironmentVariables();
		
		Program.ConfigureControllers(builder);
		Program.ConfigureOptions(builder);
		Program.ConfigureScopedServices(builder);
		Program.ConfigureEntityFramework(builder);
		Program.ConfigureIdentity(builder);
		
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
	}

	private static void ConfigureControllers(WebApplicationBuilder builder) {
		builder.Services.AddControllers();
	}
	
	private static void ConfigureOptions(WebApplicationBuilder builder) {
		// Disable default ApiControllerAttribute model state validation to allow controllers to pass model state errors to custom responses.
		builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
	}
	
	private static void ConfigureScopedServices(WebApplicationBuilder builder) {
		builder.Services.AddScoped<ISignUpService, SignUpService>();
	}
	
	private static void ConfigureEntityFramework(WebApplicationBuilder builder) {
		builder.Services.AddDbContext<ApplicationDbContext>(
			options => options.UseNpgsql(
				builder.Configuration.GetConnectionString("postgres")
					.Replace("$POSTGRES_USER", Environment.GetEnvironmentVariable("POSTGRES_USER"))
					.Replace("$POSTGRES_PASSWORD", Environment.GetEnvironmentVariable("POSTGRES_PASSWORD"))
					.Replace("$POSTGRES_DB", Environment.GetEnvironmentVariable("POSTGRES_DB")),
				optionsBuilder => {
					optionsBuilder.MigrationsAssembly(typeof(Program).Assembly.GetName().Name);
				}
			)
		);
	}

	private static void ConfigureIdentity(WebApplicationBuilder builder) {
		builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
			options.User.RequireUniqueEmail = true;
			options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._+"; // removed @ so username cannot be an email
		}).AddEntityFrameworkStores<ApplicationDbContext>();
	}

	private static void ConfigurePipeline(WebApplication app) {
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