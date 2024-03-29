using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using WebAppSendMessageService.DataAccess.Interfaces;
using WebAppSendMessageService.DataAccess.Models;

namespace WebAppSendMessageService.DataAccess.Services
{
    public class TurboSmsService : ISmsService
    {
        private IConfiguration _conf;
        private readonly string _authToken;
        private readonly string _sender;
        private readonly string _url;

        public TurboSmsService(IConfiguration configuration)
        {
            _conf = configuration;
            _authToken = _conf["TurboSms:AuthToken"];
            _sender = _conf["TurboSms:Sender"];
            _url = _conf["TurboSms:Url"];
        }

        public SmsResponceStatus SendMessage(SendNotification message)
        {
            try
            {           
                string json = JsonConvert.SerializeObject(new TurboSmsSendMessage
                {
                    Recipients = new string[] { message.Phone },
                    // TODO: required to send a combined Viber and SMS message
                    //Viber = new Body
                    //{
                    //    Sender = _sender,
                    //    Text = message.Body
                    //},
                    Sms = new Body
                    {
                        Sender = _sender,
                        Text = message.Body
                    }
                });

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(_url);

                httpWebRequest.Headers["Authorization"] = $"Bearer {_authToken}";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }

                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();

                    return new SmsResponceStatus
                    {
                        ResponceCode = httpResponse.StatusCode,
                        ResponceStatus = httpResponse.StatusDescription,
                        RequestDateTime = DateTimeOffset.Now
                    };
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it) $"An error occurred while sending the message: {ex.Message}"
                return null;
            }
        }
    }
}
