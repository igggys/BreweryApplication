using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Globalization;
using WebAppGeoCodingServices.Infrastructure;
using WebAppGeoCodingServices.Models;

namespace WebAppGeoCodingServices.Services.GeoCoding.TomTom
{
    public class Service : IServiceGeoCoding
    {
        private readonly GeoCodingServiceSettings _serviceSettings;
        public Service(IOptions<List<GeoCodingServiceSettings>> servicesSettings)
        {
            _serviceSettings = servicesSettings.Value.FirstOrDefault(item => item.ServiceName == "TomTom");
        }

        public async Task<GeoCodingServiceResult> GetAsync(string address, CultureInfo culture, HttpClient httpClient)
        {
            string language = culture.Name.StartsWith("ar-") ? "ar" : culture.Name;
            string url = $"{_serviceSettings.Controller}/{Uri.EscapeDataString(address)}.json?key={_serviceSettings.Token}&language={language}";
            try
            {
                var httpResponseMessage = await httpClient.GetAsync(url);
                string contentString = await httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Model result = JsonConvert.DeserializeObject<Model>(contentString);

                    return new()
                    {
                        StatusCode = httpResponseMessage.StatusCode,
                        GeoCodingResult = result.results.Where(item => item.type == "Point Address").Select(item =>
                        new GeoCodingResult
                        {
                            DisplayAddress = $"{item.address.streetName} {item.address.streetNumber}, {item.address.localName}, {item.address.country}",
                            GeoLocation = new()
                            {
                                Latitude = item.position.lat,
                                Longitude = item.position.lon
                            }
                        }).ToArray(),
                        Message = string.Empty
                    };
                }
                else
                {
                    Error error = JsonConvert.DeserializeObject<Error>(contentString);
                    return new()
                    {
                        StatusCode = (System.Net.HttpStatusCode)System.Enum.Parse(typeof(System.Net.HttpStatusCode), error.httpStatusCode),
                        GeoCodingResult = null,
                        Message = error.errorText
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
