using Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authorization;
using Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authorization.Units;
using Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authentication;
using Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization;
using Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization.Units;
using Inplanticular.IdentityService.Core.V1.Services.Authorization;
using Inplanticular.IdentityService.WebAPI.V1.Extensions;

using Microsoft.AspNetCore.Mvc;

namespace Inplanticular.IdentityService.WebAPI.V1.Controller; 

[ApiController]
[Route("v1/authorize")]
public class AuthorizationController : ControllerBase {
	private readonly IGlobalAuthorizationService _globalAuthorizationService;
	private readonly IEntityAuthorizationService _entityAuthorizationService;
	private readonly ILogger<AuthorizationController> _logger;

	public AuthorizationController(IGlobalAuthorizationService globalAuthorizationService, IEntityAuthorizationService entityAuthorizationService, ILogger<AuthorizationController> logger) {
		this._globalAuthorizationService = globalAuthorizationService;
		this._entityAuthorizationService = entityAuthorizationService;
		this._logger = logger;
	}

	/// <summary>
	/// Authorizes a user which requested to do something requiring authorization
	/// </summary>
	[HttpPost]
	[Route("user")]
	[ProducesResponseType(typeof(AuthorizeResponse), 200)]
	public async Task<IActionResult> AuthorizeUser([FromBody] AuthorizeRequest request) {
		if (!this.HasValidModelState(out AuthorizeResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._globalAuthorizationService.AuthorizeUserAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.AuthorizeUser)} threw an exception");
			return this.InternalServerError<AuthorizeResponse>(e);
		}
	}
	/// <summary>
	/// Authorizes a user with a certain global role
	/// </summary>
	[HttpPost]
	[Route("globalrole")]
	[ProducesResponseType(typeof(AuthorizeGlobalRoleResponse), 200)]
	public async Task<IActionResult> AuthorizeUserGlobalRoleAsync([FromBody] AuthorizeGlobalRoleRequest request) {
		if (!this.HasValidModelState(out AuthorizeGlobalRoleResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._globalAuthorizationService.AuthorizeUserGlobalRoleAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.AuthorizeUserGlobalRoleAsync)} threw an exception");
			return this.InternalServerError<AuthorizeGlobalRoleResponse>(e);
		}
	}
	/// <summary>
	/// Validates if a user claim is valid
	/// </summary>
	[HttpPost]
	[Route("userclaim")]
	[ProducesResponseType(typeof(ValidateOrganizationalUnitUserClaimResponse), 200)]
	public async Task<IActionResult> ValidateOrganizationalUnitUserClaimAsync([FromBody] ValidateOrganizationalUnitUserClaimRequest request) {
		if (!this.HasValidModelState(out ValidateOrganizationalUnitUserClaimResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._entityAuthorizationService.ValidateOrganizationalUnitUserClaimAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.ValidateOrganizationalUnitUserClaimAsync)} threw an exception");
			return this.InternalServerError<ValidateOrganizationalUnitUserClaimResponse>(e);
		}
	}
}