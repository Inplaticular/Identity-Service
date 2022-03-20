using Inplanticular.IdentityService.Core.V1.Dtos;
using Inplanticular.IdentityService.Core.V1.ValueObjects;

namespace Inplanticular.IdentityService.Core.V1.Contracts.Responses.Information; 

public class GetUsersByNameOrEmailResponse : BaseResponse<GetUsersByNameOrEmailResponse.Body> {
	public class Body {
		public IEnumerable<UserDto> Users { get; set; }
	}

	public static class Message {
		public static readonly Info SuitableUsersReturned = new Info {
			Code = nameof(SuitableUsersReturned),
			Description = "All suitable user for the specified query where returned successfully"
		};
	}
}