using System.Security.Claims;

using Inplanticular.IdentityService.Core.V1.Options;

namespace Inplanticular.IdentityService.Core.V1.Services; 

public interface IJwtIssuingService {
	string CreateToken(JwtIssuingOptions options, IEnumerable<Claim> claims);
	IEnumerable<Claim>? GetClaimsFromToken(string token);
	bool IsValidToken(string token);
}