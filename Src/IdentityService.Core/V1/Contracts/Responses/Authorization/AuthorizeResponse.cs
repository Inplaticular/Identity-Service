using Inplanticular.IdentityService.Core.V1.ValueObjects;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization; 

public class AuthorizeResponse : BaseResponse {
	public bool Authorized { get; set; }
	
	public static class Message {
		public static readonly Info Authorized = new() {
			Code = nameof(Authorized),
			Description = "The User is authorized"
		};
		
		public static readonly Info Unauthorized = new() {
			Code = nameof(Authorized),
			Description = "The User is not authorized"
		};
	}
	
	public static class Error {
		public static readonly Info InvalidToken = new() {
			Code = nameof(InvalidToken),
			Description = "The provided token is not valid"
		};
		
		public static readonly Info ExpiredToken = new() {
			Code = nameof(ExpiredToken),
			Description = "The token expired and is not valid anymore"
		};
	}
}