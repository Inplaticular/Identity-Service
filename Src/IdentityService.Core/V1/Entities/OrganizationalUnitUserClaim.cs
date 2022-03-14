namespace Inplanticular.IdentityService.Core.V1.Entities;

public class OrganizationalUnitUserClaim {
	public string Id { get; set; }
	public string UserId { get; set; }
	public string UnitId { get; set; }
	public string Type { get; set; }
	public string Value { get; set; }

	public OrganizationalUnitUserClaim() {
		this.Id = Guid.NewGuid().ToString();
	}
}