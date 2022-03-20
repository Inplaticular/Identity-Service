using Inplanticular.IdentityService.Core.V1.ValueObjects;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization.Groups; 

public class AddOrganizationalGroupResponse : OrganizationalGroupResponse<AddOrganizationalGroupResponse.Body> {
	public new class Body : OrganizationalGroupResponse<Body>.Body { }

	public static class Message {
		public static readonly Info OrganizationalGroupAdded = new() {
			Code = nameof(OrganizationalGroupAdded),
			Description = "The organizational group was added successfully"
		};
	}
	
	public static class Error {
		public static readonly Info OrganizationalGroupAlreadyExists = new() {
			Code = nameof(OrganizationalGroupAlreadyExists),
			Description = "The organizational group already exists"
		};
	}
}