using Inplanticular.IdentityService.Core.V1.Entities;

namespace Inplanticular.IdentityService.Core.V1.Repositories; 

public interface IOrganizationalGroupRepository {
	Task AddGroupAsync(OrganizationalGroup group);
	Task RemoveGroupAsync(OrganizationalGroup group);
	Task UpdateGroupAsync(OrganizationalGroup group);
	Task<OrganizationalGroup> FindGroupById(string id);
	Task<OrganizationalGroup> FindGroupByName(string name);
}