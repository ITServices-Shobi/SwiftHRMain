using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vonage;
using Vonage.Request;
using Microsoft.Extensions.Configuration;

namespace SwiftHR.Models
{
    public class SMSUtility
    {
        private IConfiguration _config;

        public SMSUtility(IConfiguration _configuration)
        {
            _config = _configuration;
        }
        //Sending Sms through Vonage account...
        public string SendVonageSms(string messageString, string toPhone, string fromText)
        {
            var SMSCountryCode= _config["SMSSettings:SMSCountryCode"];
            var APIKey = _config["SMSSettings:APIKey"];
            var APISecret = _config["SMSSettings:APISecret"];
            
            var credentials = Credentials.FromApiKeyAndSecret(
            APIKey,
            APISecret
            );

            var VonageClient = new VonageClient(credentials);

            var response = VonageClient.SmsClient.SendAnSms(new Vonage.Messaging.SendSmsRequest()
            {
                To = SMSCountryCode+toPhone,
                From = fromText,
                Text = messageString
            });
            return response.Messages[0].StatusCode.ToString();
        }

    }
}
