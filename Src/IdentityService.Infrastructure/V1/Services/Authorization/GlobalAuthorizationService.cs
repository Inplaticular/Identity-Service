using Inplanticular.IdentityService.Core.V1.Contracts.Requests.Authorization;
using Inplanticular.IdentityService.Core.V1.Contracts.Responses.Authorization;
using Inplanticular.IdentityService.Core.V1.Options;
using Inplanticular.IdentityService.Core.V1.Services;
using Inplanticular.IdentityService.Core.V1.Services.Authentication;
using Inplanticular.IdentityService.Core.V1.Services.Authorization;
using Inplanticular.IdentityService.Core.V1.ValueObjects;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Inplanticular.IdentityService.Infrastructure.V1.Services.Authorization; 

public class GlobalAuthorizationService : IGlobalAuthorizationService {
	private readonly UserManager<IdentityUser> _userManager;
	private readonly ILoginService<IdentityUser> _loginService;
	private readonly IJwtIssuingService _jwtIssuingService;
	private readonly ILogger<GlobalAuthorizationService> _logger;
	private readonly JwtIssuingOptions _jwtIssuingOptions;

	public GlobalAuthorizationService(UserManager<IdentityUser> userManager, ILoginService<IdentityUser> loginService, IJwtIssuingService jwtIssuingService, IOptions<JwtIssuingOptions> jwtIssuingOptions, ILogger<GlobalAuthorizationService> logger) {
		this._userManager = userManager;
		this._loginService = loginService;
		this._jwtIssuingService = jwtIssuingService;
		this._logger = logger;
		this._jwtIssuingOptions = jwtIssuingOptions.Value;
	}

	public AuthorizeResponse AuthorizeUser(AuthorizeRequest request) {
		if (string.IsNullOrWhiteSpace(request.Token))
			return new AuthorizeResponse() {Errors = new[] {AuthorizeResponse.Error.InvalidToken}};

		try {
			this._jwtIssuingService.ValidateToken(request.Token);
			return new AuthorizeResponse() {
				Succeeded = true,
				Messages = new[] {AuthorizeResponse.Message.Authorized},
				Content = new AuthorizeResponse.Body() {
					Authorized = true,
				}
			};
		} catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.AuthorizeUser)} threw an exception");
			
			return new AuthorizeResponse() {
				Succeeded = true,
				Messages = new[] {AuthorizeResponse.Message.Unauthorized},
				Errors = new[] {
					e is SecurityTokenExpiredException
						? AuthorizeResponse.Error.ExpiredToken
						: AuthorizeResponse.Error.InvalidToken
				}
			};
		}
	}

	public async Task<AuthorizeGlobalRoleResponse> AuthorizeUserGlobalRoleAsync(AuthorizeGlobalRoleRequest request) {
		var authTokenResponse = this.AuthorizeUser(request);

		if (authTokenResponse.Content is null || !authTokenResponse.Content.Authorized) {
			return new AuthorizeGlobalRoleResponse() {
				Succeeded = authTokenResponse.Succeeded,
				
				Messages = authTokenResponse.Messages,
				Errors = authTokenResponse.Errors
			};
		}

		var user = await this._loginService.GetUserForTokenAsync(request.Token);

		if (user is null) {
			return new AuthorizeGlobalRoleResponse() {
				Succeeded = true,
				Messages = new[] {AuthorizeResponse.Message.Unauthorized},
				Errors = new[] {AuthorizeGlobalRoleResponse.Error.UserNotFound}
			};
		}

		var isInRole = await this._userManager.IsInRoleAsync(user, request.GlobalRole);

		return new AuthorizeGlobalRoleResponse() {
			Succeeded = true,
			Messages = new[] {isInRole ? AuthorizeResponse.Message.Authorized : AuthorizeResponse.Message.Unauthorized},
			Errors = isInRole ? Enumerable.Empty<Info>() : new[] {AuthorizeGlobalRoleResponse.Error.UserNotInRole},
			Content = new AuthorizeGlobalRoleResponse.Body() {
				Authorized = isInRole,
			}
		};
	}
}