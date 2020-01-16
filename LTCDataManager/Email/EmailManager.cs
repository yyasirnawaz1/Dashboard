using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using ElasticEmailClient;
using LTCDataModel.NewsLetter;
using LTCDataModel.Office;
using static ElasticEmailClient.Api;
using static ElasticEmailClient.ApiTypes;
 
namespace LTCDataManager.Email
{
    public class EmailManager
    {
        public static class StringExtention
        {

            public static string ClearTemplate( gOfficeInfo office, gGetUserDefinedTemplateModel template, gPatientOfficeInfo patient)
            {
                StringBuilder sb = new StringBuilder(template.MainBodymarkup);
                
                sb.Replace("[OfficeName]", office?.Business_Name);
               sb.Replace("[Subscriber]",patient.Email);
               sb.Replace("[PatientName]", patient.FirstName);
                sb.Replace("[PatientSalutation]", patient.Salutation);
                sb.Replace("[PatientFirstName]", patient.FirstName);
                sb.Replace("[PatientLastName]", patient.LastName);
                sb.Replace("[AppointDate]", patient.AppointmentDate);
                sb.Replace("[AppointTime]", patient.AppointmentTime);
                sb.Replace("[ProviderName]", patient.Provider);
                sb.Replace("[Date]", DateTime.Now.Date.ToString());
                sb.Replace("[subjecttext]", template.TemplateTitle);
                sb.Replace("[OfficeEmail]", office?.OfficeEmailAddress);
                sb.Replace("[OfficeStreet]", office?.AddressLine1);
                sb.Replace("[OfficeStreet2]", office?.AddressLine2);
                sb.Replace("[OfficeStreet3]", office?.AddressLine3);
                sb.Replace("[OfficeProvince]", office?.Province);
                sb.Replace("[City]", office?.City);
                sb.Replace("[Country]", office?.Country);
                sb.Replace("[PostalCode]", office?.PostalCode);
                sb.Replace("[OfficePhone]", office?.Phone);
                sb.Replace("[Fax]", office?.Fax);

                return sb.ToString();
            }

         
        }

        public class ElasticEmail
        {
            public string Email { get; set; }
            public string APIKey { get; set; }
            public string FromName { get; set; }

        }

        
        public static void Send(string subject, string[] msgTo, string html , ElasticEmail email)
        {

            ApiKey = email.APIKey;

            var task = SendEmail(subject, email.Email, email.FromName, msgTo, html, html);

            task.ContinueWith(t =>
            {
                if (t.Result == null)
                    Console.WriteLine("Something went wrong. Check the logs.");
                else
                {
                    Console.WriteLine("MsgID to store locally: " + t.Result.MessageID); // Available only if sent to a single recipient
                    Console.WriteLine("TransactionID to store locally: " + t.Result.TransactionID);
                }
            });

            task.Wait();
        }

        public static async Task<ElasticEmailClient.ApiTypes.EmailSend> SendEmail(string subject, string fromEmail, string fromName, string[] msgTo, string html, string text)
        {
            try
            {
                return await ElasticEmailClient.Api.Email.SendAsync(subject, fromEmail, fromName, msgTo: msgTo, bodyHtml: html, bodyText: text);
            }
            catch (Exception ex)
            {
                if (ex is ApplicationException)
                    Console.WriteLine("Server didn't accept the request: " + ex.Message);
                else
                    Console.WriteLine("Something unexpected happened: " + ex.Message);

                return null;
            }
        }
    }
}
