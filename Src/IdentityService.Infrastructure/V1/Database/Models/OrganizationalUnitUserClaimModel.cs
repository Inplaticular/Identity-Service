using Inplanticular.IdentityService.Core.V1.Entities;

namespace Inplanticular.IdentityService.Infrastructure.V1.Database.Models;

public class OrganizationalUnitUserClaimModel : OrganizationalUnitUserClaim {
	public OrganizationalUnitModel Unit { get; set; } 
}