using Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authentication;
using Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authentication;

namespace Inplanticular.IdentityService.Core.V1.Services.Authentication; 

public interface IPasswordResetService {
	Task<RequestChangePasswordResponse> RequestChangePasswordTokenAsync(RequestChangePasswordRequest request);
	Task<ChangePasswordResponse> ChangePasswordAsync(ChangePasswordRequest request);
}