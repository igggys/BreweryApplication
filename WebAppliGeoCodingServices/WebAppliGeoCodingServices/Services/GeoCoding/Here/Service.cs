using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Globalization;
using WebAppGeoCodingServices.Infrastructure;
using WebAppGeoCodingServices.Models;

namespace WebAppGeoCodingServices.Services.GeoCoding.Here
{
    public class Service : IServiceGeoCoding
    {
        private readonly GeoCodingServiceSettings _serviceSettings;
        public Service(IOptions<List<GeoCodingServiceSettings>> servicesSettings)
        {
            _serviceSettings = servicesSettings.Value.FirstOrDefault(item => item.ServiceName == "Here");
        }

        public async Task<GeoCodingServiceResult> GetAsync(string address, CultureInfo culture, HttpClient httpClient)
        {
            string url = $"{_serviceSettings.Controller}?q={Uri.EscapeDataString(address)}&apiKey={_serviceSettings.Token}&lang={culture.TwoLetterISOLanguageName}";
            try
            {
                var httpResponseMessage = await httpClient.GetAsync(url);
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string contentString = await httpResponseMessage.Content.ReadAsStringAsync();
                    Model hereResult = JsonConvert.DeserializeObject<Model>(contentString);

                    return new()
                    {
                        StatusCode = httpResponseMessage.StatusCode,
                        GeoCodingResult = hereResult.items
                        .Where(item => item.resultType == "houseNumber")
                        .Select(item =>
                        new GeoCodingResult
                        {
                            DisplayAddress = $"{item.address.street} {item.address.houseNumber}, {item.address.city}, {item.address.countryName}",
                            GeoLocation = new()
                            {
                                Latitude = item.position.lat,
                                Longitude = item.position.lng
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
