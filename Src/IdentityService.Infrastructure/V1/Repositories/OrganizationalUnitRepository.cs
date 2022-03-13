﻿using Inplanticular.IdentityService.Core.V1.Entities;
using Inplanticular.IdentityService.Core.V1.Repositories;
using Inplanticular.IdentityService.Core.V1.Services;
using Inplanticular.IdentityService.Infrastructure.V1.Database;
using Inplanticular.IdentityService.Infrastructure.V1.Database.Models;

using Microsoft.EntityFrameworkCore;

namespace Inplanticular.IdentityService.Infrastructure.V1.Repositories; 

public class OrganizationalUnitRepository : IOrganizationalUnitRepository {
	private readonly ApplicationDbContext _applicationDbContext;
	private readonly IMappingService _mappingService;

	public OrganizationalUnitRepository(ApplicationDbContext applicationDbContext, IMappingService mappingService) {
		this._applicationDbContext = applicationDbContext;
		this._mappingService = mappingService;
	}

	public async Task AddUnitAsync(OrganizationalUnit unit) {
		this._applicationDbContext.OrganizationalUnits.Add(this._mappingService.MapTo<OrganizationalUnitModel>(unit)!);
		await this._applicationDbContext.SaveChangesAsync();
	}

	public async Task RemoveUnitAsync(OrganizationalUnit unit) {
		this._applicationDbContext.OrganizationalUnits.Remove(this._mappingService.MapTo<OrganizationalUnitModel>(unit)!);
		await this._applicationDbContext.SaveChangesAsync();
	}

	public async Task<bool> UpdateUnitAsync(OrganizationalUnit unit) {
		var entry = this._applicationDbContext.Entry(this._mappingService.MapTo<OrganizationalUnitModel>(unit)!);
		if (entry.State == EntityState.Detached)
			return false;
		
		entry.CurrentValues.SetValues(unit);
		await this._applicationDbContext.SaveChangesAsync();
		return true;
	}

	public async Task<bool> UpdateUnitAsync(string id, Action<OrganizationalUnit> unitUpdater) {
		var unit = await this.FindUnitByIdAsync(id);
		if (unit is null)
			return false;

		// Memberwise clone
		var newUnit = this._mappingService.MapTo<OrganizationalUnit>(unit)!;
		unitUpdater(newUnit);

		var entry = this._applicationDbContext.Entry(this._mappingService.MapTo<OrganizationalUnitModel>(unit)!);
		if (entry.State == EntityState.Detached)
			return false;
		
		entry.CurrentValues.SetValues(newUnit);
		await this._applicationDbContext.SaveChangesAsync();
		return true;
	}

	public async Task<OrganizationalUnit?> FindUnitByIdAsync(string id) {
		var unit = await this._applicationDbContext.OrganizationalUnits.FirstOrDefaultAsync(unit => unit.Id.Equals(id));

		if (unit is null)
			return null;
		
		return this._mappingService.MapTo<OrganizationalUnit>(unit);
	}

	public async Task<OrganizationalUnit?> FindUnitByNameAsync(string name) {
		var unit = await this._applicationDbContext.OrganizationalUnits.FirstOrDefaultAsync(unit => unit.Name.Equals(name));

		if (unit is null)
			return null;
		
		return this._mappingService.MapTo<OrganizationalUnit>(unit);
	}

	public async Task AddUserClaimAsync(OrganizationalUnitUserClaim userClaim) {
		this._applicationDbContext.OrganizationalUnitUserClaims.Add(this._mappingService.MapTo<OrganizationalUnitUserClaimModel>(userClaim)!);
		await this._applicationDbContext.SaveChangesAsync();
	}

	public async Task RemoveUserClaimAsync(OrganizationalUnitUserClaim userClaim) {
		this._applicationDbContext.OrganizationalUnitUserClaims.Remove(this._mappingService.MapTo<OrganizationalUnitUserClaimModel>(userClaim)!);
		await this._applicationDbContext.SaveChangesAsync();
	}
	
	public async Task<OrganizationalUnitUserClaim?> FindUserClaimByIdAsync(string id) {
		var unit = await this._applicationDbContext.OrganizationalUnitUserClaims.FirstOrDefaultAsync(unit => unit.Id.Equals(id));

		if (unit is null)
			return null;
		
		return this._mappingService.MapTo<OrganizationalUnitUserClaim>(unit);
	}

	public async Task<OrganizationalUnitUserClaim?> FindUserClaimByValuesAsync(string userId, string type, string value) {
		var unit = await this._applicationDbContext.OrganizationalUnitUserClaims.FirstOrDefaultAsync(userClaim => userClaim.UserId.Equals(userId) && userClaim.Type.Equals(type) && userClaim.Value.Equals(type));

		if (unit is null)
			return null;
		
		return this._mappingService.MapTo<OrganizationalUnitUserClaim>(unit);
	}

	public async Task<IEnumerable<OrganizationalUnitUserClaim>> GetUserClaimsForUnitAsync(OrganizationalUnit unit) {
		return await this._applicationDbContext.OrganizationalUnitUserClaims
			.Where(uc => uc.UnitId.Equals(unit.Id))
			.Select(uc => this._mappingService.MapTo<OrganizationalUnitUserClaim>(uc)!).AsQueryable()
			.ToListAsync();
	}
}