namespace Inplanticular.IdentityService.Core.V1.Entities;

public class OrganizationalUnit {
	public string GroupId { get; set; }
	public string Id { get; set; }
	public string Name { get; set; }
	public string Type { get; set; }

	public OrganizationalUnit() {
		this.Id = Guid.NewGuid().ToString();
	}
}