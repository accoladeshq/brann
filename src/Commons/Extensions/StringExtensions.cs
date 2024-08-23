namespace Accolades.Brann.Commons.Extensions;

public static class StringExtensions
{
    public static Uri ToUri(this string path)
    {
        return new Uri(path);
    }
}