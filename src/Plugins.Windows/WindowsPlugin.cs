using Microsoft.Win32;
using Splat;

namespace Accolades.Brann.Plugins.Windows;

public class WindowsPlugin : Plugin, IEnableLogger
{
    private const string UninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
    private const string DisplayKeyName = "DisplayName";
    
    private readonly List<string> _applications;
    
    /// <summary>
    /// Initialize a new <see cref="WindowsPlugin"/>.
    /// </summary>
    public WindowsPlugin() : base("Windows")
    {
        _applications = new List<string>();
    }

    public override Task<IEnumerable<ISuggestion>> Search(string search, CancellationToken cancellationToken)
    {
        var suggestions = _applications.Select(app => new AppSuggestion(app)).Cast<ISuggestion>().ToList();

        return Task.FromResult<IEnumerable<ISuggestion>>(suggestions);
    }

    public override Task Initialize()
    {
        if (!OperatingSystem.IsWindows())
        {
            this.Log().Debug("Not on windows, passing...");
            return Task.CompletedTask;
        }
        
        using var uninstallKey = Registry.LocalMachine.OpenSubKey(UninstallKey);

        if (uninstallKey == null)
        {
            this.Log().Debug("The uninstall key is null");
            return Task.CompletedTask;
        }
        
        foreach (var subKey in uninstallKey.GetSubKeyNames())
        {
            var name = GetSubKeyValue(uninstallKey, subKey, DisplayKeyName);

            if (name is not null)
            {
                _applications.Add(name);
            }
        }

        return Task.CompletedTask;
    }
    
    private static string? GetSubKeyValue(RegistryKey key, string subKeyName, string property)
    {
#pragma warning disable CA1416
        try
        {
            using var sk = key.OpenSubKey(subKeyName);

            var name = sk?.GetValue(property)?.ToString();

            return name ?? null;
        }
        catch (Exception)
        {
            return null;
        }
#pragma warning restore CA1416
    }
}