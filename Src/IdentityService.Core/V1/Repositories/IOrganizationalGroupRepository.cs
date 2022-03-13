using Inplanticular.IdentityService.Core.V1.Entities;

namespace Inplanticular.IdentityService.Core.V1.Repositories; 

public interface IOrganizationalGroupRepository {
	Task AddGroupAsync(OrganizationalGroup group);
	Task RemoveGroupAsync(OrganizationalGroup group);
	Task<bool> UpdateGroupAsync(OrganizationalGroup group);
	Task<bool> UpdateGroupAsync(string id, Action<OrganizationalGroup> groupUpdater);
	Task<OrganizationalGroup?> FindGroupByIdAsync(string id);
	Task<OrganizationalGroup?> FindGroupByNameAsync(string name);
}