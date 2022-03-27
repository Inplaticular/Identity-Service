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

	/// <summary>
	/// Creates an organizational unit.
	/// </summary>
	[HttpPost]
	[ProducesResponseType(typeof(AddOrganizationalUnitResponse), 200)]
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
	/// <summary>
	/// Deletes an organizational unit.
	/// </summary>
	[HttpDelete]
	[ProducesResponseType(typeof(RemoveOrganizationalUnitResponse), 200)]
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
	/// <summary>
	/// Edits an organizational unit.
	/// </summary>
	[HttpPut]
	[Route("update")]
	[ProducesResponseType(typeof(UpdateOrganizationalUnitResponse), 200)]
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
	/// <summary>
	/// Creates a user claim for an organizational unit.
	/// </summary>
	[HttpPost]
	[Route("userclaim")]
	[ProducesResponseType(typeof(AddOrganizationalUnitUserClaimResponse), 200)]
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
	/// <summary>
	/// Deletes a user claim belonging to a certain organizational unit
	/// </summary>
	[HttpDelete]
	[Route("userclaim")]
	[ProducesResponseType(typeof(RemoveOrganizationalUnitUserClaimResponse), 200)]
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