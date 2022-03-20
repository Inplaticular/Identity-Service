using System.ComponentModel.DataAnnotations;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Requests.Information;

public class GetOrganizationalGroupByNameRequest {
	[Required] public string GroupName { get; set; }
}