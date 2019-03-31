using System;
using System.Runtime.InteropServices;
using System.Security;

namespace CarnGo.Security
{
    public static class SecureStringHelpers
    {
        public static string ConvertToString(this SecureString value)
        {
            if (value == null)
                return string.Empty;
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
        public static SecureString ConvertToSecureString(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            var secure = new SecureString();
            secure.Clear();
            foreach (char c in value)
            {
                secure.AppendChar(c);
            }

            return secure;
        }
    }
}