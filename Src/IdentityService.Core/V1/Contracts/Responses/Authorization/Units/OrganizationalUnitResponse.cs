using Inplanticular.IdentityService.Core.V1.Entities;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization.Units; 

public class OrganizationalUnitResponse : BaseResponse {
	public OrganizationalUnit Unit { get; set; }
}