using System.ComponentModel.DataAnnotations;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authorization; 

public class AuthorizeRequest {
	[Required]
	public string Token { get; set; }
}