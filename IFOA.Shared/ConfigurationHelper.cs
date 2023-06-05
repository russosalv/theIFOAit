using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace IFOA.Shared;

public static class ConfigurationHelper
{
    /// <summary>
    /// This Extract data from launchSettings.json if application is launched in debug mode
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <param name="forceResearchOnEnvironments"> apply research in enviroment variables also if application is not in debug</param>
    /// <returns></returns>
    public static string TryGetConfigurationValue(
        this IConfiguration configuration, 
        string key, 
        bool forceResearchOnEnvironments = false)
    {
        var value = string.Empty;

        var keyWithUnderscores = key.WithUnderscores();
        var keyWithColumns = key.WithoutColumns();

        var configurationKey = configuration[key];
        var configurationKeyWithUnderscores = configuration[keyWithUnderscores];
        var configurationKeyWithColumns = configuration[keyWithColumns];

        if (Debugger.IsAttached || forceResearchOnEnvironments)
        {
            var valueDebug1 = Environment.GetEnvironmentVariable(key);
            var valueDebug2 = Environment.GetEnvironmentVariable(keyWithUnderscores);
            var valueDebug3 = Environment.GetEnvironmentVariable(keyWithColumns);

            value = valueDebug1 ?? 
                    valueDebug2 ?? 
                    valueDebug3 ?? 
                    configurationKey ?? 
                    configurationKeyWithUnderscores ?? 
                    configurationKeyWithColumns;
        }
        else
        {
            value = configurationKey ?? configurationKeyWithUnderscores ?? configurationKeyWithColumns;
        }

        return value;
    }

    public static string WithUnderscores(this string value)
    {
        return value.Replace(":", "__");
    }

    public static string WithoutColumns(this string value)
    {
        return value.Replace("__", ":");
    }
}