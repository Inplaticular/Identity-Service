using System.ComponentModel.DataAnnotations;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Requests.Information; 

public class GetUsersByNameOrEmailRequest {
	[Required] public string UsernameEmail { get; set; }
}