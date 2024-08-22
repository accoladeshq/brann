namespace Accolades.Brann.Updater;

internal static class StringExtensions
{
    public static Uri ToUri(this string path)
    {
        return new Uri(path);
    }
}