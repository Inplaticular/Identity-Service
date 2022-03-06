using Inplanticular.IdentityService.Core.V1.Entities;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization.Groups;

public class OrganizationalGroupResponse : BaseResponse {
	public OrganizationalGroup Group { get; set; }
}