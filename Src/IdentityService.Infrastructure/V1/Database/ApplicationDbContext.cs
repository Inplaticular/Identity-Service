using Inplanticular.IdentityService.Infrastructure.V1.Database.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Inplanticular.IdentityService.Infrastructure.V1.Database; 

public class ApplicationDbContext : IdentityDbContext {
	public DbSet<OrganizationalGroupModel> OrganizationalGroups { get; set; }
	public DbSet<OrganizationalUnitModel> OrganizationalUnits { get; set; }
	public DbSet<OrganizationalUnitUserClaimModel> OrganizationalUnitUserClaims { get; set; }
	
	public ApplicationDbContext(DbContextOptions options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder builder) {
		base.OnModelCreating(builder);
		
		builder.Entity<OrganizationalGroupModel>(entity => {
			entity.HasKey(g => g.Id);
			entity.HasIndex(g => g.Name).IsUnique();
		});
		
		builder.Entity<OrganizationalUnitModel>(entity => {
			entity.HasKey(u => u.Id);
			entity.HasIndex(u => u.Name).IsUnique();

			entity.HasOne(u => u.Group)
				.WithMany(g => g.Units)
				.HasForeignKey(u => u.GroupId);
		});
		
		builder.Entity<OrganizationalUnitUserClaimModel>(entity => {
			entity.HasKey(uc => uc.Id);
			entity.HasIndex(uc => new { uc.UnitId, uc.UserId, uc.Type, uc.Value }).IsUnique();

			entity.HasOne(uc => uc.Unit)
				.WithMany(u => u.UserClaims)
				.HasForeignKey(u => u.UnitId);

			entity.HasOne(uc => uc.User)
				.WithMany()
				.HasForeignKey(uc => uc.UserId);
		});
	}
}