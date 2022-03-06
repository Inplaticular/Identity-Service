using Inplanticular.IdentityService.Core.V1.ValueObjects;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization.Units;

public class AddOrganizationalUnitResponse : OrganizationalUnitResponse {
	public static class Message {
		public static readonly Info OrganizationalUnitAdded = new() {
			Code = nameof(OrganizationalUnitAdded),
			Description = "The organizational unit was added successfully"
		};
	}
	
	public static class Error {
		public static readonly Info OrganizationalUnitAlreadyExists = new() {
			Code = nameof(OrganizationalUnitAlreadyExists),
			Description = "The organizational unit already exists"
		};
	}
}