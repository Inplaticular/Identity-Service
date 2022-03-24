using Inplanticular.IdentityService.Core.V1.Contracts.Requests.Information;
using Inplanticular.IdentityService.Core.V1.Contracts.Responses.Information;
using Inplanticular.IdentityService.Core.V1.Entities;
using Inplanticular.IdentityService.Core.V1.Repositories;
using Inplanticular.IdentityService.Core.V1.Services.Information;
using Inplanticular.IdentityService.Core.V1.ValueObjects;

namespace Inplanticular.IdentityService.Infrastructure.V1.Services.Information;

public class AuthorizationInformationService : IAuthorizationInformationService {
	private readonly IOrganizationalGroupRepository _organizationalGroupRepository;
	private readonly IOrganizationalUnitRepository _organizationalUnitRepository;

	public AuthorizationInformationService(IOrganizationalGroupRepository organizationalGroupRepository, IOrganizationalUnitRepository organizationalUnitRepository) {
		this._organizationalGroupRepository = organizationalGroupRepository;
		this._organizationalUnitRepository = organizationalUnitRepository;
	}
	
	public async Task<GetOrganizationalGroupByNameResponse> GetOrganizationalGroupByNameAsync(GetOrganizationalGroupByNameRequest request) {
		var group = await this._organizationalGroupRepository.FindGroupByNameAsync(request.GroupName);

		return new GetOrganizationalGroupByNameResponse {
			Succeeded = group is not null,
			Messages = group is null
				? Enumerable.Empty<Info>()
				: new[] {GetOrganizationalGroupByNameResponse.Message.GroupReturnedSuccessfully},
			Errors = group is null
				? new[] {GetOrganizationalGroupByNameResponse.Error.OrganizationalUnitDoesNotExist}
				: Enumerable.Empty<Info>(),
			Content = new GetOrganizationalGroupByNameResponse.Body {
				Group = group
			}
		};
	}

	public async Task<GetUserClaimsForOrganizationalUnitResponse> GetUserClaimsForOrganizationalUnitAsync(GetUserClaimsForOrganizationalUnitRequest request) {
		var unit = await this._organizationalUnitRepository.FindUnitByIdAsync(request.UnitId);

		if (unit is null) {
			return new GetUserClaimsForOrganizationalUnitResponse {
				Errors = new[] {GetUserClaimsForOrganizationalUnitResponse.Error.OrganizationalUnitDoesNotExist},
				Content = new GetUserClaimsForOrganizationalUnitResponse.Body {
					UserClaims = Enumerable.Empty<OrganizationalUnitUserClaim>()
				}
			};
		}

		var userClaims = await this._organizationalUnitRepository.GetUserClaimsForUnitAsync(unit);
		
		return new GetUserClaimsForOrganizationalUnitResponse {
			Succeeded = true,
			Messages = new[] {GetUserClaimsForOrganizationalUnitResponse.Message.UserClaimsReturnedSuccessfully},
			Content = new GetUserClaimsForOrganizationalUnitResponse.Body {
				UserClaims = userClaims
			}
		};
	}
}