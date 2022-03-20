using Inplanticular.IdentityService.Core.V1.ValueObjects;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization.Units;

public class AddOrganizationalUnitResponse : OrganizationalUnitResponse<AddOrganizationalUnitResponse.Body> {
	public class Body : OrganizationalUnitResponse<Body>.Body { }

	public static class Message {
		public static readonly Info OrganizationalUnitAdded = new() {
			Code = nameof(OrganizationalUnitAdded),
			Description = "The organizational unit was added successfully"
		};
	}
	
	public static class Error {
		public static readonly Info OrganizationalGroupDoesNotExist = new() {
			Code = nameof(OrganizationalGroupDoesNotExist),
			Description = "The organizational group the unit should be added to does not exist"
		};
		
		public static readonly Info OrganizationalUnitNameAlreadyExists = new() {
			Code = nameof(OrganizationalUnitNameAlreadyExists),
			Description = "The organizational unit name already exists"
		};
	}
}