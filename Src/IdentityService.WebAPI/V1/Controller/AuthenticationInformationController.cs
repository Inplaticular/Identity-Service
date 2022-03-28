using Inplanticular.IdentityService.Core.V1.Contracts.Requests.Information;
using Inplanticular.IdentityService.Core.V1.Contracts.Responses.Information;
using Inplanticular.IdentityService.Core.V1.Services.Information;
using Inplanticular.IdentityService.WebAPI.V1.Extensions;

using Microsoft.AspNetCore.Mvc;

namespace Inplanticular.IdentityService.WebAPI.V1.Controller; 

[ApiController]
[Route("v1/information/authentication")]
public class AuthenticationInformationController : ControllerBase {
	private readonly IAuthenticationInformationService _authenticationInformationService;
	private readonly ILogger<AuthenticationInformationController> _logger;

	public AuthenticationInformationController(IAuthenticationInformationService authenticationInformationService, ILogger<AuthenticationInformationController> logger) {
		this._authenticationInformationService = authenticationInformationService;
		this._logger = logger;
	}
	/// <summary>
	/// Get a user matching to the passed userId
	/// </summary>
	[HttpGet]
	[Route("user")]
	[ProducesResponseType(typeof(GetUserByIdResponse), 200)]
	public async Task<IActionResult> GetUserByIdAsync([FromQuery] GetUserByIdRequest request) {
		if (!this.HasValidModelState(out GetUserByIdResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._authenticationInformationService.GetUserByIdAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.GetUserByIdAsync)} threw an exception");
			return this.InternalServerError<GetUserByIdResponse>(e);
		}
	}

	/// <summary>
	/// Gets a user by a passed name OR a passed email.
	/// </summary>
	[HttpGet]
	[Route("users")]
	[ProducesResponseType(typeof(GetUsersByNameOrEmailResponse), 200)]
	public async Task<IActionResult> GetUsersByNameOrEmailAsync([FromQuery] GetUsersByNameOrEmailRequest request) {
		if (!this.HasValidModelState(out GetUsersByNameOrEmailResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._authenticationInformationService.GetUsersByNameOrEmailAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.GetUsersByNameOrEmailAsync)} threw an exception");
			return this.InternalServerError<GetUsersByNameOrEmailResponse>(e);
		}
	}
}