namespace Inplanticular.IdentityService.Core.V1.Extensions; 

public static class ExceptionExtensions {
	public static IEnumerable<Exception> GetExceptionHierarchy(this Exception exception) {
		var ex = exception;

		yield return ex;
		while (ex.InnerException is not null)
			yield return ex = ex.InnerException;
	}
}