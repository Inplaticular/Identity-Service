using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Inplanticular.IdentityService.Core.V1.Options;
using Inplanticular.IdentityService.Core.V1.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Inplanticular.IdentityService.Infrastructure.V1.Services; 

public class JwtIssuingService : IJwtIssuingService {
	private readonly JwtBearerOptions _jwtBearerOptions;
	
	public JwtIssuingService(IOptions<JwtBearerOptions> jwtBearerOptions) {
		this._jwtBearerOptions = jwtBearerOptions.Value;
	}

	public string CreateToken(JwtIssuingOptions options, IEnumerable<Claim> claims) {
		var secretBytes = Encoding.ASCII.GetBytes(options.Secret);
		var tokenHandler = new JwtSecurityTokenHandler();

		var tokenDescriptor = new SecurityTokenDescriptor() {
			Subject = new ClaimsIdentity(claims),
			Expires = DateTime.UtcNow + options.ExpiringTime,
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretBytes), SecurityAlgorithms.HmacSha512Signature)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}

	public IEnumerable<Claim>? GetClaimsFromToken(string token) {
		var tokenHandler = new JwtSecurityTokenHandler();
		var securityToken = tokenHandler.ReadToken(token);

		return securityToken is not JwtSecurityToken jwtSecurityToken ? null : jwtSecurityToken.Claims;
	}

	public bool IsValidToken(string token) {
		try {
			var tokenHandler = new JwtSecurityTokenHandler();
			_ = tokenHandler.ValidateToken(token, this._jwtBearerOptions.TokenValidationParameters, out _);
			return true;
		} catch (Exception) {
			return false;
		}
	}
}