using Inplanticular.IdentityService.Core.V1.Contracts.Requests.Information;
using Inplanticular.IdentityService.Core.V1.Contracts.Responses.Information;
using Inplanticular.IdentityService.Core.V1.Dtos;
using Inplanticular.IdentityService.Core.V1.Services;
using Inplanticular.IdentityService.Core.V1.Services.Information;
using Inplanticular.IdentityService.Infrastructure.V1.Database;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Inplanticular.IdentityService.Infrastructure.V1.Services.Information; 

public class AuthenticationInformationService : IAuthenticationInformationService {
	private readonly ApplicationDbContext _applicationDbContext;
	private readonly IMappingService _mappingService;

	public AuthenticationInformationService(ApplicationDbContext applicationDbContext, IMappingService mappingService) {
		this._applicationDbContext = applicationDbContext;
		this._mappingService = mappingService;
	}
	
	public async Task<GetUsersByNameOrEmailResponse> GetUsersByNameOrEmailAsync(GetUsersByNameOrEmailRequest request) {
		if (string.IsNullOrWhiteSpace(request.UsernameEmail)) {
			return new GetUsersByNameOrEmailResponse {
				Succeeded = true,
				Messages = new[] {GetUsersByNameOrEmailResponse.Message.SuitableUsersReturned},
				Content = new GetUsersByNameOrEmailResponse.Body {
					Users = Enumerable.Empty<UserDto>()
				}
			};
		}

		var users = await this._applicationDbContext.Users
			.Where(user => user.Email.ToLower().Contains(request.UsernameEmail.ToLower()) ||
			               user.UserName.ToLower().Contains(request.UsernameEmail.ToLower()))
			.Select(user => this._mappingService.MapTo<UserDto>(user)!)
			.ToArrayAsync();

		return new GetUsersByNameOrEmailResponse {
			Succeeded = true,
			Messages = new[] {GetUsersByNameOrEmailResponse.Message.SuitableUsersReturned},
			Content = new GetUsersByNameOrEmailResponse.Body {
				Users = users
			}
		};
	}
}