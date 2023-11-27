using System.Net;

namespace WebAppGeoCodingServices.Models
{
    public class GeoCodingServiceResult
    {
        public GeoCodingResult[] GeoCodingResult { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
    public class GeoCodingResult
    {
        public string DisplayAddress { get; set; }
        public GeoLocation GeoLocation { get; set; }
    }

    public class GeoLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
