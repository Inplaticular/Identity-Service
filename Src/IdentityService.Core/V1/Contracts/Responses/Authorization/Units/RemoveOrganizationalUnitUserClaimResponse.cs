using Inplanticular.IdentityService.Core.V1.ValueObjects;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization.Units;

public class RemoveOrganizationalUnitUserClaimResponse : OrganizationalUnitUserClaimResponse<RemoveOrganizationalUnitUserClaimResponse.Body> {
	public class Body : OrganizationalUnitUserClaimResponse<Body>.Body { }

	public static class Message {
		public static readonly Info OrganizationalUnitUserClaimRemoved = new() {
			Code = nameof(OrganizationalUnitUserClaimRemoved),
			Description = "The organizational unit user claim was removed successfully"
		};
	}
	
	public static class Error {
		public static readonly Info OrganizationalUnitUserClaimDoesNotExist = new() {
			Code = nameof(OrganizationalUnitUserClaimDoesNotExist),
			Description = "The specified organizational unit user claim does not exist"
		};
	}
}