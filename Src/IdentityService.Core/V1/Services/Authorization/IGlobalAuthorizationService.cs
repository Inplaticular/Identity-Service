using Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authorization;
using Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization;

namespace Inplanticular.IdentityService.Core.V1.Services.Authorization; 

public interface IGlobalAuthorizationService {
	AuthorizeResponse AuthorizeUser(AuthorizeRequest request);
	Task<AuthorizeGlobalRoleResponse> AuthorizeUserGlobalRoleAsync(AuthorizeGlobalRoleRequest request);
}