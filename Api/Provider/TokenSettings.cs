namespace Api.Provider
{
    public class TokenSettings
    {
        public string Secret { get; set; }
        public int ExpirationMinutes { get; set; }
        public int? RenewalMinutes { get; set; }
        public string Issuer { get; set; }
        public string ValidIn { get; set; }
    }
}
