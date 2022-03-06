using Inplanticular.IdentityService.Core.V1.Entities;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization.Units; 

public class OrganizationalUnitResponse<TBody> : BaseResponse<TBody> where TBody : OrganizationalUnitResponse<TBody>.Body {
	public class Body {
		public OrganizationalUnit Unit { get; set; }
	}
}