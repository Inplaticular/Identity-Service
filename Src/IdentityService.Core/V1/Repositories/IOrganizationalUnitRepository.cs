using Inplanticular.IdentityService.Core.V1.Entities;

namespace Inplanticular.IdentityService.Core.V1.Repositories;

public interface IOrganizationalUnitRepository {
	Task AddUnitAsync(OrganizationalUnit unit);
	Task RemoveUnitAsync(OrganizationalUnit unit);
	Task UpdateUnitAsync(OrganizationalUnit unit);
	Task<OrganizationalUnit> FindUnitById(string id);
	Task<OrganizationalUnit> FindUnitByName(string name);
	
	Task AddUserClaimAsync(OrganizationalUnitUserClaim userClaim);
	Task RemoveUserClaimAsync(OrganizationalUnitUserClaim userClaim);
	Task<IEnumerable<OrganizationalUnitUserClaim>> GetUserClaimsForUnitAsync(OrganizationalUnit unit);
}