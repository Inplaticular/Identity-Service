using Inplanticular.IdentityService.Core.V1.Entities;
using Inplanticular.IdentityService.Core.V1.ValueObjects;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Responses.Information; 

public class GetUserClaimsForOrganizationalUnitResponse : BaseResponse<GetUserClaimsForOrganizationalUnitResponse.Body> {
	public class Body {
		public IEnumerable<OrganizationalUnitUserClaim> UserClaims { get; set; }
	}

	public static class Message {
		public static Info UserClaimsReturnedSuccessfully = new() {
			Code = nameof(UserClaimsReturnedSuccessfully),
			Description = "All registered user claims for the specified organizational unit were returned successfully"
		};
	}

	public static class Error {
		public static Info OrganizationalUnitDoesNotExist = new() {
			Code = nameof(OrganizationalUnitDoesNotExist),
			Description = "The specified organizational unit does not exist"
		};
	}
}