using Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authentication;
using Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authentication;
using Inplanticular.IdentityService.Core.V1.Services.Authentication;
using Inplanticular.IdentityService.Core.V1.ValueObjects;

using Microsoft.AspNetCore.Identity;

namespace Inplanticular.IdentityService.Infrastructure.V1.Services.Authentication; 

public class SignUpService : ISignUpService {
	private readonly UserManager<IdentityUser> _userManager;

	public SignUpService(UserManager<IdentityUser> userManager) {
		this._userManager = userManager;
	}
	
	public async Task<SignUpResponse> SignUpUserAsync(SignUpRequest request) {
		var user = new IdentityUser() {
			UserName = request.Username,
			Email = request.Email
		};

		var creationResult = await this._userManager.CreateAsync(user, request.Password);
		
		if (!creationResult.Succeeded) {
			return new SignUpResponse {
				Errors = creationResult.Errors.Select(error => new Info() {
					Code = error.Code,
					Description = error.Description
				})
			};
		}
		
		return new SignUpResponse {
			Succeeded = true,
			Messages = new[] {SignUpResponse.Message.SignedUp}
		};
	}
}