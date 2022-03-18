using System.Text;

using Inplanticular.IdentityService.Core.V1.Options;
using Inplanticular.IdentityService.Core.V1.Repositories;
using Inplanticular.IdentityService.Core.V1.Services;
using Inplanticular.IdentityService.Core.V1.Services.Authentication;
using Inplanticular.IdentityService.Core.V1.Services.Authorization;
using Inplanticular.IdentityService.Core.V1.Services.Information;
using Inplanticular.IdentityService.Infrastructure.V1.Database;
using Inplanticular.IdentityService.Infrastructure.V1.Repositories;
using Inplanticular.IdentityService.Infrastructure.V1.Services;
using Inplanticular.IdentityService.Infrastructure.V1.Services.Authentication;
using Inplanticular.IdentityService.Infrastructure.V1.Services.Authorization;
using Inplanticular.IdentityService.Infrastructure.V1.Services.Information;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Inplanticular.IdentityService.WebAPI; 

public static class Program {
	public static async Task Main(string[] args) {
		var builder = WebApplication.CreateBuilder(args);
		Program.ConfigureServices(builder);
		var app = builder.Build();
		Program.ConfigurePipeline(app);

		using (var scope = app.Services.CreateScope()) {
			var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
			
			if ((await context.Database.GetPendingMigrationsAsync()).Any())
				await context.Database.MigrateAsync();
		}
		
		await app.RunAsync();
	}

	private static void ConfigureServices(WebApplicationBuilder builder) {
		builder.Configuration.AddEnvironmentVariables();

		Program.ConfigureOptions(builder, out var jwtIssuingOptions);
		Program.ConfigureControllers(builder);
		Program.ConfigureScopedServices(builder);
		Program.ConfigureEntityFramework(builder);
		Program.ConfigureIdentity(builder);
		Program.ConfigureJwtAuthentication(builder, jwtIssuingOptions);
		
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
	}
	
	private static void ConfigureOptions(WebApplicationBuilder builder, out JwtIssuingOptions jwtIssuingOptions) {
		// Disable default ApiControllerAttribute model state validation to allow controllers to pass model state errors to custom responses.
		builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
		
		jwtIssuingOptions = new JwtIssuingOptions();
		builder.Configuration.Bind(JwtIssuingOptions.AppSettingsKey, jwtIssuingOptions);
		builder.Services.Configure<JwtIssuingOptions>(builder.Configuration.GetSection(JwtIssuingOptions.AppSettingsKey));
		builder.Services.Configure<EmailHostOptions>(builder.Configuration.GetSection(EmailHostOptions.AppSettingsKey));
	}

	private static void ConfigureControllers(WebApplicationBuilder builder) {
		builder.Services.AddControllers();
	}
	
	private static void ConfigureScopedServices(WebApplicationBuilder builder) {
		builder.Services.AddScoped<IJwtIssuingService, JwtIssuingService>();
		builder.Services.AddScoped<IEmailService, EmailService>();
		builder.Services.AddScoped<IMappingService, MappingService>();
		
		builder.Services.AddScoped<ISignUpService, SignUpService>();
		builder.Services.AddScoped<ILoginService<IdentityUser>, LoginService>();
		builder.Services.AddScoped<IPasswordResetService, PasswordResetService>();

		builder.Services.AddScoped<IGlobalAuthorizationService, GlobalAuthorizationService>();
		builder.Services.AddScoped<IEntityAuthorizationService, EntityAuthorizationService>();

		builder.Services.AddScoped<IOrganizationalGroupRepository, OrganizationalGroupRepository>();
		builder.Services.AddScoped<IOrganizationalUnitRepository, OrganizationalUnitRepository>();
		builder.Services.AddScoped<IOrganizationalGroupManagementService, OrganizationalGroupManagementService>();
		builder.Services.AddScoped<IOrganizationalUnitManagementService, OrganizationalUnitManagementService>();

		builder.Services.AddScoped<IAuthenticationInformationService, AuthenticationInformationService>();
		builder.Services.AddScoped<IAuthorizationInformationService, AuthorizationInformationService>();
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

	private static void ConfigureIdentity(WebApplicationBuilder builder)
	{
		builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
			{
				options.User.RequireUniqueEmail = true;
				options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._+"; // removed @ so username cannot be an email
			})
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders();
	}
	
	private static void ConfigureJwtAuthentication(WebApplicationBuilder builder, JwtIssuingOptions jwtIssuingOptions) {
		builder.Services.AddAuthentication(
			options => {
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}
		).AddJwtBearer(
			options => {
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters {
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtIssuingOptions.Secret)),
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true
				};
			}
		);
	}

	private static void ConfigurePipeline(WebApplication app) {
		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment()) {
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();
	}
}