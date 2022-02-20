namespace Inplanticular.IdentityService.Core.V1.Options; 

public class JwtIssuingOptions {
	public const string AppSettingsKey = nameof(JwtIssuingOptions);
	
	public string Secret { get; set; }
	public TimeSpan ExpiringTime { get; set; }
}