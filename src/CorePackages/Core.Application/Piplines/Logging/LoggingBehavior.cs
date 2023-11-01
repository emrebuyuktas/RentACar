using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Core.Application.Piplines.Logging
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ILoggableRequest
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LoggerServiceBase _logger;

        public LoggingBehavior(IHttpContextAccessor httpContextAccessor, LoggerServiceBase logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            List<LogParameter> parameters = new()
            {
                new LogParameter {Type = request.GetType().Name, Value = request}
            };

            LogDetail logDetail = new()
            {
                MethodName = next.Method.Name,
                Parameters = parameters,
                User = _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "Anonymous"
            };

            _logger.Info(JsonSerializer.Serialize(logDetail));

            return await next();
        }
    }
}