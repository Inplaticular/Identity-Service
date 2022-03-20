using System.ComponentModel.DataAnnotations;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authentication; 

public class RequestChangePasswordRequest {
	[Required, EmailAddress]
	public string Email { get; set; }
}