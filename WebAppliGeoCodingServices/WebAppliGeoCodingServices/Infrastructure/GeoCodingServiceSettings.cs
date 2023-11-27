using System.Globalization;

namespace WebAppGeoCodingServices.Infrastructure
{
    public class GeoCodingServiceSettings
    {
        public string ServiceName { get; set; }
        public string BaseUrl { get; set; }
        public string Controller { get; set; }
        public string Token { get; set; }
        public string ClassType { get; set; }
    }
}
