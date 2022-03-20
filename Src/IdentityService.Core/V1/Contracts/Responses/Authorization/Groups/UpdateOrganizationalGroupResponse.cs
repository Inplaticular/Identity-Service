using Inplanticular.IdentityService.Core.V1.ValueObjects;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization.Groups;

public class UpdateOrganizationalGroupResponse : OrganizationalGroupResponse<UpdateOrganizationalGroupResponse.Body> {
	public new class Body : OrganizationalGroupResponse<Body>.Body { }

	public static class Message {
		public static readonly Info OrganizationalGroupUpdated = new() {
			Code = nameof(OrganizationalGroupUpdated),
			Description = "The organizational group was updated successfully"
		};
	}
	
	public static class Error {
		public static readonly Info OrganizationalGroupDoesNotExist = new() {
			Code = nameof(OrganizationalGroupDoesNotExist),
			Description = "The specified organizational group does not exist"
		};
		
		public static readonly Info OrganizationalGroupNameAlreadyExists = new() {
			Code = nameof(OrganizationalGroupNameAlreadyExists),
			Description = "The specified organizational group name already exists"
		};
	}
}