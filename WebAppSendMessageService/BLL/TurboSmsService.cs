using Newtonsoft.Json;
using System.Net;
using WebAppSendMessageService.Dto;
using WebAppSendMessageService.Interfaces;
using WebAppSendMessageService.Models;

namespace WebAppSendMessageService.BLL
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


        public PhoneConfirmationStatus SendMessage(SendSmsDto message)
        {
            string json = JsonConvert.SerializeObject(new TurboSmsSendMessageDto
            {
                Recipients = new string[] { message.Phone },
                Sms = new Body
                {
                    Sender = _sender,
                    Text = message.Code.ToString()
                }
            });

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_url);

            httpWebRequest.Headers["Authorization"] = $"Bearer {_authToken}";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                return new PhoneConfirmationStatus
                {
                    ResponceCode = httpResponse.StatusCode,
                    ResponceStatus = httpResponse.StatusDescription,
                    RequestDateTime = DateTimeOffset.Now,
                    Code = message.Code
                };
            }
        }

        public SmsResponceStatus SendMessage(SendNotificationDto message)
        {
            string json = JsonConvert.SerializeObject(new TurboSmsSendMessageDto
            {
                Recipients = new string[] { message.Phone },
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

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_url);

            httpWebRequest.Headers["Authorization"] = $"Bearer {_authToken}";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                return new SmsResponceStatus
                {
                    ResponceCode = httpResponse.StatusCode,
                    ResponceStatus = httpResponse.StatusDescription,
                    RequestDateTime = DateTimeOffset.Now
                };
            }
        }
    }
}
