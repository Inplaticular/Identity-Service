using System.ComponentModel.DataAnnotations;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authorization.Units; 

public class AddOrganizationalUnitRequest {
	[Required] public string GroupId { get; set; }
	[Required] public string Name { get; set; }
	[Required] public string Type { get; set; }
}