namespace Accolades.Brann.WixGenerator;

public static class WixHelper
{
    public static string FormatId(string? prefix, string name)
    {
        var uniquePart = Guid.NewGuid().ToString().Substring(31,5).ToUpper();
        var validName = name.Replace("@", "").Replace('.', '_').Replace('-', '_').Replace('{', '_').Replace('}', '_').Trim('"');

        var result = prefix is null ? $"{validName}_{uniquePart}" : $"{prefix}_{validName}_{uniquePart}";

        while (result.Length > 72)
        {
            var lastUnderscorePosition = validName.LastIndexOf('_');
            validName = validName.Substring(0, lastUnderscorePosition);
            
            result = prefix is null ? $"{validName}_{uniquePart}" : $"{prefix}_{validName}_{uniquePart}";
        }

        return result;
    }

}