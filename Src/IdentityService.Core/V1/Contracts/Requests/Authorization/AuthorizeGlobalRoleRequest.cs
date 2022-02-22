namespace Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authorization; 

public class AuthorizeGlobalRoleRequest : AuthorizeRequest {
	public string GlobalRole { get; set; }
}