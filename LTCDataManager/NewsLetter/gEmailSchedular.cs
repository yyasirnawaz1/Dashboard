using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ElasticEmailClient;
using LTCDataManager.Email;
using LTCDataManager.NewsLetter;
using LTCDataManager.Office;
using LTCDataManager.Subscriber;
using LTCDataModel.Office;
using LTCDataModel.Subscriber;

namespace DentalDataManager.DataManager.NewsLetter
{
    public class gEmailSchedular
    {
        public static void GetTemplates(string SenderEmail, string ApiKey, string FromName)
        {
            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, easternZone);
            var emailList = gNewsLetterManager.GetPatientCallList();
            //var finalList = emailList.Where(e => e.DateToSendEmail <= now);
            foreach (var newsletter in emailList)
            {
                try
                {
                    var office = gOfficeManager.GetOfficeName(newsletter.office_Sequence);
                    var subscriber = gSubscriber.GetByEmail(newsletter.Email);
                    var patient = new gPatientOfficeInfo();
                    String SubScriberName = subscriber.Salutation + " " + subscriber.FirstName + " " + subscriber.LastName;
                    string familyList = subscriber.Salutation + "," + subscriber.LastName + "," + subscriber.FirstName;
                    patient.FirstName = subscriber.FirstName;
                    patient.LastName = subscriber.LastName;
                    patient.Name = SubScriberName;
                    patient.Salutation = subscriber.Salutation;

                    if (String.IsNullOrEmpty(newsletter.EmailContent))
                    {
                        if (newsletter.PublicNewsletter)
                        {
                            var article = gNewsLetterManager.GetArticle(newsletter.NewsletterId);
                            if (article != null)
                            {
                                if (subscriber != null)
                                {
                                    newsletter.EmailContent = EmailManager.StringExtention.ClearArticleTemplate(office, article, patient, familyList);
                                }
                            }
                        }
                        else
                        {
                            var Template = gNewsLetterManager.GetUserDefinedTemplate(newsletter.NewsletterId);
                            if (Template != null)
                            {

                                if (subscriber != null)
                                {
                                    newsletter.EmailContent = EmailManager.StringExtention.ClearTemplate(office, Template, patient, familyList);
                                }
                            }
                        }
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder(newsletter.EmailContent);
                        sb.Replace("&OfficeName&", office?.ClinicName);
                        sb.Replace("&Subscriber&", patient.Email);
                        sb.Replace("&PatientName&", patient.FirstName);
                        sb.Replace("&PatientSalutation&", patient.Salutation);
                        sb.Replace("&PatientFirstName&", patient.FirstName);
                        sb.Replace("&PatientLastName&", patient.LastName);
                        sb.Replace("&AppointDate&", patient.AppointmentDate);
                        sb.Replace("&AppointTime&", patient.AppointmentTime);
                        sb.Replace("&ProviderName&", patient.Provider);
                        sb.Replace("&Date&", DateTime.Now.Date.ToString());
                        sb.Replace("&subjecttext&", newsletter.EmailSubject);
                        sb.Replace("&OfficeEmail&", office?.OfficeEmailAddress);
                        sb.Replace("&OfficeStreet&", office?.AddressLine1);
                        sb.Replace("&OfficeStreet2&", office?.AddressLine2);
                        sb.Replace("&OfficeStreet3&", office?.AddressLine3);
                        sb.Replace("&OfficeProvince&", office?.Province);
                        sb.Replace("&City&", office?.City);
                        sb.Replace("&Country&", office?.Country);
                        sb.Replace("&PostalCode&", office?.PostalCode);
                        sb.Replace("&OfficePhone&", office?.Phone);
                        sb.Replace("&OfficeWebAddress&", office?.WebSite);
                        sb.Replace("&Fax&", office?.Fax);
                        sb.Replace("&FamilyList&", familyList);

                        newsletter.EmailContent = sb.ToString();
                    }
                    var messageId = EmailManager.Send(newsletter.EmailSubject,
                            new[]
                            {
                            newsletter.Email
                            }, newsletter.EmailContent, new EmailManager.ElasticEmail { Email = SenderEmail, FromName = FromName, APIKey = ApiKey });
                    if (messageId != null)
                    {
                        gNewsLetterManager.UpdatePatientCallList(newsletter.ID, messageId);
                    }
                }
                catch (Exception)
                {
                    newsletter.NoOfRetry++;
                    gNewsLetterManager.UpdatePatientCallListNotSent(newsletter.ID, newsletter.NoOfRetry);
                }

            }
        }
    }
}
