using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.CrossCuttingConcerns.Logging.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers;

public class MsSqlLogger : LoggerServiceBase
{
    private readonly IConfiguration _configuration;

    public MsSqlLogger(IConfiguration configuration)
    {
        _configuration = configuration;

        var msSqlLogConfiguration = _configuration.GetSection("SerilogLogConfigurations:MsSqlConfiguration").Get<MsSqlConfiguration>()
    ?? throw new Exception(SerilogMessages.NullOptionsMessage);

        MSSqlServerSinkOptions sinkOptions = new MSSqlServerSinkOptions
        {
            TableName = msSqlLogConfiguration.TableName,
            AutoCreateSqlTable = msSqlLogConfiguration.AutoCreateSqlTable
        };

        ColumnOptions columnOptions = new();

        global::Serilog.Core.Logger logger = new LoggerConfiguration()
            .WriteTo.MSSqlServer(connectionString: msSqlLogConfiguration.ConnectionString, sinkOptions: sinkOptions, columnOptions: columnOptions)
            .CreateLogger();

        Logger = logger;
    }
}
