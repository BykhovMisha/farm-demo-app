using FarmDemoApp.API.Models.MiddlewareModels;
using FarmDemoApp.Common.Exceptions;
using FluentValidation;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FarmDemoApp.API.Middlewares
{
    public class ValidationResponseMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            IResult? problemResult = null;

            try
            {
                await next(context);
            }
            catch (ValidationException exception)
            {
                var errors = exception.Errors.Select(x => x.ErrorMessage);
                var problemDetails = new FarmValidationProblemDetails(errors)
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Status = (int)HttpStatusCode.BadRequest,
                    Instance = context.Request.Path
                };

                problemResult = Results.Problem(problemDetails);
            }
            catch (FarmAppException exception)
            {
                var problemDetails = new FarmValidationProblemDetails(exception.Message)
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Status = (int)HttpStatusCode.BadRequest,
                    Instance = context.Request.Path
                };

                problemResult = Results.Problem(problemDetails);
            }
            catch (FarmAppNotFoundException exception)
            {
                var problemDetails = new FarmValidationProblemDetails(exception.Message)
                {
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
                    Status = (int)HttpStatusCode.NotFound,
                    Instance = context.Request.Path
                };

                problemResult = Results.NotFound(exception.Message);
            }
            finally
            {
                if (problemResult != null)
                {
                    await problemResult.ExecuteAsync(context);
                }
            }
        }
    }
}
