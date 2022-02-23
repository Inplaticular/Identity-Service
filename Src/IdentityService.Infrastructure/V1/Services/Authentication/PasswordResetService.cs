using Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authentication;
using Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authentication;
using Inplanticular.IdentityService.Core.V1.Services;
using Inplanticular.IdentityService.Core.V1.Services.Authentication;
using Inplanticular.IdentityService.Core.V1.ValueObjects;

using Microsoft.AspNetCore.Identity;

namespace Inplanticular.IdentityService.Infrastructure.V1.Services.Authentication; 

public class PasswordResetService : IPasswordResetService {
	private readonly UserManager<IdentityUser> _userManager;
	private readonly IEmailService _emailService;

	public PasswordResetService(UserManager<IdentityUser> userManager, IEmailService emailService) {
		this._userManager = userManager;
		this._emailService = emailService;
	}

	public async Task<RequestChangePasswordResponse> RequestChangePasswordTokenAsync(RequestChangePasswordRequest request) {
		var user = await this._userManager.FindByEmailAsync(request.Email);

		if (user is null) {
			return new RequestChangePasswordResponse() {
				Errors = new[] {RequestChangePasswordResponse.Error.UserNotFound}
			};
		}

		await this._emailService.SendEmailAsync(request.Email, "Reset password requested", $"Token: {await this._userManager.GeneratePasswordResetTokenAsync(user)}");
		return new RequestChangePasswordResponse() {
			Succeeded = true,
			Messages = new[] {RequestChangePasswordResponse.Message.EmailSendIfRegistered}
		};
	}

	public async Task<ChangePasswordResponse> ChangePasswordAsync(ChangePasswordRequest request) {
		var user = await this._userManager.FindByEmailAsync(request.Email);

		if (user is null) {
			return new ChangePasswordResponse() {
				Errors = new[] {ChangePasswordResponse.Error.UserNotFound}
			};
		}

		var changePasswordResult = await this._userManager.ResetPasswordAsync(user, request.ResetToken, request.NewPassword);

		return new ChangePasswordResponse() {
			Succeeded = changePasswordResult.Succeeded,
			Messages = changePasswordResult.Succeeded ? new[] {ChangePasswordResponse.Message.PasswordReset} : Enumerable.Empty<Info>(),
			Errors = changePasswordResult.Succeeded ? Enumerable.Empty<Info>() : changePasswordResult.Errors.Select(error => new Info() {
				Code = error.Code,
				Description = error.Description
			}).ToArray()
		};
	}
}