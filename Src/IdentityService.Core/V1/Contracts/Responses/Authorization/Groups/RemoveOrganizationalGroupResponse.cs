using Inplanticular.IdentityService.Core.V1.ValueObjects;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization.Groups;

public class RemoveOrganizationalGroupResponse : OrganizationalGroupResponse<RemoveOrganizationalGroupResponse.Body> {
	public new class Body : OrganizationalGroupResponse<Body>.Body { }
	
	public static class Message {
		public static readonly Info OrganizationalGroupRemoved = new() {
			Code = nameof(OrganizationalGroupRemoved),
			Description = "The organizational group was removed successfully"
		};
	}
	
	public static class Error {
		public static readonly Info OrganizationalGroupDoesNotExist = new() {
			Code = nameof(OrganizationalGroupDoesNotExist),
			Description = "The specified organizational group does not exist"
		};
	}
}