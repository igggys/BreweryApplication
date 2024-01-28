using Microsoft.Extensions.Configuration;
using System.Text;

namespace WebAppSendMessageService.Clients
{
    public class TurboSmsClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _turboSmsApiKey;

        public TurboSmsClient(string turboSmsApiKey)
        {
            _turboSmsApiKey = turboSmsApiKey;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.turbosms.ua/");
        }

        public async Task<TurboSmsResult> SendAsync(string phoneNumber, string message)
        {
            var requestData = new
            {
                action = "sendSMS",
                sms = new
                {
                    sender = "BrewTime",
                    destination = phoneNumber,
                    text = message
                }
            };

            var content = new StringContent(requestData.ToString(), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _turboSmsApiKey);

            var response = await _httpClient.PostAsync("message/send", content);

            if (response.IsSuccessStatusCode)
            {
                // Processing of successful message sending
                return new TurboSmsResult { Success = true };
            }
            else
            {
                // Message sending error handling
                return new TurboSmsResult { Success = false };
            }
        }
    }

    public class TurboSmsResult
    {
        public bool Success { get; set; }
        // Additional properties for handling the dispatch result
    }
}
