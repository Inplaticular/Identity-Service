using Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authentication;
using Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authentication;
using Inplanticular.IdentityService.Core.V1.Enums;
using Inplanticular.IdentityService.Core.V1.Services.Authentication;
using Inplanticular.IdentityService.Core.V1.ValueObjects;
using Inplanticular.IdentityService.Infrastructure.V1.Database;

using Microsoft.AspNetCore.Identity;

namespace Inplanticular.IdentityService.Infrastructure.V1.Services.Authentication; 

public class SignUpService : ISignUpService {
	private readonly ApplicationDbContext _dbContext;
	private readonly UserManager<IdentityUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;

	public SignUpService(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) {
		this._dbContext = dbContext;
		this._userManager = userManager;
		this._roleManager = roleManager;
	}
	
	public async Task<SignUpResponse> SignUpUserAsync(SignUpRequest request) {
		var user = new IdentityUser() {
			UserName = request.Username,
			Email = request.Email
		};

		await using (var transaction = await this._dbContext.Database.BeginTransactionAsync()) {
			var creationResult = await this._userManager.CreateAsync(user, request.Password);
		
			if (!creationResult.Succeeded) {
				return new SignUpResponse {
					Errors = creationResult.Errors.Select(error => new Info() {
						Code = error.Code,
						Description = error.Description
					})
				};
			}

			if (!await this._roleManager.RoleExistsAsync(GlobalRoles.User.ToString()))
				await this._roleManager.CreateAsync(new IdentityRole(GlobalRoles.User.ToString()));

			await this._userManager.AddToRoleAsync(user, GlobalRoles.User.ToString());
			await transaction.CommitAsync();
		}
		
		return new SignUpResponse {
			Succeeded = true,
			Messages = new[] {SignUpResponse.Message.SignedUp}
		};
	}
}