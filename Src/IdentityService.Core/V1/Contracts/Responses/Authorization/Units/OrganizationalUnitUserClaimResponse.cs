using Inplanticular.IdentityService.Core.V1.Entities;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization.Units;

public class OrganizationalUnitUserClaimResponse<TBody> : BaseResponse<TBody> where TBody : OrganizationalUnitUserClaimResponse<TBody>.Body {
	public class Body {
		public OrganizationalUnitUserClaim UserClaim { get; set; }
	}
}