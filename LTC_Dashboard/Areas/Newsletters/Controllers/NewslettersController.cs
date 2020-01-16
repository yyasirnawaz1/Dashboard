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
namespace LTC_Dashboard.Areas.Newsletters.Controllers
{
    [Area("Newsletters")]
    [Authorize]
    public class NewslettersController : BaseController
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

        public NewslettersController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor accessor, IOptions<EmailManager.ElasticEmail> email) : base(hostingEnvironment)
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
        //public JsonResult GetIndustries()
        //{
        //    var objResult = new List<gIndustryModel>();

        //    try
        //    {
        //        objResult = gNewsLetterManager.GetIndustries(CurrentOfficeId);
        //        return Json(objResult);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(null);
        //    }
        //}

        //public JsonResult GetSubIndustries()
        //{
        //    var objResult = new List<gSubIndustryModel>();

        //    try
        //    {
        //        objResult = gNewsLetterManager.GetSubIndustries(CurrentOfficeId);
        //        return Json(objResult);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(null);
        //    }
        //}

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
                return Json(DateTime.Now.ToUniversalTime().ToString());
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
        public async System.Threading.Tasks.Task<JsonResult> SendNewsletterAsync([FromBody]NewsletterViewModel model)
        {
            try
            {
                int status = 2;
                if (model.ScheduledDateTime.ToUniversalTime().Date < DateTime.Now.ToUniversalTime().Date)
                {
                    model.ScheduledDateTime = DateTime.Now.ToUniversalTime();
                    status = 2;

                }
                else if (model.ScheduledDateTime.ToUniversalTime().Date> DateTime.Now.ToUniversalTime().Date)
                {
                    status = 1;
                }

                int Hour = model.ScheduledDateTime.ToUniversalTime().Hour;
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
                patient.AppointmentDate = model.ScheduledDateTime.ToUniversalTime().Date.ToString("yyyy-MM-dd");
                patient.AppointmentTime = model.ScheduledDateTime.ToUniversalTime().ToString("HH:mm");
                var emailSent = false;
                if (model.SendToSubscribers)
                {
                    SubscriberFilterParams parm = new SubscriberFilterParams() { DoctorID = UserId.ToString() };
                    var subscribers = gSubscriber.GetSubscribers(parm);
                    var pl = gNewsLetterManager.GetPatientCallList(UserId);
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
                                }, template, new EmailManager.ElasticEmail{ Email = _email.Value.Email, FromName = _email.Value.FromName, APIKey = _email.Value.APIKey});
                        emailSent = true;
                        }

                        gPatientCallList obj = new gPatientCallList()
                        {
                            Office_Sequence = OfficeSequence,
                            Branch_Number = 1,
                            Email = subscriber.EmailAddress,
                           // AppointDate = model.ScheduledDateTime.Date,
                            PatientName = SubScriberName,
                            NewsletterID = model.TemplateId,
                            SubscriberID = subscriber.Id,
                            ScheduledID = 0,
                            ErrorCode = 0,
                            EmailResult = "N",
                            PublicNewsletter = false,
                            EmailSentTime = model.ScheduledDateTime.ToUniversalTime(),
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
                        patient  = new gPatientOfficeInfo();
                        patient.FirstName = model.Email;
                        patient.LastName = model.Email;
                        patient.Name = model.Email;
                        
                    }
                    patient.AppointmentDate = model.ScheduledDateTime.ToUniversalTime().Date.ToString("yyyy-MM-dd");
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
                                Email = _email.Value.Email, FromName = _email.Value.FromName,
                                APIKey = _email.Value.APIKey
                            });
                         emailSent = true;
                    }
                    gPatientCallList obj = new gPatientCallList()
                    {
                        Office_Sequence = OfficeSequence,
                        Branch_Number = 1,
                        Email = model.Email,
                        //AppointDate = model.ScheduledDateTime.Date,
                        PatientName = model.Email,
                        NewsletterID = model.TemplateId,
                        SubscriberID = 0,
                        ErrorCode = 0,
                        PublicNewsletter = false,
                        EmailSentTime =  model.ScheduledDateTime.ToUniversalTime(),
                        EmailReceiveTime = DateTime.Now,
                        Account =  UserId,
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
                gNewsLetterManager.MakeDefault(model.LetterID,model.IsDefault);
                return Json(gNewsLetterManager.MakeDefault(model.LetterID,model.IsDefault));
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
                gNewsLetterManager.CopySystiemTemplate(model.TemplateId, model.Title, CurrentBranchId, OfficeSequence, UserId);
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
                //var user = (CustomMembershipUser)Membership.GetUser(User.Identity.Name, true);
                model.DoctorID = UserId;
                model.Office_Sequence = OfficeSequence;
                model.Branch_number = CurrentBranchId;


                return Json(gNewsLetterManager.SaveUserNewsTemplate(model));
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }

    }
}