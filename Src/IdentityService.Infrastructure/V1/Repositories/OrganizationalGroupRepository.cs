using Inplanticular.IdentityService.Core.V1.Entities;
using Inplanticular.IdentityService.Core.V1.Repositories;
using Inplanticular.IdentityService.Core.V1.Services;
using Inplanticular.IdentityService.Infrastructure.V1.Database;
using Inplanticular.IdentityService.Infrastructure.V1.Database.Models;

using Microsoft.EntityFrameworkCore;

namespace Inplanticular.IdentityService.Infrastructure.V1.Repositories; 

public class OrganizationalGroupRepository : IOrganizationalGroupRepository {
	private readonly ApplicationDbContext _applicationDbContext;
	private readonly IMappingService _mappingService;

	public OrganizationalGroupRepository(ApplicationDbContext applicationDbContext, IMappingService mappingService) {
		this._applicationDbContext = applicationDbContext;
		this._mappingService = mappingService;
	}
	
	public async Task AddGroupAsync(OrganizationalGroup group) {
		this._applicationDbContext.OrganizationalGroups.Add(this._mappingService.MapTo<OrganizationalGroupModel>(group)!);
		await this._applicationDbContext.SaveChangesAsync();
	}

	public async Task RemoveGroupAsync(OrganizationalGroup group) {
		this._applicationDbContext.OrganizationalGroups.Remove(this._mappingService.MapTo<OrganizationalGroupModel>(group)!);
		await this._applicationDbContext.SaveChangesAsync();
	}

	public async Task<bool> UpdateGroupAsync(OrganizationalGroup group) {
		var entry = this._applicationDbContext.Entry(this._mappingService.MapTo<OrganizationalGroupModel>(group)!);
		if (entry.State == EntityState.Detached)
			return false;
		
		entry.CurrentValues.SetValues(group);
		await this._applicationDbContext.SaveChangesAsync();
		return true;
	}
	
	public async Task<bool> UpdateGroupAsync(string id, Action<OrganizationalGroup> groupUpdater) {
		var group = await this.FindGroupByIdAsync(id);
		if (group is null)
			return false;

		// Memberwise clone
		var newGroup = this._mappingService.MapTo<OrganizationalGroup>(group)!;
		groupUpdater(newGroup);

		var entry = this._applicationDbContext.Entry(this._mappingService.MapTo<OrganizationalGroupModel>(group)!);
		if (entry.State == EntityState.Detached)
			return false;
		
		entry.CurrentValues.SetValues(newGroup);
		await this._applicationDbContext.SaveChangesAsync();
		return true;
	}

	public async Task<OrganizationalGroup?> FindGroupByIdAsync(string id) {
		var group = await this._applicationDbContext.OrganizationalGroups.FirstOrDefaultAsync(group => group.Id.Equals(id));

		if (group is null)
			return null;
		
		return this._mappingService.MapTo<OrganizationalGroup>(group);
	}

	public async Task<OrganizationalGroup?> FindGroupByNameAsync(string name) {
		var group = await this._applicationDbContext.OrganizationalGroups.FirstOrDefaultAsync(group => group.Name.Equals(name));

		if (group is null)
			return null;
		
		return this._mappingService.MapTo<OrganizationalGroup>(group);
	}
}