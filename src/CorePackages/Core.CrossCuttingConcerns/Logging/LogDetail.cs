namespace Core.CrossCuttingConcerns.Logging;

public class LogDetail
{
    public string FullName { get; set; } = string.Empty;
    public string MethodName { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public List<LogParameter> Parameters { get; set; }

    public LogDetail()
    {
        Parameters = new List<LogParameter>();
    }

    public LogDetail(string fullName, string methodName, string user, List<LogParameter> parameters) : this()
    {
        FullName = fullName;
        MethodName = methodName;
        User = user;
        Parameters = parameters;
    }
}