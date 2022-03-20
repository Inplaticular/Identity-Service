using Inplanticular.IdentityService.Core.V1.Dtos;
using Inplanticular.IdentityService.Core.V1.Services;

using Mapster;

using Microsoft.AspNetCore.Identity;

namespace Inplanticular.IdentityService.Infrastructure.V1.Services; 

public class MappingService : IMappingService {
	private TypeAdapterConfig _typeAdapterConfig;
	
	public MappingService() {
		this._typeAdapterConfig = new TypeAdapterConfig();
		this._typeAdapterConfig.NewConfig<IdentityUser, UserDto>()
			.Map(dest => dest.Username, src => src.UserName);
	}
	
	public TTo? MapTo<TTo>(object? obj) {
		return obj is null ? default : obj.Adapt<TTo>(this._typeAdapterConfig);
	}
}