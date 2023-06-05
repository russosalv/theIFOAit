namespace IFOA.Exceptions;

public class MissingConfigurationException : IfoaManagedException
{
    public MissingConfigurationException(string key)
        : base($"Missing configuration for key {key}")
    {
        ErrorCode = "MISSING_CONFIGURATION";
    }
}