namespace Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;

public class FileLogConfiguration
{
    public string FolderPath { get; set; }

    public FileLogConfiguration(string folderPath)
    {
        FolderPath = folderPath;
    }
}