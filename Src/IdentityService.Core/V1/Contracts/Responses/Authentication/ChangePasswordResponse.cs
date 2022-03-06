using Inplanticular.IdentityService.Core.V1.ValueObjects;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authentication;

public class ChangePasswordResponse : BaseResponse<ChangePasswordResponse.Body> {
	public class Body { }
	
	public static class Message {
		public static readonly Info PasswordReset = new() {
			Code = nameof(PasswordReset),
			Description = "The password has been reset successfully"
		};
	}
	
	public static class Error {
		public static readonly Info UserNotFound = new() {
			Code = nameof(UserNotFound),
			Description = "The user for the provided email address was not found"
		};
	}
}