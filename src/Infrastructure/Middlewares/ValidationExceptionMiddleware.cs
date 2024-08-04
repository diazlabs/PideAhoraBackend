using Domain.Common;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Middlewares
{
    public sealed class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ValidationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException exception)
            {
                var response = new Response<string>
                {
                    Ok = false,
                    GeneralErrors = ["Error al validar"]
                };

                if (exception.Errors is not null)
                {
                    response.Errors = exception.Errors
                        .GroupBy(x => x.PropertyName)
                        .Select(x => new Dictionary<string, string[]>()
                        {
                            { x.Key, x.Select(x => x.ErrorMessage).ToArray() }
                        }).ToList();
                }

                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
