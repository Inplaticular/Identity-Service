using Inplanticular.IdentityService.Core.V1.ValueObjects;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Responses;

public class BaseResponse {
	public bool Succeeded { get; set; } = false;
	public IEnumerable<Info> Messages { get; set; } = Enumerable.Empty<Info>();
	public IEnumerable<Info> Errors { get; set; } = Enumerable.Empty<Info>();
}