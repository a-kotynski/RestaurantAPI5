using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RestaurantAPI5.Exceptions;
using System;
using System.Threading.Tasks;

namespace RestaurantAPI5.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            Logger = logger;
        }

        public ILogger<ErrorHandlingMiddleware> Logger { get; }


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context); // if an exception occurs a catch block write that into our logger
            }
            catch(ForbidException forbidException)
            {
                context.Response.StatusCode = 403;
            }
            catch(BadRequestException badRequestException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (Exception e) // access to logger is created by injection in this classe's ctor
            {

                Logger.LogError(e, e.Message);

                context.Response.StatusCode = 500; // overwriting response status code
                await context.Response.WriteAsync("Something went wrong"); // overwriting a response received by the client
            }
        }
    }
}
 