﻿using Inplanticular.IdentityService.Core.V1.Contracts.Responses;
using Inplanticular.IdentityService.Core.V1.ValueObjects;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Inplanticular.IdentityService.WebAPI.V1.Extensions;

public static class ControllerBaseExtensions {
	/// <summary>
	/// Validates the model state of a given <see cref="ControllerBase"/> instance
	/// and creates a response containing the error information of the validation process, if the model state is invalid.
	/// </summary>
	/// <param name="controller">The <see cref="ControllerBase"/> instance assigned to the extension method.</param>
	/// <param name="response">The created response. Null if the model state is valid.</param>
	/// <typeparam name="TResponse">The type of the response the method should create. Has to inherit from <see cref="BaseResponse"/></typeparam>
	/// <returns>True if the model state is valid, otherwise false</returns>
	public static bool HasValidModelState<TResponse>(this ControllerBase controller, out TResponse? response) where TResponse : BaseResponse {
		if (controller.ModelState.IsValid) {
			response = null;
			return true;
		}

		response = Activator.CreateInstance<TResponse>();
		response.Errors = controller.ModelState.Values.SelectMany(value => value.Errors).Select(error => new Info {
			Code = error.Exception?.GetType().Name ?? "",
			Description = error.ErrorMessage
		});

		return false;
	}

	/// <summary>
	/// Creates an <see cref="IActionResult"/> with status code 500 based on a given exception.
	/// It passes a response of type <typeparamref name="TResponse"/> to the action result.
	/// </summary>
	/// <param name="controller">The <see cref="ControllerBase"/> instance assigned to the extension method.</param>
	/// <param name="exception">The exception the response gets its information from.</param>
	/// <typeparam name="TResponse">The type of the response the method should create. Has to inherit from <see cref="BaseResponse"/></typeparam>
	/// <returns>The created status 500 error code.</returns>
	public static IActionResult InternalServerError<TResponse>(this ControllerBase controller, Exception exception) where TResponse : BaseResponse {
		var response = Activator.CreateInstance<TResponse>();
		response.Errors = new[] {
			new Info {
				Code = exception.GetType().Name,
				Description = exception.Message
			}
		};
		
		return controller.StatusCode(500, response);
	}
}