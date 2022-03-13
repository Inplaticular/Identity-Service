using Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authorization.Units;
using Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization.Units;
using Inplanticular.IdentityService.Core.V1.Entities;
using Inplanticular.IdentityService.Core.V1.Repositories;
using Inplanticular.IdentityService.Core.V1.Services.Authorization;
using Inplanticular.IdentityService.Core.V1.ValueObjects;

using Microsoft.AspNetCore.Identity;

namespace Inplanticular.IdentityService.Infrastructure.V1.Services.Authorization;

public class OrganizationalUnitManagementService : IOrganizationalUnitManagementService {
	private readonly UserManager<IdentityUser> _userManager;
	private readonly IOrganizationalGroupRepository _organizationalGroupRepository;
	private readonly IOrganizationalUnitRepository _organizationalUnitRepository;

	public OrganizationalUnitManagementService(UserManager<IdentityUser> userManager, IOrganizationalGroupRepository organizationalGroupRepository, IOrganizationalUnitRepository organizationalUnitRepository) {
		this._userManager = userManager;
		this._organizationalGroupRepository = organizationalGroupRepository;
		this._organizationalUnitRepository = organizationalUnitRepository;
	}
	
	public async Task<AddOrganizationalUnitResponse> AddOrganizationalUnitAsync(AddOrganizationalUnitRequest request) {
		var group = await this._organizationalGroupRepository.FindGroupByIdAsync(request.GroupId);

		if (group is null) {
			return new AddOrganizationalUnitResponse {
				Errors = new[] {AddOrganizationalUnitResponse.Error.OrganizationalGroupDoesNotExist}
			};
		}

		var existingUnit = await this._organizationalUnitRepository.FindUnitByNameAsync(request.Name);

		if (existingUnit is not null) {
			return new AddOrganizationalUnitResponse() {
				Errors = new[] {AddOrganizationalUnitResponse.Error.OrganizationalUnitAlreadyExists}
			};
		}

		var newUnit = new OrganizationalUnit() {
			GroupId = request.GroupId,
			Name = request.Name
		};
		
		await this._organizationalUnitRepository.AddUnitAsync(newUnit);

		return new AddOrganizationalUnitResponse() {
			Succeeded = true,
			Messages = new[] {AddOrganizationalUnitResponse.Message.OrganizationalUnitAdded},
			Content = new AddOrganizationalUnitResponse.Body() {
				Unit = newUnit
			}
		};
	}

	public async Task<RemoveOrganizationalUnitResponse> RemoveOrganizationalUnitAsync(RemoveOrganizationalUnitRequest request) {
		var existingUnit = await this._organizationalUnitRepository.FindUnitByIdAsync(request.Id);

		if (existingUnit is null) {
			return new RemoveOrganizationalUnitResponse() {
				Errors = new[] {RemoveOrganizationalUnitResponse.Error.OrganizationalUnitDoesNotExist}
			};
		}

		await this._organizationalUnitRepository.RemoveUnitAsync(existingUnit);

		return new RemoveOrganizationalUnitResponse() {
			Succeeded = true,
			Errors = new[] {RemoveOrganizationalUnitResponse.Message.OrganizationalUnitRemoved}
		};
	}

	public async Task<UpdateOrganizationalUnitResponse> UpdateOrganizationalUnitAsync(UpdateOrganizationalUnitRequest request) {
		var result = await this._organizationalUnitRepository.UpdateUnitAsync(
			request.Id,
			unit => {
				if (request.Name is not null)
					unit.Name = request.Name;

				if (request.Type is not null)
					unit.Type = request.Type;
			}
		);

		return new UpdateOrganizationalUnitResponse() {
			Succeeded = result,
			Messages = result
				? new[] {UpdateOrganizationalUnitResponse.Message.OrganizationalUnitUpdated}
				: Enumerable.Empty<Info>(),
			Errors = !result
				? new[] {UpdateOrganizationalUnitResponse.Error.OrganizationalUnitDoesNotExist}
				: Enumerable.Empty<Info>()
		};
	}

	public async Task<AddOrganizationalUnitUserClaimResponse> AddOrganizationalUnitUserClaimAsync(AddOrganizationalUnitUserClaimRequest request) {
		var unit = await this._organizationalUnitRepository.FindUnitByIdAsync(request.UnitId);

		if (unit is null) {
			return new AddOrganizationalUnitUserClaimResponse() {
				Errors = new[] {AddOrganizationalUnitUserClaimResponse.Error.UserDoesNotExist}
			};
		}

		var user = await this._userManager.FindByIdAsync(request.UserId);

		if (user is null) {
			return new AddOrganizationalUnitUserClaimResponse() {
				Errors = new[] {AddOrganizationalUnitUserClaimResponse.Error.OrganizationalUnitDoesNotExist}
			};
		}

		var existingUserClaim = await this._organizationalUnitRepository.FindUserClaimByValuesAsync(request.UserId, request.Type, request.Value);

		if (existingUserClaim is not null) {
			return new AddOrganizationalUnitUserClaimResponse() {
				Errors = new[] {AddOrganizationalUnitUserClaimResponse.Error.OrganizationalUnitUserClaimAlreadyExists}
			};
		}

		var newUserClaim = new OrganizationalUnitUserClaim {
			UnitId = request.UnitId,
			UserId = request.UserId,
			Type = request.Type,
			Value = request.Value
		};
		
		await this._organizationalUnitRepository.AddUserClaimAsync(newUserClaim);

		return new AddOrganizationalUnitUserClaimResponse() {
			Succeeded = true,
			Messages = new[] {AddOrganizationalUnitUserClaimResponse.Message.OrganizationalUnitUserClaimAdded},
			Content = new AddOrganizationalUnitUserClaimResponse.Body() {
				UserClaim = newUserClaim
			}
		};
	}

	public async Task<RemoveOrganizationalUnitUserClaimResponse> RemoveOrganizationalUnitUserClaimAsync(RemoveOrganizationalUnitUserClaimRequest request) {
		var existingUserClaim = await this._organizationalUnitRepository.FindUserClaimByIdAsync(request.Id);

		if (existingUserClaim is null) {
			return new RemoveOrganizationalUnitUserClaimResponse() {
				Errors = new[] {RemoveOrganizationalUnitUserClaimResponse.Error.OrganizationalUnitUserClaimDoesNotExist}
			};
		}

		await this._organizationalUnitRepository.RemoveUserClaimAsync(existingUserClaim);

		return new RemoveOrganizationalUnitUserClaimResponse() {
			Succeeded = true,
			Errors = new[] {RemoveOrganizationalUnitUserClaimResponse.Message.OrganizationalUnitUserClaimRemoved}
		};
	}
}