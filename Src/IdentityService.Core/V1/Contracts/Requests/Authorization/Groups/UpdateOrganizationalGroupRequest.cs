using System.ComponentModel.DataAnnotations;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authorization.Groups;

public class UpdateOrganizationalGroupRequest {
	[Required] public string Id { get; set; }
	[Required] public string Name { get; set; }
}