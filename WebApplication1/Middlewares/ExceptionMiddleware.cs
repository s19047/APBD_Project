
using AdvertApi.ExceptionHandling;
using AdvertApi.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace TasksApiTest_s19047.MiddleWares
{
	public class ExceptionMiddleware
	{
		
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				// call next middleware
				await _next(context); 
			}
			catch (Exception e)
			{
				await HandleException(context, e);
			}

		}

		private Task HandleException(HttpContext context, Exception e)
		{
			context.Response.ContentType ="application/json";
			if ((e is IncorrectPasswordException) || (e is UserAlreadyExistsException) || (e is InvalidTokenException) || (e is BuildingsOnDifferentStreetException) )
			{
				return context.Response.WriteAsync(new ErrorDetails
				{
					StatusCode = StatusCodes.Status400BadRequest,
					Message = e.Message

				}.ToString());

			}else if( e is UserDoesNotExistException){
				return context.Response.WriteAsync(new ErrorDetails
				{
					StatusCode = StatusCodes.Status404NotFound,
					Message = e.Message

				}.ToString());
			}
			else
			{
				return context.Response.WriteAsync(new ErrorDetails
				{
					StatusCode = StatusCodes.Status500InternalServerError,
					Message = "Oops Something went wrong \n"+e.Message

				}.ToString());
			}

		}
	}
}
