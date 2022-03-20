using System.ComponentModel.DataAnnotations;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authorization.Units; 

public class AddOrganizationalUnitUserClaimRequest {
	[Required] public string UnitId { get; set; }
	[Required] public string UserId { get; set; }
	[Required] public string Type { get; set; }
	[Required] public string Value { get; set; }
}