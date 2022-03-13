using Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authorization.Groups;
using Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization.Groups;
using Inplanticular.IdentityService.Core.V1.Entities;
using Inplanticular.IdentityService.Core.V1.Repositories;
using Inplanticular.IdentityService.Core.V1.Services.Authorization;
using Inplanticular.IdentityService.Core.V1.ValueObjects;

namespace Inplanticular.IdentityService.Infrastructure.V1.Services.Authorization;

public class OrganizationalGroupManagementService : IOrganizationalGroupManagementService {
	private readonly IOrganizationalGroupRepository _organizationalGroupRepository;

	public OrganizationalGroupManagementService(IOrganizationalGroupRepository organizationalGroupRepository) {
		this._organizationalGroupRepository = organizationalGroupRepository;
	}
	
	public async Task<AddOrganizationalGroupResponse> AddOrganizationalGroupAsync(AddOrganizationalGroupRequest request) {
		var existingGroup = await this._organizationalGroupRepository.FindGroupByNameAsync(request.Name);

		if (existingGroup is not null) {
			return new AddOrganizationalGroupResponse() {
				Errors = new[] {AddOrganizationalGroupResponse.Error.OrganizationalGroupAlreadyExists}
			};
		}

		var newGroup = new OrganizationalGroup() {Name = request.Name};
		await this._organizationalGroupRepository.AddGroupAsync(newGroup);

		return new AddOrganizationalGroupResponse() {
			Succeeded = true,
			Messages = new[] {AddOrganizationalGroupResponse.Message.OrganizationalGroupAdded},
			Content = new AddOrganizationalGroupResponse.Body() {
				Group = newGroup
			}
		};
	}

	public async Task<RemoveOrganizationalGroupResponse> RemoveOrganizationalGroupAsync(RemoveOrganizationalGroupRequest request) {
		var existingGroup = await this._organizationalGroupRepository.FindGroupByIdAsync(request.Id);

		if (existingGroup is null) {
			return new RemoveOrganizationalGroupResponse() {
				Errors = new[] {RemoveOrganizationalGroupResponse.Error.OrganizationalGroupDoesNotExist}
			};
		}

		await this._organizationalGroupRepository.RemoveGroupAsync(existingGroup);

		return new RemoveOrganizationalGroupResponse() {
			Succeeded = true,
			Errors = new[] {RemoveOrganizationalGroupResponse.Message.OrganizationalGroupRemoved}
		};
	}

	public async Task<UpdateOrganizationalGroupResponse> UpdateOrganizationalGroupAsync(UpdateOrganizationalGroupRequest request) {
		var result = await this._organizationalGroupRepository.UpdateGroupAsync(
			request.Id,
			group => group.Name = request.Name
		);

		return new UpdateOrganizationalGroupResponse() {
			Succeeded = result,
			Messages = result
				? new[] {UpdateOrganizationalGroupResponse.Message.OrganizationalGroupUpdated}
				: Enumerable.Empty<Info>(),
			Errors = !result
				? new[] {UpdateOrganizationalGroupResponse.Error.OrganizationalGroupDoesNotExist}
				: Enumerable.Empty<Info>()
		};
	}
}