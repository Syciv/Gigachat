using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Security;

namespace Chatt.Services
{
    public static class HashService
    {
         public static byte[] GetSalt()
        {
            byte[] buf = new byte[32];

            using(var pr = new RNGCryptoServiceProvider())
            {
                pr.GetBytes(buf);
            }
            return buf;
        }

        public static byte[] GetHash(string password)
        {
            return DigestUtilities.CalculateDigest("SHA256", Encoding.Default.GetBytes(password));
        }
    }
}
