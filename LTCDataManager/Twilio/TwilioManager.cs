using LTCDataModel.Configurations;
using System;
using System.Collections.Generic;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace LTCDataManager.Twilio
{
    public static class TwilioManager
    {
        public static void SendSms(TwilioSettings twilioSettings, string phoneNumber, string url)
        {
            try
            {
                TwilioClient.Init(twilioSettings.Account, twilioSettings.Password);

                var message = MessageResource.Create(
                    body: $"Please use {url} to login to physician portal",
                    from: new PhoneNumber(twilioSettings.FromPhone),
                    to: new PhoneNumber(phoneNumber)
                );
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

    }
}
