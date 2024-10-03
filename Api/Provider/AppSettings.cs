namespace Api.Provider
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string TimeKey { get; set; }
        public int ExpiracaoMinutos { get; set; }
        public int ExpiracaoTimeKeyMinutos { get; set; }
        public string Emissor { get; set; }
        public string ValidoEm { get; set; }
    }
}
