using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Inplanticular.IdentityService.Infrastructure.V1.Database; 

public class ApplicationDbContext : IdentityDbContext {
	public ApplicationDbContext(DbContextOptions options) : base(options) { }
}