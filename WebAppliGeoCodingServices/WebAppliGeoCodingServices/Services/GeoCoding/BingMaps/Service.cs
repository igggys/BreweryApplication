using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Globalization;
using System.Text.RegularExpressions;
using WebAppGeoCodingServices.Infrastructure;
using WebAppGeoCodingServices.Models;
using WebAppGeoCodingServices.Services.GeoCoding.GoogleMaps;

namespace WebAppGeoCodingServices.Services.GeoCoding.BingMaps
{
    public class Service : IServiceGeoCoding
    {
        private readonly GeoCodingServiceSettings _serviceSettings;
        public Service(IOptions<List<GeoCodingServiceSettings>> servicesSettings)
        {
            _serviceSettings = servicesSettings.Value.FirstOrDefault(item => item.ServiceName == "BingMaps");
        }

        public async Task<GeoCodingServiceResult> GetAsync(string address, CultureInfo culture, HttpClient httpClient)
        {
            string url = $"{_serviceSettings.Controller}?q={Uri.EscapeDataString(address)}&maxRes=20&key={_serviceSettings.Token}";
            try
            {
                var httpResponseMessage = await httpClient.GetAsync(url);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string contentString = await httpResponseMessage.Content.ReadAsStringAsync();
                    Model micResult = JsonConvert.DeserializeObject<Model>(contentString);
                    
                    return new()
                    {
                        StatusCode = httpResponseMessage.StatusCode,
                        GeoCodingResult = micResult.resourceSets[0].resources
                        .Where(item => item.entityType == "Address")
                        .Select(item =>
                        new GeoCodingResult
                        {
                            DisplayAddress = Regex.Replace(item.address.formattedAddress, $", ?[0-9]* ?{item.address.locality} ?,", $", {item.address.locality},"),
                            GeoLocation = new()
                            {
                                Latitude = item.point.coordinates[0],
                                Longitude = item.point.coordinates[1]
                            }
                        }).ToArray()
                    };
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
