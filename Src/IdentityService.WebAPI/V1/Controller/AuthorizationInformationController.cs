using Inplanticular.IdentityService.Core.V1.Contracts.Requests.Information;
using Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authentication;
using Inplanticular.IdentityService.Core.V1.Contracts.Responses.Information;
using Inplanticular.IdentityService.Core.V1.Services.Information;
using Inplanticular.IdentityService.WebAPI.V1.Extensions;

using Microsoft.AspNetCore.Mvc;

namespace Inplanticular.IdentityService.WebAPI.V1.Controller;

[ApiController]
[Route("v1/information/authorization")]
public class AuthorizationInformationController : ControllerBase {
	private readonly IAuthorizationInformationService _authorizationInformationService;
	private readonly ILogger<AuthorizationInformationController> _logger;

	public AuthorizationInformationController(IAuthorizationInformationService authorizationInformationService, ILogger<AuthorizationInformationController> logger) {
		this._authorizationInformationService = authorizationInformationService;
		this._logger = logger;
	}
	/// <summary>
	/// Gets the organizational group by its name
	/// </summary>
	[HttpGet]
	[Route("group")]
	[ProducesResponseType(typeof(GetOrganizationalGroupByNameResponse), 200)]
	public async Task<IActionResult> GetOrganizationalGroupByNameAsync([FromQuery] GetOrganizationalGroupByNameRequest request) {
		if (!this.HasValidModelState(out GetOrganizationalGroupByNameResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._authorizationInformationService.GetOrganizationalGroupByNameAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.GetOrganizationalGroupByNameAsync)} threw an exception");
			return this.InternalServerError<GetOrganizationalGroupByNameResponse>(e);
		}
	}
	/// <summary>
	/// Gets all user claims for a certain organizational unit.
	/// </summary>
	[HttpGet]
	[Route("userclaims")]
	[ProducesResponseType(typeof(GetUserClaimsForOrganizationalUnitResponse), 200)]
	public async Task<IActionResult> GetUserClaimsForOrganizationalUnitAsync([FromQuery] GetUserClaimsForOrganizationalUnitRequest request) {
		if (!this.HasValidModelState(out GetUserClaimsForOrganizationalUnitResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._authorizationInformationService.GetUserClaimsForOrganizationalUnitAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.GetUserClaimsForOrganizationalUnitAsync)} threw an exception");
			return this.InternalServerError<GetUserClaimsForOrganizationalUnitResponse>(e);
		}
	}
}