using Inplanticular.IdentityService.Core.V1.Services;

using Mapster;

namespace Inplanticular.IdentityService.Infrastructure.V1.Services; 

public class MappingService : IMappingService {
	public TTo? MapTo<TTo>(object? obj) {
		return obj is null ? default : obj.Adapt<TTo>();
	}
}