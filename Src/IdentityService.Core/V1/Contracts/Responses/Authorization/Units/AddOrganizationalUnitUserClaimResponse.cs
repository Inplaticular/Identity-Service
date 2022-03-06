using Inplanticular.IdentityService.Core.V1.ValueObjects;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization.Units;

public class AddOrganizationalUnitUserClaimResponse : OrganizationalUnitUserClaimResponse<AddOrganizationalUnitUserClaimResponse.Body> {
	public class Body : OrganizationalUnitUserClaimResponse<Body>.Body { }

	public static class Message {
		public static readonly Info OrganizationalUnitUserClaimAdded = new() {
			Code = nameof(OrganizationalUnitUserClaimAdded),
			Description = "The organizational unit user claim was added successfully"
		};
	}
	
	public static class Error {
		public static readonly Info OrganizationalUnitUserClaimAlreadyExists = new() {
			Code = nameof(OrganizationalUnitUserClaimAlreadyExists),
			Description = "The organizational unit user claim already exists"
		};
	}
}