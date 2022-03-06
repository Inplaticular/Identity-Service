using System.ComponentModel.DataAnnotations;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authorization.Groups;

public class RemoveOrganizationalGroupRequest {
	[Required] public string Id { get; set; }
}