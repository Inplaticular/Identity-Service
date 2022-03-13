using Inplanticular.IdentityService.Core.V1.Entities;

namespace Inplanticular.IdentityService.Infrastructure.V1.Database.Models;

public class OrganizationalGroupModel : OrganizationalGroup {
	public ICollection<OrganizationalUnitModel> Units { get; set; }
}