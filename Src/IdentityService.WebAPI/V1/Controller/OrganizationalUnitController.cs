using Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authorization.Units;
using Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization.Units;
using Inplanticular.IdentityService.Core.V1.Services.Authorization;
using Inplanticular.IdentityService.WebAPI.V1.Extensions;

using Microsoft.AspNetCore.Mvc;

namespace Inplanticular.IdentityService.WebAPI.V1.Controller; 

[ApiController]
[Route("v1/authorization/unit")]
public class OrganizationalUnitController : ControllerBase {
	private readonly IOrganizationalUnitManagementService _organizationalUnitManagementService;
	private readonly ILogger<OrganizationalUnitController> _logger;

	public OrganizationalUnitController(IOrganizationalUnitManagementService organizationalUnitManagementService, ILogger<OrganizationalUnitController> logger) {
		this._organizationalUnitManagementService = organizationalUnitManagementService;
		this._logger = logger;
	}

	[HttpPost]
	public async Task<IActionResult> AddOrganizationalUnitAsync([FromBody] AddOrganizationalUnitRequest request) {
		if (!this.HasValidModelState(out AddOrganizationalUnitResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._organizationalUnitManagementService.AddOrganizationalUnitAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.AddOrganizationalUnitAsync)} threw an exception");
			return this.InternalServerError<AddOrganizationalUnitResponse>(e);
		}
	}
	
	[HttpDelete]
	public async Task<IActionResult> RemoveOrganizationalUnitAsync([FromBody] RemoveOrganizationalUnitRequest request) {
		if (!this.HasValidModelState(out RemoveOrganizationalUnitResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._organizationalUnitManagementService.RemoveOrganizationalUnitAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.RemoveOrganizationalUnitAsync)} threw an exception");
			return this.InternalServerError<RemoveOrganizationalUnitResponse>(e);
		}
	}
	
	[HttpPut]
	[Route("update")]
	public async Task<IActionResult> UpdateOrganizationalUnitAsync([FromBody] UpdateOrganizationalUnitRequest request) {
		if (!this.HasValidModelState(out UpdateOrganizationalUnitResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._organizationalUnitManagementService.UpdateOrganizationalUnitAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.UpdateOrganizationalUnitAsync)} threw an exception");
			return this.InternalServerError<UpdateOrganizationalUnitResponse>(e);
		}
	}
	
	[HttpPost]
	[Route("userclaim")]
	public async Task<IActionResult> AddOrganizationalUnitUserClaimAsync([FromBody] AddOrganizationalUnitUserClaimRequest request) {
		if (!this.HasValidModelState(out AddOrganizationalUnitUserClaimResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._organizationalUnitManagementService.AddOrganizationalUnitUserClaimAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.AddOrganizationalUnitUserClaimAsync)} threw an exception");
			return this.InternalServerError<AddOrganizationalUnitUserClaimResponse>(e);
		}
	}
	
	[HttpDelete]
	[Route("userclaim")]
	public async Task<IActionResult> RemoveOrganizationalUnitUserClaimAsync([FromBody] RemoveOrganizationalUnitUserClaimRequest request) {
		if (!this.HasValidModelState(out RemoveOrganizationalUnitUserClaimResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._organizationalUnitManagementService.RemoveOrganizationalUnitUserClaimAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.RemoveOrganizationalUnitUserClaimAsync)} threw an exception");
			return this.InternalServerError<RemoveOrganizationalUnitUserClaimResponse>(e);
		}
	}
}