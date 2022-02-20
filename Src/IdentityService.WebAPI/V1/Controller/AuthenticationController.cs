using Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authentication;
using Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authentication;
using Inplanticular.IdentityService.Core.V1.Services.Authentication;
using Inplanticular.IdentityService.WebAPI.V1.Extensions;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inplanticular.IdentityService.WebAPI.V1.Controller; 

[ApiController]
[Route("v1/authentication")]
public class AuthenticationController : ControllerBase {
	private readonly ILogger<AuthenticationController> _logger;
	private readonly ISignUpService _signUpService;
	private readonly ILoginService<IdentityUser> _loginService;
	
	public AuthenticationController(ILogger<AuthenticationController> logger, ISignUpService signUpService, ILoginService<IdentityUser> loginService) {
		this._logger = logger;
		this._signUpService = signUpService;
		this._loginService = loginService;
	}

	[HttpPost]
	[Route("signup")]
	public async Task<IActionResult> SignUpUserAsync([FromBody] SignUpRequest request) {
		if (!this.HasValidModelState(out SignUpResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._signUpService.SignUpUserAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.SignUpUserAsync)} threw an exception");
			return this.InternalServerError<SignUpResponse>(e);
		}
	}
	
	[HttpPost]
	[Route("login")]
	public async Task<IActionResult> LoginUserAsync([FromBody] LoginRequest request) {
		if (!this.HasValidModelState(out LoginResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._loginService.LoginUserAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.LoginUserAsync)} threw an exception");
			return this.InternalServerError<LoginResponse>(e);
		}
	}
}