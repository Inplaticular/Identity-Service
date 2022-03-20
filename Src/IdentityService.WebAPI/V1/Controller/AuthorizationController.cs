﻿using Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authorization;
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

	[HttpPost]
	[Route("user")]
	public IActionResult AuthorizeUser([FromBody] AuthorizeRequest request) {
		if (!this.HasValidModelState(out AuthorizeResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(this._globalAuthorizationService.AuthorizeUser(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.AuthorizeUser)} threw an exception");
			return this.InternalServerError<AuthorizeResponse>(e);
		}
	}
	
	[HttpPost]
	[Route("globalrole")]
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
	
	[HttpPost]
	[Route("userclaim")]
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