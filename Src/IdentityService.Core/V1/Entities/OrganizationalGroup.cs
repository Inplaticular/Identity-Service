namespace Inplanticular.IdentityService.Core.V1.Entities; 

public class OrganizationalGroup {
	public string Id { get; set; }
	public string Name { get; set; }

	public OrganizationalGroup() {
		this.Id = Guid.NewGuid().ToString();
	}
}