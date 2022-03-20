using Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authentication;
using Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authentication;

namespace Inplanticular.IdentityService.Core.V1.Services.Authentication; 

public interface ILoginService<TUser> {
	Task<LoginResponse> LoginUserAsync(LoginRequest request);
	Task<TUser?> GetUserForTokenAsync(string token);
}