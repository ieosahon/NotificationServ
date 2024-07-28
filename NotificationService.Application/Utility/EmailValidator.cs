global using System.Globalization;
global using System.Text.RegularExpressions;

namespace NotificationService.Application.Utility;

public static class EmailValidator
{


    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                RegexOptions.None, TimeSpan.FromMilliseconds(200));
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private static string DomainMapper(Match match)
    {
        var idn = new IdnMapping();
        var domainName = match.Groups[2].Value;
        domainName = idn.GetAscii(domainName);
        return match.Groups[1].Value + domainName;
    }
}