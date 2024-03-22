using Newtonsoft.Json;

namespace WebAppSendMessageService.DataAccess.Models
{
    public class TurboSmsSendMessage
    {
        [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        [JsonProperty("recipients")]
        public string[] Recipients { get; set; }

        [JsonProperty("sms")]
        public Body Sms { get; set; }

        [JsonProperty("viber", NullValueHandling = NullValueHandling.Ignore)]
        public Body Viber { get; set; }

        [JsonProperty("start_time", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? StartDate { get; set; }
    }

    public class Body
    {
        [JsonProperty("sender")]
        public string Sender { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
