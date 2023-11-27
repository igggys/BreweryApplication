using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLib.DTO
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
