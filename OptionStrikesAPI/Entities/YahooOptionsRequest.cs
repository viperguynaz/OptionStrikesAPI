namespace OptionStrikes.Entities
{
    public class YahooOptionsRequest
    {
        private readonly string BaseUrl = "https://viperoptions.azurewebsites.net/api/";

        public string Url => $"{BaseUrl}{Ticker.ToUpperInvariant()}/{ExpirationDate}";

        public long? ExpirationDate { get; set; }
        public string Version { get; set; }
        public string Ticker { get; set; }
        public YahooOptionsRequest(string ticker, long? expirationDate = null, string version = "v7")
        {
            Ticker = ticker;
            ExpirationDate = expirationDate;
            Version = version;
        }
    }
}
