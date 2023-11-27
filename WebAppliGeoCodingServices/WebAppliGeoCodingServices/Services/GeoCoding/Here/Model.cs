namespace WebAppGeoCodingServices.Services.GeoCoding.Here
{
    public class Model
    {
        public List<Result> items { get; set; }
    }

    public class Access
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Address
    {
        public string label { get; set; }
        public string countryCode { get; set; }
        public string countryName { get; set; }
        public string stateCode { get; set; }
        public string state { get; set; }
        public string countyCode { get; set; }
        public string county { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string subdistrict { get; set; }
        public string street { get; set; }
        public List<string> streets { get; set; }
        public string block { get; set; }
        public string subblock { get; set; }
        public string postalCode { get; set; }
        public string houseNumber { get; set; }
        public string building { get; set; }
    }

    public class Block
    {
        public double start { get; set; }
        public double end { get; set; }
        public string value { get; set; }
        public string qq { get; set; }
    }

    public class Building
    {
        public double start { get; set; }
        public double end { get; set; }
        public string value { get; set; }
        public string qq { get; set; }
    }

    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool primary { get; set; }
    }

    public class City
    {
        public double start { get; set; }
        public double end { get; set; }
        public string value { get; set; }
        public string qq { get; set; }
    }

    public class Country
    {
        public double start { get; set; }
        public double end { get; set; }
        public string value { get; set; }
        public string qq { get; set; }
    }

    public class CountryInfo
    {
        public string alpha2 { get; set; }
        public string alpha3 { get; set; }
    }

    public class County
    {
        public double start { get; set; }
        public double end { get; set; }
        public string value { get; set; }
        public string qq { get; set; }
    }

    public class District
    {
        public double start { get; set; }
        public double end { get; set; }
        public string value { get; set; }
        public string qq { get; set; }
    }

    public class FieldScore
    {
        public double country { get; set; }
        public double countryCode { get; set; }
        public double state { get; set; }
        public double stateCode { get; set; }
        public double county { get; set; }
        public double countyCode { get; set; }
        public double city { get; set; }
        public double district { get; set; }
        public double subdistrict { get; set; }
        public List<double> streets { get; set; }
        public double block { get; set; }
        public double subblock { get; set; }
        public double houseNumber { get; set; }
        public double postalCode { get; set; }
        public double building { get; set; }
        public double unit { get; set; }
        public double placeName { get; set; }
        public double ontologyName { get; set; }
    }

    public class FoodType
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool primary { get; set; }
    }

    public class HouseNumber
    {
        public double start { get; set; }
        public double end { get; set; }
        public string value { get; set; }
        public string qq { get; set; }
    }

    public class Result
    {
        public string title { get; set; }
        public string id { get; set; }
        public string politicalView { get; set; }
        public string resultType { get; set; }
        public string houseNumberType { get; set; }
        public string addressBlockType { get; set; }
        public string localityType { get; set; }
        public string administrativeAreaType { get; set; }
        public Address address { get; set; }
        public Position position { get; set; }
        public List<Access> access { get; set; }
        public double distance { get; set; }
        public MapView mapView { get; set; }
        public List<Category> categories { get; set; }
        public List<FoodType> foodTypes { get; set; }
        public bool houseNumberFallback { get; set; }
        public TimeZone timeZone { get; set; }
        public Scoring scoring { get; set; }
        public Parsing parsing { get; set; }
        public List<StreetInfo> streetInfo { get; set; }
        public CountryInfo countryInfo { get; set; }
    }

    public class MapView
    {
        public double west { get; set; }
        public double south { get; set; }
        public double east { get; set; }
        public double north { get; set; }
    }

    public class OntologyName
    {
        public double start { get; set; }
        public double end { get; set; }
        public string value { get; set; }
        public string qq { get; set; }
    }

    public class Parsing
    {
        public List<PlaceName> placeName { get; set; }
        public List<Country> country { get; set; }
        public List<State> state { get; set; }
        public List<County> county { get; set; }
        public List<City> city { get; set; }
        public List<District> district { get; set; }
        public List<Subdistrict> subdistrict { get; set; }
        public List<Street> street { get; set; }
        public List<Block> block { get; set; }
        public List<Subblock> subblock { get; set; }
        public List<HouseNumber> houseNumber { get; set; }
        public List<PostalCode> postalCode { get; set; }
        public List<Building> building { get; set; }
        public List<SecondaryUnit> secondaryUnits { get; set; }
        public List<OntologyName> ontologyName { get; set; }
    }

    public class PlaceName
    {
        public double start { get; set; }
        public double end { get; set; }
        public string value { get; set; }
        public string qq { get; set; }
    }

    public class Position
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class PostalCode
    {
        public double start { get; set; }
        public double end { get; set; }
        public string value { get; set; }
        public string qq { get; set; }
    }

    public class Scoring
    {
        public double queryScore { get; set; }
        public FieldScore fieldScore { get; set; }
    }

    public class SecondaryUnit
    {
        public double start { get; set; }
        public double end { get; set; }
        public string value { get; set; }
        public string qq { get; set; }
    }

    public class State
    {
        public double start { get; set; }
        public double end { get; set; }
        public string value { get; set; }
        public string qq { get; set; }
    }

    public class Street
    {
        public double start { get; set; }
        public double end { get; set; }
        public string value { get; set; }
        public string qq { get; set; }
    }

    public class StreetInfo
    {
        public string baseName { get; set; }
        public string streetType { get; set; }
        public bool streetTypePrecedes { get; set; }
        public bool streetTypeAttached { get; set; }
        public string prefix { get; set; }
        public string suffix { get; set; }
        public string direction { get; set; }
        public string language { get; set; }
    }

    public class Subblock
    {
        public double start { get; set; }
        public double end { get; set; }
        public string value { get; set; }
        public string qq { get; set; }
    }

    public class Subdistrict
    {
        public double start { get; set; }
        public double end { get; set; }
        public string value { get; set; }
        public string qq { get; set; }
    }

    public class TimeZone
    {
        public string name { get; set; }
        public string utcOffset { get; set; }
    }

    public class Error
    {
        public int status { get; set; }
        public string title { get; set; }
        public string code { get; set; }
        public string cause { get; set; }
        public string action { get; set; }
        public string correlationId { get; set; }
        public string requestId { get; set; }
    }
}
