using Inplanticular.IdentityService.Core.V1.ValueObjects;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization;

public class AuthorizeGlobalRoleResponse : AuthorizeResponse {
	public static class Error {
		public static readonly Info UserNotFound = new() {
			Code = nameof(UserNotFound),
			Description = "No user was found for the provided token"
		};
		
		public static readonly Info UserNotInRole = new() {
			Code = nameof(UserNotInRole),
			Description = "The user does not belong to the provided global role"
		};
	}
}