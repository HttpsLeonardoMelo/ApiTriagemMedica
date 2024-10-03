using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.Provider
{
    public class TokenBuilder
    {
        #region Atributos
        //Encapsulando atrubutos
        private SecurityKey securityKey = null;
        private string subject = "";
        private string issuer = "";
        private string audience = "";
        private Dictionary<string, string> claims = new Dictionary<string, string>();
        private int expiryMinutes = 0;
        #endregion

        #region Metodos
        public TokenBuilder AddSecurityKey(SecurityKey securityKey)
        {
            this.securityKey = securityKey;
            return this;
        }

        public TokenBuilder AddSubject(string subject)
        {
            this.subject = subject;
            return this;
        }

        public TokenBuilder AddIssuer(string issuer)
        {
            this.issuer = issuer;
            return this;
        }

        public TokenBuilder AddAudience(string audience)
        {
            this.audience = audience;
            return this;
        }

        public TokenBuilder AddClaim(string type, string value)
        {
            this.claims.Add(type, value);
            return this;
        }

        public TokenBuilder AddClaims(Dictionary<string, string> claims)
        {
            this.claims.Union(claims);
            return this;
        }

        public TokenBuilder AddExpiry(int expiryMinutes)
        {
            this.expiryMinutes = expiryMinutes;
            return this;
        }

        public TokenGen Builder()
        {
            EnsureArguments();


            var claims_ = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, this.subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }.Union(this.claims.Select(s => new Claim(s.Key, s.Value)));

            var token = new JwtSecurityToken
            (
                issuer : this.issuer,
                audience : this.audience,
                claims : claims_,
                expires : DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials : new SigningCredentials(
                                        this.securityKey,
                                        SecurityAlgorithms.HmacSha256)

            );

            return new TokenGen(token);

        }

        private void EnsureArguments()
        {
            if (this.securityKey == null)
                throw new ArgumentNullException("Security Key");

            if (String.IsNullOrEmpty(this.subject))
                throw new ArgumentNullException("Subject");

            if (String.IsNullOrEmpty(this.issuer))
                throw new ArgumentNullException("Issuer");

            if (String.IsNullOrEmpty(this.audience))
                throw new ArgumentNullException("Audience");
        }
        #endregion
    }
}
