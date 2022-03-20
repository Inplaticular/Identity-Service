namespace Inplanticular.IdentityService.Core.V1.Services; 

public interface IEmailService {
	Task SendEmailAsync(string to, string subject, string message);
}