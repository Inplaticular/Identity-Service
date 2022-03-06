using System.ComponentModel.DataAnnotations;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authorization.Units;

public class RemoveOrganizationalUnitUserClaimRequest {
	[Required] public string Id { get; set; }
}