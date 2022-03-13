using Inplanticular.IdentityService.Core.V1.Entities;

namespace Inplanticular.IdentityService.Core.V1.Repositories;

public interface IOrganizationalUnitRepository {
	Task AddUnitAsync(OrganizationalUnit unit);
	Task RemoveUnitAsync(OrganizationalUnit unit);
	Task<bool> UpdateUnitAsync(OrganizationalUnit unit);
	Task<bool> UpdateUnitAsync(string id, Action<OrganizationalUnit> unitUpdater);
	Task<OrganizationalUnit?> FindUnitByIdAsync(string id);
	Task<OrganizationalUnit?> FindUnitByNameAsync(string name);
	
	Task AddUserClaimAsync(OrganizationalUnitUserClaim userClaim);
	Task RemoveUserClaimAsync(OrganizationalUnitUserClaim userClaim);
	Task<OrganizationalUnitUserClaim?> FindUserClaimByIdAsync(string id);
	Task<OrganizationalUnitUserClaim?> FindUserClaimByValuesAsync(string userId, string type, string value);
	Task<IEnumerable<OrganizationalUnitUserClaim>> GetUserClaimsForUnitAsync(OrganizationalUnit unit);
}