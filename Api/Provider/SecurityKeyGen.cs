using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Api.Provider
{
    public class SecurityKeyGen
    {
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}
