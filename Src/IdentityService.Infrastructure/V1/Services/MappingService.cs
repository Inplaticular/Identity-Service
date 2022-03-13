using Mapster;

namespace Inplanticular.IdentityService.Infrastructure.V1.Services; 

public class MappingService {
	public TTo? MapTo<TTo>(object? obj) {
		return obj is null ? default : obj.Adapt<TTo>();
	}
}