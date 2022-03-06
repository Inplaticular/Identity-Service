using System.ComponentModel.DataAnnotations;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authorization.Groups;

public class ChangeOrganizationalGroupNameRequest {
	[Required] public string Id { get; set; }
	[Required] public string NewName { get; set; }
}