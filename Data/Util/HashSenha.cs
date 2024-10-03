using System.Security.Cryptography;
using System.Text;
using Data.Interfaces.Util;

namespace Data.Util
{
    public class HashSenha : IHashSenha
    {
        public string Hash(string raw)
        {
            using (var algorith = SHA512.Create())
            {
                var hashedBytes = algorith.ComputeHash(Encoding.UTF8.GetBytes(raw));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
