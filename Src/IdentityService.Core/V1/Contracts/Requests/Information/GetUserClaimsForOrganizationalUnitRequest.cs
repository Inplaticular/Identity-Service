using System.ComponentModel.DataAnnotations;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Requests.Information;

public class GetUserClaimsForOrganizationalUnitRequest {
	[Required] public string UnitId { get; set; }
}