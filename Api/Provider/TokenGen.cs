using System.IdentityModel.Tokens.Jwt;

namespace Api.Provider
{
    public class TokenGen
    {
        private JwtSecurityToken token;

        internal TokenGen(JwtSecurityToken token)
        {
            this.token = token;
        }

        public DateTime ValidTo => token.ValidTo;

        public string value => new JwtSecurityTokenHandler().WriteToken(this.token);
    }
}
