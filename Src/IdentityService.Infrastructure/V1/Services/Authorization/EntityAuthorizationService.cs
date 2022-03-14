using Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authorization.Units;
using Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization.Units;
using Inplanticular.IdentityService.Core.V1.Repositories;
using Inplanticular.IdentityService.Core.V1.Services.Authorization;

namespace Inplanticular.IdentityService.Infrastructure.V1.Services.Authorization;

public class EntityAuthorizationService : IEntityAuthorizationService {
	private readonly IOrganizationalUnitRepository _organizationalUnitRepository;

	public EntityAuthorizationService(IOrganizationalUnitRepository organizationalUnitRepository) {
		this._organizationalUnitRepository = organizationalUnitRepository;
	}
	
	public async Task<ValidateOrganizationalUnitUserClaimResponse> ValidateOrganizationalUnitUserClaimAsync(ValidateOrganizationalUnitUserClaimRequest request) {
		var userClaim = await this._organizationalUnitRepository.FindUserClaimByValuesAsync(
			request.UnitId, request.UserId, request.Type, request.Value
		);

		return new ValidateOrganizationalUnitUserClaimResponse {
			Succeeded = true,
			Messages = new[] {
				userClaim is not null
					? ValidateOrganizationalUnitUserClaimResponse.Message.ValidUserClaim
					: ValidateOrganizationalUnitUserClaimResponse.Message.InvalidUserClaim
			},
			Content = new ValidateOrganizationalUnitUserClaimResponse.Body {
				Valid = userClaim is not null
			}
		};
	}
}