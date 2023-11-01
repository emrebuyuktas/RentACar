using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Core.CrossCuttingConcerns.Exceptions.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly HttpExceptionHandler _httpExceptionHandler;
    private readonly LoggerServiceBase _loggerServiceBase;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor, LoggerServiceBase loggerServiceBase)
    {
        _next = next;
        _httpExceptionHandler = new HttpExceptionHandler();
        _httpContextAccessor = httpContextAccessor;
        _loggerServiceBase = loggerServiceBase;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await LogException(context, exception);
            await HandleExceptionAsync(context.Response, exception);
        }
    }

    private async Task LogException(HttpContext context, Exception exception)
    {
        List<LogParameter> parameters = new()
        {
            new LogParameter{Type = context.GetType().Name, Value = exception.ToString()},
        };

        LogDetailWithException logDetail = new()
        {
            ExceptionMessage = exception.Message,
            MethodName = _next.Method.Name,
            Parameters = parameters,
            User = _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "Anonymous"
        };

        _loggerServiceBase.Error(JsonSerializer.Serialize(logDetail));

        await Task.CompletedTask;
    }

    private Task HandleExceptionAsync(HttpResponse response, Exception exception)
    {
        response.ContentType = "application/json";
        _httpExceptionHandler.Response = response;
        return _httpExceptionHandler.HandleExceptionAsync(exception);
    }
}