using Inplanticular.IdentityService.Core.V1.ValueObjects;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization.Units;

public class ValidateOrganizationalUnitUserClaimResponse : BaseResponse<ValidateOrganizationalUnitUserClaimResponse.Body> {
	public class Body {
		public bool Valid { get; set; }
	}
	
	public static class Message {
		public static readonly Info ValidUserClaim = new() {
			Code = nameof(ValidUserClaim),
			Description = "The provided user claim information is valid and exists"
		};
		
		public static readonly Info InvalidUserClaim = new() {
			Code = nameof(InvalidUserClaim),
			Description = "The provided user claim information is invalid"
		};
	}
}