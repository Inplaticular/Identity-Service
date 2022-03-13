namespace Inplanticular.IdentityService.Core.V1.Services; 

public interface IMappingService {
	TTo? MapTo<TTo>(object obj);
}