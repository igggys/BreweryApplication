using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Globalization;
using WebAppGeoCodingServices.Infrastructure;
using WebAppGeoCodingServices.Models;

namespace WebAppGeoCodingServices.Services.GeoCoding.GoogleMaps
{
    public class Service : IServiceGeoCoding
    {
        private readonly GeoCodingServiceSettings _serviceSettings;
        private readonly string[] cultures = new[] { "zh-CN", "zh-HK", "zh-TW", "en-AU", "en-GB", "fr-CA", "pt-BR", "pt-PT", "es-419" };
        private readonly Dictionary<string, string> _messagesByStatus = new()
        {
            { "OK", "The address was successfully parsed and at least one geocode was returned." },
            { "ZERO_RESULTS", "Was passed a non-existent address." },
            { "OVER_DAILY_LIMIT", "The API key is missing or invalid or Billing has not been enabled on your account or A self-imposed usage cap has been exceeded or The provided method of payment is no longer valid." },
            { "OVER_QUERY_LIMIT", "Over quota." },
            { "REQUEST_DENIED", "The request was denied." },
            { "INVALID_REQUEST", "The query is missing." },
            { "UNKNOWN_ERROR", "The request could not be processed due to a server error. The request may succeed if you try again." }
        };
        public Service(IOptions<List<GeoCodingServiceSettings>> servicesSettings)
        {
            _serviceSettings = servicesSettings.Value.FirstOrDefault(item => item.ServiceName == "GoogleMaps");
        }
        public async Task<GeoCodingServiceResult> GetAsync(string address, CultureInfo culture, HttpClient httpClient)
        {
            string url;
            if (cultures.Any(item => item == culture.Name))
            {
                url = $"{_serviceSettings.Controller}?address={Uri.EscapeDataString(address)}&key={_serviceSettings.Token}&language={culture.Name}";
            }
            else
            {
                url = $"{_serviceSettings.Controller}?address={Uri.EscapeDataString(address)}&key={_serviceSettings.Token}&language={culture.TwoLetterISOLanguageName}";
            }

            try
            {
                var httpResponseMessage = await httpClient.GetAsync(url);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string contentString = await httpResponseMessage.Content.ReadAsStringAsync();
                    Model googleResult = (JsonConvert.DeserializeObject<Model>(contentString));
                    if (googleResult.status == "OK" || googleResult.status == "ZERO_RESULTS")
                    {
                        return new()
                        {
                            StatusCode = httpResponseMessage.StatusCode,
                            GeoCodingResult = googleResult.results.
                            Where(item => item.types.Contains("street_address") || item.types.Contains("street_number")).
                            Select(item =>
                            new GeoCodingResult
                            {
                                DisplayAddress = item.formatted_address,
                                GeoLocation = new()
                                {
                                    Latitude = item.geometry.location.lat,
                                    Longitude = item.geometry.location.lng
                                }
                            }).ToArray()
                        };
                    }
                    else
                    {
                        if (_messagesByStatus.Keys.Contains(googleResult.status))
                        {
                            throw new Exception($"Service error - {_messagesByStatus[googleResult.status]}");
                        }
                        throw new Exception($"Service error - unknown request status");
                    }

                }
                else
                {
                    return new() { StatusCode = httpResponseMessage.StatusCode, GeoCodingResult = null };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
