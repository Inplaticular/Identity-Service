using Inplanticular.IdentityService.Core.V1.ValueObjects;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization.Units;

public class UpdateOrganizationalUnitResponse : OrganizationalUnitResponse<UpdateOrganizationalUnitResponse.Body> {
	public class Body : OrganizationalUnitResponse<Body>.Body { }

	public static class Message {
		public static readonly Info OrganizationalUnitUpdated = new() {
			Code = nameof(OrganizationalUnitUpdated),
			Description = "The organizational unit was updated successfully"
		};
	}
	
	public static class Error {
		public static readonly Info OrganizationalUnitDoesNotExist = new() {
			Code = nameof(OrganizationalUnitDoesNotExist),
			Description = "The specified organizational unit does not exist"
		};
	}
}