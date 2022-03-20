using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Inplanticular.IdentityService.Infrastructure.Test.TestUtils; 

public static class InMemoryDbContextFactory {
	public static async Task<TDbContext> CreateNewAsync<TDbContext>() where TDbContext : DbContext
	{
		// Create a fresh service provider, and therefore a fresh
		// InMemory database instance.
		var serviceProvider = new ServiceCollection()
			.AddEntityFrameworkSqlite()
			.BuildServiceProvider();

		// Create a new options instance telling the context to use an
		// InMemory database and the new service provider.
		var builder = new DbContextOptionsBuilder<TDbContext>()
			.UseSqlite("Data Source=Database;Mode=Memory", _ => { })
			.UseInternalServiceProvider(serviceProvider);

		var dbContext = (Activator.CreateInstance(typeof(TDbContext), builder.Options) as TDbContext)!;
		await dbContext.Database.OpenConnectionAsync();
		await dbContext.Database.EnsureCreatedAsync();
		
		return dbContext;
	}
}