using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using LTCDashboard.Data;
using LTCDataManager.Email;
using LTCDataManager.NewsLetter;
using LTCDataManager.Office;
using LTCDataManager.Subscriber;
using LTCDataModel.NewsLetter;
using LTCDataModel.Office;
using LTCDataModel.Subscriber;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using LTCDashboard.Controllers;
using LTCDataModel.Newsletter;
using LTCDataModel.Enums;

namespace LTC_Dashboard.Controllers
{
    [Authorize]
    public class NewsletterController : NewsletterBaseController
    {
        private readonly IOptions<EmailManager.ElasticEmail> _email;
        private readonly UserManager<ApplicationUser> _userManager;
        // GET: Newsletters
        public ActionResult Index()
        {
            @ViewBag.OfficeName = OfficeName;
            return View();
        }

        private IHttpContextAccessor _accessor;

        public NewsletterController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor accessor, IOptions<EmailManager.ElasticEmail> email) : base(hostingEnvironment)

        {
            _accessor = accessor;
            _email = email;


        }
        public JsonResult GetTemplateTypes()
        {
            var objResult = new List<gTemplateTypeModel>();

            try
            {
                objResult = gNewsLetterManager.GetTemplateTypes(OfficeSequence);
                return Json(objResult);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
        public JsonResult GetArticles()
        {
            var objResult = new List<gArticleModel>();

            try
            {
                objResult = gNewsLetterManager.GetArticles();
                
                return Json(objResult);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }


        public JsonResult GetShellTemplates()
        {
            var objResult = new List<gGetPreDefinedTemplateModel>();

            try
            {
                objResult = gNewsLetterManager.GetPreDefinedTemplates();
                return Json(objResult);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }

        public JsonResult GetSystemTemplates()
        {
            var objResult = new List<gGetPreDefinedTemplateModel>();

            try
            {
                //CurrentOfficeId
                objResult = gNewsLetterManager.GetPreDefinedTemplates();
                return Json(objResult);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }

        public JsonResult GetUserDefinedTemplates()
        {
            var objResult = new List<gGetUserDefinedTemplateModel>();

            try
            {

                objResult = gNewsLetterManager.GetUserDefinedTemplates(OfficeSequence);
                return Json(objResult);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }

        public JsonResult RemoveSelectedUserDefinedTemplate([FromBody] NewsletterViewDeleteModel model)
        {
            try
            {

                gNewsLetterManager.DeleteUserDefinedTemplate(model.tempId);
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }

        public JsonResult LoadServerTime()
        {
            try
            {
                var timeUtc = DateTime.UtcNow;
                TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
                return Json(easternTime.ToString());
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
        public class DateSetting
        {
            public static DateTime ValidDate
            {
                get
                {
                    return new DateTime(1990, 1, 1);
                }
            }
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> SendNewsletterAsync([FromBody]LTCDataModel.Newsletter.NewsletterViewModel model)
        {
            try
            {
                TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                DateTime now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, easternZone);
                int status = 2;
                if ((model.ScheduledDateTime.Date <= now.Date) && (model.ScheduledDateTime.TimeOfDay <= now.TimeOfDay))
                {
                    model.ScheduledDateTime = now;
                    status = 2;

                }
                else if (model.ScheduledDateTime > now)
                {
                    status = 1;
                }

                int Hour = model.ScheduledDateTime.Hour;
                if (Hour <= 0)
                {
                    Hour = 12;
                }

                var Template = gNewsLetterManager.GetUserDefinedTemplate(model.TemplateId);
                if (Template == null)
                {
                    return Json(null);
                }

                var office = gOfficeManager.GetOfficeName(OfficeSequence);
                var patient = new gPatientOfficeInfo();
                patient.AppointmentDate = model.ScheduledDateTime.Date.ToString("yyyy-MM-dd");
                patient.AppointmentTime = model.ScheduledDateTime.ToString("HH:mm");
                //var test = model.ScheduledDateTime.ToUniversalTime();
                //TimeSpan startTime = model.ScheduledDateTime.TimeOfDay - TimeSpan.Parse(model.Offset.Replace('+',' '));
                //var time = model.ScheduledDateTime + startTime;
                var emailSent = false;
                if (model.SendToSubscribers)
                {
                    SubscriberFilterParams parm = new SubscriberFilterParams() { Office_Sequence = OfficeSequence.ToString() };
                    var subscribers = gSubscriber.GetSubscribers(parm);
                    var pl = gNewsLetterManager.GetPatientCallList(OfficeSequence);
                    int counter = pl.Count();
                    foreach (var subscriber in subscribers)
                    {
                        String SubScriberName = subscriber.FirstName + " " + subscriber.LastName;

                        patient.FirstName = subscriber.FirstName;
                        patient.LastName = subscriber.LastName;
                        patient.Name = SubScriberName;
                        if (status == 2)
                        {
                            var template = EmailManager.StringExtention.ClearTemplate(office, Template, patient);

                            EmailManager.Send(Template.TemplateTitle,
                                new[]
                                {
                                    subscriber.EmailAddress
                                }, template, new EmailManager.ElasticEmail { Email = _email.Value.Email, FromName = _email.Value.FromName, APIKey = _email.Value.APIKey });
                            emailSent = true;
                        }

                        gPatientCallList obj = new gPatientCallList()
                        {
                            Office_Sequence = OfficeSequence,

                            Email = subscriber.EmailAddress,
                            // AppointDate = model.ScheduledDateTime.Date,
                            PatientName = SubScriberName,
                            NewsletterID = model.TemplateId,
                            SubscriberID = subscriber.Id,
                            ScheduledID = 0,
                            ErrorCode = 0,
                            EmailResult = "N",
                            PublicNewsletter = false,
                            EmailSentTime = model.ScheduledDateTime,
                            EmailReceiveTime = DateSetting.ValidDate,
                            Account = UserId,
                            Status = status,
                            EmailSent = emailSent
                        };
                        gNewsLetterManager.SendSubscriber(obj);




                        counter++;
                    }

                }
                else
                {
                    patient = gOfficeManager.GetPatientInfo(model.Email);
                    if (patient == null)
                    {
                        patient = new gPatientOfficeInfo();
                        patient.FirstName = model.Email;
                        patient.LastName = model.Email;
                        patient.Name = model.Email;

                    }
                    patient.AppointmentDate = model.ScheduledDateTime.Date.ToString("yyyy-MM-dd");
                    patient.AppointmentTime = model.ScheduledDateTime.ToString("HH:mm");
                    if (status == 2)
                    {
                        var template = EmailManager.StringExtention.ClearTemplate(office, Template, patient);

                        EmailManager.Send(Template.TemplateTitle,
                            new[]
                            {
                                model.Email
                            }, template,
                            new EmailManager.ElasticEmail
                            {
                                Email = _email.Value.Email,
                                FromName = _email.Value.FromName,
                                APIKey = _email.Value.APIKey
                            });
                        emailSent = true;
                    }
                    gPatientCallList obj = new gPatientCallList()
                    {
                        Office_Sequence = OfficeSequence,

                        Email = model.Email,
                        //AppointDate = model.ScheduledDateTime.Date,
                        PatientName = model.Email,
                        NewsletterID = model.TemplateId,
                        SubscriberID = 0,
                        ErrorCode = 0,
                        PublicNewsletter = false,
                        EmailSentTime = model.ScheduledDateTime,
                        EmailReceiveTime = DateTime.Now,
                        Account = UserId,
                        Status = status,
                        EmailSent = emailSent


                    };
                    gNewsLetterManager.SendSubscriber(obj);

                }


                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }

        [HttpPost]
        public JsonResult MakeDefault([FromBody]gMakeDefault model)
        {
            try
            {
                //   var user = (CustomMembershipUser)Membership.GetUser(User.Identity00-.Name, true);
                gNewsLetterManager.MakeDefault(model.LetterID, model.IsDefault);
                return Json(gNewsLetterManager.MakeDefault(model.LetterID, model.IsDefault));
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
        [HttpPost]
        public JsonResult CopySystemTemplate([FromBody]gCopyTemplate model)
        {
            try
            {
                //   var user = (CustomMembershipUser)Membership.GetUser(User.Identity00-.Name, true);
                gNewsLetterManager.CopySystemTemplate(model.TemplateId, model.Title, OfficeSequence);
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
        [HttpPost]
        public JsonResult CopyArticle([FromBody]gArticleTemplate model)
        {
            try
            {
                gNewsLetterManager.CopyArticle(model.TemplateId, model.ArticleId, model.Title, OfficeSequence, model.Content);
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }

        [HttpPost]
        public JsonResult DeleteSelected([FromBody]gSelectedIds model)
        {
            try
            {
                gNewsLetterManager.DeleteMultiple(model.SelectedIds);
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
        public JsonResult SaveNewsletterEditor([FromBody] gSaveUserTemplate model)
        {
            try
            {
                model.Office_Sequence = OfficeSequence;
                return Json(gNewsLetterManager.SaveUserNewsTemplate(model));
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }

        #region Home
        public ActionResult Home()
        {
            @ViewBag.OfficeName = OfficeName;
            gNewsLetterManager.CreateDefaultParadigmNewsletter(OfficeSequence);


            return View();
        }
        public ActionResult GetScheduledNewsLetterStatistics(string category, string period)
        {
            ScheduledNewsLetterStatisticsViewModel result = new ScheduledNewsLetterStatisticsViewModel();
            try
            {
                int intCategory = 3;
                if (category == "Sent")
                {
                    intCategory = 2;
                }
                else if (category == "Scheduled")
                {
                    intCategory = 1;
                }

                result.ScheduledNewsLetter = gNewsLetterManager.GetDashboard(intCategory, period, OfficeSequence);
                foreach (var item in result.ScheduledNewsLetter)
                {
                    item.SentTimeString = item.SentTime.ToString(@"yyyy-MM-dd hh:mm tt", new CultureInfo("en-US"));
                }

                result.ScheduledNewsLetter = result.ScheduledNewsLetter.OrderByDescending(c => c.SentTime).ToList();
                result.Scheduled = result.ScheduledNewsLetter.Count(s => s.Status == ScheduledNewsLetterStatus.Scheduled);
                result.Sent = result.ScheduledNewsLetter.Count(s => s.Status == ScheduledNewsLetterStatus.Sent);
            }
            catch (Exception ex)
            {
                // log
            }
            return PartialView("_ScheduledNewsLettersStatistics", result);
        }

        #endregion
    }
}