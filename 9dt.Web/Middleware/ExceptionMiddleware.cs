using _9dt.Web.Exceptions;
using _9dt.Web.Middleware.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace _9dt.Web.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BadRequestException ex)
            {
                HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, ex.Message, ex.ErrorData);
            }
            catch (ConflictException ex)
            {
                HandleExceptionAsync(httpContext, HttpStatusCode.Conflict, ex.Message, null);
            }
            catch (GoneException ex)
            {
                HandleExceptionAsync(httpContext, HttpStatusCode.Gone, ex.Message, null);
            }
            catch (NotFoundException ex)
            {
                HandleExceptionAsync(httpContext, HttpStatusCode.NotFound, ex.Message, null);
            }
            catch (Exception ex)
            {   // 500 
                _logger.LogError($"Exception: {ex}");
                HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        private static async void HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message, object errorData)
        {
            var error = new Error
            {
                Code = (int)statusCode,
                Message = message,
                ErrorData = errorData,
            };

            var result = new ObjectResult(error);
            //result.ContentType = "application/json";
            result.StatusCode = (int)statusCode;
            await result.ExecuteResultAsync(new ActionContext
            {
                HttpContext = context
            });
        }
    }
}
