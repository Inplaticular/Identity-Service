using Inplanticular.IdentityService.Core.V1.ValueObjects;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization.Groups;

public class UpdateOrganizationalGroupResponse : OrganizationalGroupResponse {
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
	}
}