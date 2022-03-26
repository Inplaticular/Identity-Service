using System.ComponentModel.DataAnnotations;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Requests.Information;

public class GetUserByIdRequest {
	[Required] public string UserId { get; set; }
}