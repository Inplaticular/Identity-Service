using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Inplanticular.IdentityService.Infrastructure.V1.Extensions; 

public static class DbContextExtensions {
	public static async Task<TEntity?> FindTrackedAsync<TEntity>(this DbContext context, TEntity entity) where TEntity : class {
		var entityType = context.Model.FindRuntimeEntityType(typeof(TEntity));
		var keyProperties = entityType?.FindPrimaryKey()?.Properties;

		if (keyProperties is null)
			return null;

		var keyValues = keyProperties.Select(prop => prop.GetGetter().GetClrValue(entity)).ToArray();
		return await context.FindAsync<TEntity>(keyValues);
	}
}