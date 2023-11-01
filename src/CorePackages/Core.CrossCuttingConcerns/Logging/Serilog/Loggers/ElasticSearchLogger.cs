using Microsoft.Extensions.Configuration;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using Serilog;
using System.Reflection;
using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers;

public class ElasticSearchLogger : LoggerServiceBase
{
    public ElasticSearchLogger(IConfiguration configuration)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var indexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}";


        var logConfiguration = configuration
            .GetSection("SerilogLogConfigurations:ElasticSearchConfiguration")
            .Get<ElasticSearchConfiguration>();

        Logger = new LoggerConfiguration().WriteTo
            .Elasticsearch(
            new ElasticsearchSinkOptions(new Uri(logConfiguration.ConnectionString))
            {
                AutoRegisterTemplate = true,
                CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                IndexFormat = indexFormat
            }
            ).CreateLogger();
    }
}