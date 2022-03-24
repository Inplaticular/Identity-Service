using Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authorization.Groups;
using Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization.Groups;
using Inplanticular.IdentityService.Core.V1.Services.Authorization;
using Inplanticular.IdentityService.WebAPI.V1.Extensions;

using Microsoft.AspNetCore.Mvc;

namespace Inplanticular.IdentityService.WebAPI.V1.Controller; 

[ApiController]
[Route("v1/authorization/group")]
public class OrganizationalGroupController : ControllerBase {
	private readonly IOrganizationalGroupManagementService _organizationalGroupManagementService;
	private readonly ILogger<OrganizationalGroupController> _logger;

	public OrganizationalGroupController(IOrganizationalGroupManagementService organizationalGroupManagementService, ILogger<OrganizationalGroupController> logger) {
		this._organizationalGroupManagementService = organizationalGroupManagementService;
		this._logger = logger;
	}

	[HttpPost]
	public async Task<IActionResult> AddOrganizationalGroupAsync([FromBody] AddOrganizationalGroupRequest request) {
		if (!this.HasValidModelState(out AddOrganizationalGroupResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._organizationalGroupManagementService.AddOrganizationalGroupAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.AddOrganizationalGroupAsync)} threw an exception");
			return this.InternalServerError<AddOrganizationalGroupResponse>(e);
		}
	}

	[HttpDelete]
	public async Task<IActionResult> RemoveOrganizationalGroupAsync([FromBody] RemoveOrganizationalGroupRequest request) {
		if (!this.HasValidModelState(out RemoveOrganizationalGroupResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._organizationalGroupManagementService.RemoveOrganizationalGroupAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.RemoveOrganizationalGroupAsync)} threw an exception");
			return this.InternalServerError<RemoveOrganizationalGroupResponse>(e);
		}
	}

	[HttpPut]
	[Route("update")]
	public async Task<IActionResult> UpdateOrganizationalGroupAsync([FromBody] UpdateOrganizationalGroupRequest request) {
		if (!this.HasValidModelState(out UpdateOrganizationalGroupResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._organizationalGroupManagementService.UpdateOrganizationalGroupAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.UpdateOrganizationalGroupAsync)} threw an exception");
			return this.InternalServerError<UpdateOrganizationalGroupResponse>(e);
		}
	}
}