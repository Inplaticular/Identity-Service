using System.ComponentModel.DataAnnotations;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authentication;

public class SignUpRequest {
	[Required]
	public string Username { get; set; }
	
	[Required, EmailAddress]
	public string Email { get; set; }
	
	[Required]
	public string Password { get; set; }
}