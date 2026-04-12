using System.Runtime.InteropServices;
using System.Security;

namespace InvestLens.Common.Helpers;

public static class PasswordHelper
{
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
    }

    public static bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }

    public static string GetPasswordAsString(SecureString? securePassword)
    {
        if (securePassword is null) return string.Empty;

        var ptr = Marshal.SecureStringToBSTR(securePassword);
        try
        {
            return Marshal.PtrToStringBSTR(ptr);
        }
        finally
        {
            Marshal.ZeroFreeBSTR(ptr);
        }
    }
}