using System.Globalization;
using WebAppGeoCodingServices.Models;

namespace WebAppGeoCodingServices.Services.GeoCoding
{
    public interface IServiceGeoCoding
    {
        Task<GeoCodingServiceResult> GetAsync(string address, CultureInfo culture, HttpClient httpClient);
    }
}