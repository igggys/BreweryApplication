using InfrastructureLib.DTO;
using System.Globalization;

namespace WebApi_GisService.Services
{
    public interface IServiceGeoCoding
    {
        Task<GeoCodingServiceResult> GetAsync(string address, CultureInfo culture, HttpClient httpClient);
    }
}
