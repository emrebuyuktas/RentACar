using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.CrossCuttingConcerns.Logging.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers;

public class FileLogger : LoggerServiceBase
{
    private readonly IConfiguration _configuration;

    public FileLogger(IConfiguration configuration)
    {
        _configuration = configuration;

        var fileLogConfiguration = _configuration.GetSection("SerilogLogConfigurations:FileLogConfiguration").Get<FileLogConfiguration>()
            ?? throw new Exception(SerilogMessages.NullOptionsMessage);

        var logFilePath = string.Format(format: "{0}{1}", arg0: Directory.GetCurrentDirectory() + fileLogConfiguration.FolderPath, arg1: ".txt");

        Logger = new LoggerConfiguration()
            .WriteTo.File(path: logFilePath, rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, 
            fileSizeLimitBytes: 5000000, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
            .CreateLogger();
    }
}