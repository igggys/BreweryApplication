namespace WebAppGeoCodingServices.Services.GeoCoding.TomTom
{
    public class Model
    {
        public Summary summary { get; set; }
        public Result[] results { get; set; }
    }

    public class Result
    {
        public string type { get; set; }
        public string id { get; set; }
        public double score { get; set; }
        public MatchConfidence matchConfidence { get; set; }
        public double dist { get; set; }
        public Address address { get; set; }
        public Position position { get; set; }
        public List<Mapcode> mapcodes { get; set; }
        public Viewport viewport { get; set; }
        public EntryPoint[] entryPoints { get; set; }
        public AddressRanges addressRanges { get; set; }
        public DataSources dataSources { get; set; }
    }
    public class GeoBias
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }
    public class Summary
    {
        public string query { get; set; }
        public string queryType { get; set; }
        public int queryTime { get; set; }
        public int numResults { get; set; }
        public int offset { get; set; }
        public int totalResults { get; set; }
        public int fuzzyLevel { get; set; }
        public GeoBias geoBias { get; set; }
    }
    public class Address
    {
        public string streetNumber { get; set; }
        public string streetName { get; set; }
        public string municipalitySubdivision { get; set; }
        public string municipality { get; set; }
        public string countrySecondarySubdivision { get; set; }
        public string countryTertiarySubdivision { get; set; }
        public string countrySubdivision { get; set; }
        public string postalCode { get; set; }
        public string extendedPostalCode { get; set; }
        public string countryCode { get; set; }
        public string country { get; set; }
        public string countryCodeISO3 { get; set; }
        public string freeformAddress { get; set; }
        public string countrySubdivisionName { get; set; }
        public string localName { get; set; }
    }
    public class AddressRanges
    {
        public string rangeLeft { get; set; }
        public string rangeRight { get; set; }
        public From from { get; set; }
        public To to { get; set; }
    }
    public class BtmRightPoint
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }
    public class DataSources
    {
        public Geometry geometry { get; set; }
    }
    public class EntryPoint
    {
        public string type { get; set; }
        public Position position { get; set; }
    }
    public class From
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }
    public class Geometry
    {
        public string id { get; set; }
    }
    public class Mapcode
    {
        public string type { get; set; }
        public string fullMapcode { get; set; }
        public string territory { get; set; }
        public string code { get; set; }
    }
    public class MatchConfidence
    {
        public int score { get; set; }
    }
    public class Position
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }
    public class To
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }
    public class TopLeftPoint
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }
    public class Viewport
    {
        public TopLeftPoint topLeftPoint { get; set; }
        public BtmRightPoint btmRightPoint { get; set; }
    }

    public class Error
    {
        public string errorText { get; set; }
        public DetailedError detailedError { get; set; }
        public string httpStatusCode { get; set; }
    }

    public class DetailedError
    {
        public string code { get; set; }
        public string message { get; set; }
        public string target { get; set; }
    }
}
