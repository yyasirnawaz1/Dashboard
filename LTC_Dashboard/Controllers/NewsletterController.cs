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
using LTCDataManager.User;
using System.Security.Claims;

namespace LTC_Dashboard.Controllers
{
    [Authorize]
    public class NewsletterController : NewsletterBaseController
    {
        private readonly IOptions<EmailManager.ElasticEmail> _email;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
 
        // GET: Newsletters
        private readonly string blankImage = "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gAABAgQITK+AYHV6z9bOCBAgQIAAAQIECBAgQIAAAQIECBBoSUCw2hKssgQIECBAgAABAgQIECBAgAABAgQITK+AYHV6z9bOCBAgQIAAAQIECBAgQIAAAQIECBBoSUCw2hKssgQIECBAgAABAgQIECBAgAABAgQITK+AYHV6z9bOCBAgQIAAAQIECBAgQIAAAQIECBBoSUCw2hKssgQIECBAgAABAgQIECBAgAABAgQITK+AYHV6z9bOCBAgQIAAAQIECBAgQIAAAQIECBBoSUCw2hKssgQIECBAgAABAgQIECBAgAABAgQITK+AYHV6z9bOCBAgQIAAAQIECBAgQIAAAQIECBBoSUCw2hKssgQIECBAgAABAgQIECBAgAABAgQITK+AYHV6z9bOCBAgQIAAAQIECBAgQIAAAQIECBBoSUCw2hKssgQIECBAgAABAgQIECBAgAABAgQITK+AYHV6z9bOCBAgQIAAAQIECBAgQIAAAQIECBBoSUCw2hKssgQIECBAgAABAgQIECBAgAABAgQITK+AYHV6z9bOCBAgQIAAAQIECBAgQIAAAQIECBBoSUCw2hKssgQIECBAgAABAgQIECBAgAABAgQITK+AYHV6z9bOCBAgQIAAAQIECBAgQIAAAQIECBBoSUCw2hKssgQIECBAgAABAgQIECBAgAABAgQITK+AYHV6z9bOCBAgQIAAAQIECBAgQIAAAQIECBBoSUCw2hKssgQIECBAgAABAgQIECBAgAABAgQITK+AYHV6z9bOCBAgQIAAAQIECBAgQIAAAQIECBBoSUCw2hKssgQIECBAgAABAgQIECBAgAABAgQITK+AYHV6z9bOCBAgQIAAAQIECBAgQIAAAQIECBBoSeB/AZUOJX55GTSTAAAAAElFTkSuQmCC";
        public ActionResult Index()
        {


            @ViewBag.OfficeName = OfficeName;
            gNewsLetterManager.CreateDefaultParadigmNewsletter(OfficeSequence);
            return View();
        }



        [AllowAnonymous]
        [Route("Newsletter/{id}")]
        public async System.Threading.Tasks.Task<IActionResult> WithoutLoginAsync(string id)
        {
            // Step 1 get default user 
            var res = gUserModuleManager.NewsletterOfficeInfo(id);
            string returnUrl = Url.Content("/Identity/Account/Login");

            if (res != null)
            {
                var defaultUser = gUserModuleManager.GetDefaultUser((int)res.Office_Sequence);
                if (defaultUser != null)
                {
                    var user = await _userManager.FindByEmailAsync(defaultUser.Email);
                    if (user != null)
                    {

                        await _signInManager.SignOutAsync();

                        var result = await _signInManager.PasswordSignInAsync(defaultUser.Email, defaultUser.Password, false, lockoutOnFailure: true);
                        if (result.Succeeded)
                        {
                            //await _userManager.AddClaimAsync(user, new Claim("ModuleType", "NewsLetter"));

                            #region Cookie

                            if ( Request.Cookies["ModuleRestriction"] != null)
                                  Response.Cookies.Delete("ModuleRestriction");
                            var options = new CookieOptions
                            {
                                Expires = DateTime.Now.AddMinutes(60),
                                IsEssential = true
                            };

                            Response.Cookies.Append("ModuleRestriction", "Newsletter", options);
                            #endregion
                            return LocalRedirect("/Newsletter/Home");
                        }
                        else
                            return LocalRedirect(returnUrl);
                    }
                    else
                        return LocalRedirect(returnUrl);
                }
            }
            return View();
        }
        private IHttpContextAccessor _accessor;

        public NewsletterController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor accessor, IOptions<EmailManager.ElasticEmail> email, UserManager<ApplicationUser> userManager,
                             SignInManager<ApplicationUser> signInManager) : base(hostingEnvironment)
        {
            _accessor = accessor;
            _email = email;
            _userManager = userManager;
            _signInManager = signInManager;
            

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
        public JsonResult GetArticleTypes()
        {
            var objResult = new List<gArticleCategories>();

            try
            {
                objResult = gNewsLetterManager.GetArticleCategories();
                return Json(objResult);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
        [HttpPost]
        public JsonResult UpdateArticle([FromBody]gArticleModelTest model)
        {
            try
            {

                model.ContentImage = model.ContentImage.Replace("data:image/png;base64,", "");
                gNewsLetterManager.UpdateArticle(model);
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
        [HttpPost]
        public JsonResult UpdateNewsletter([FromBody]gLetterModelTest model)
        {
            try
            {

                model.ContentImage = model.ContentImage.Replace("data:image/png;base64,", "");
                gNewsLetterManager.UpdateLetter(model);
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
        public JsonResult GetArticles()
        {
            var objResult = new List<gArticleViewModel>();

            try
            {
                objResult = gNewsLetterManager.GetArticles();
                //foreach (var item in objResult)
                //{
                //    item.ContentWithDefaultStyle = item.ContentWithDefaultStyle.Replace("http://ltcdashboard.azurewebsites.net/", "https://localhost:44380/");
                //}

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
        [HttpPost]
        public JsonResult moveArticle([FromBody] MoveArticleModel model)
        {
            try
            {

                gNewsLetterManager.MoveArticle(model.CategoryId, model.ArticleId);
                return Json(true);
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
                var res = easternTime.Month.ToString() + "/" + easternTime.Day.ToString() + "/" + easternTime.Year.ToString() + " " + easternTime.TimeOfDay.ToString();
                return Json(res);
                // return Json(easternTime.ToString());
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

                var emailSent = false;
                if (model.SendToSubscribers)
                {
                    SubscriberFilterParams parm = new SubscriberFilterParams() { Office_Sequence = OfficeSequence.ToString() };
                    var subscribers = gSubscriber.GetSubscribers(parm);

                    foreach (var subscriber in subscribers)
                    {
                        string msgID = null;
                        String SubScriberName = subscriber.Salutation + " " + subscriber.FirstName + " " + subscriber.LastName;
                        string familyList = subscriber.Salutation + "," + subscriber.LastName + "," + subscriber.FirstName;
                        patient.FirstName = subscriber.FirstName;
                        patient.LastName = subscriber.LastName;
                        patient.Name = SubScriberName;
                        patient.Salutation = subscriber.Salutation;
                        var template = EmailManager.StringExtention.ClearTemplate(office, Template, patient, familyList);
                        if (status == 2)
                        {

                            msgID = EmailManager.Send(Template.TemplateTitle,
                                new[]
                                {
                                    subscriber.EmailAddress
                                }, template, new EmailManager.ElasticEmail { Email = _email.Value.Email, FromName = _email.Value.FromName, APIKey = _email.Value.APIKey });
                            emailSent = true;
                        }

                        gPatientCallList obj = new gPatientCallList()
                        {
                            Office_Sequence = OfficeSequence,
                            EmailContent = template,
                            EmailSubject = Template.TemplateTitle,
                            Email = subscriber.EmailAddress,
                            PatientName = SubScriberName,
                            NewsletterID = model.TemplateId,
                            SubscriberID = subscriber.Id,
                            DateToSendEmail = model.ScheduledDateTime,
                            // EmailSentOnDate = DateSetting.ValidDate,
                            Account = UserId,
                            Status = status,
                            EmailSent = emailSent,
                            MessageID = msgID
                        };

                        obj.MessageID = msgID;
                        gNewsLetterManager.SendSubscriber(obj);


                    }

                }
                else
                {
                    SubscriberFilterParams parm = new SubscriberFilterParams() { Office_Sequence = OfficeSequence.ToString() };

                    var subscriber = gSubscriber.GetByEmail(model.Email);
                    if (subscriber != null)
                    {
                        string msgID = null;
                        string familyList = subscriber.Salutation + "," + subscriber.LastName + "," + subscriber.FirstName;
                        patient = new gPatientOfficeInfo();
                        //if (patient == null)
                        //{
                        //    patient = new gPatientOfficeInfo();
                        patient.FirstName = subscriber.FirstName;
                        patient.LastName = subscriber.LastName;
                        patient.Name = model.Email;

                        //}
                        patient.AppointmentDate = model.ScheduledDateTime.Date.ToString("yyyy-MM-dd");
                        patient.AppointmentTime = model.ScheduledDateTime.ToString("HH:mm");
                        var template = EmailManager.StringExtention.ClearTemplate(office, Template, patient, familyList);
                        if (status == 2)
                        {
                            msgID = EmailManager.Send(Template.TemplateTitle,
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
                            EmailContent = template,
                            EmailSubject = Template.TemplateTitle,
                            Email = model.Email,
                            PatientName = model.Email,
                            NewsletterID = model.TemplateId,
                            SubscriberID = subscriber.Id,
                            DateToSendEmail = model.ScheduledDateTime,
                            Account = UserId,
                            Status = status,
                            EmailSent = emailSent
                        };
                        if (emailSent)
                            obj.EmailSentOnDate = model.ScheduledDateTime;


                        obj.MessageID = msgID;
                        gNewsLetterManager.SendSubscriber(obj);

                    }
                    else
                    {
                        return Json(false);
                    }


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
                byte[] contentImage = null;
                if (model.ContentImageString != "data:,")
                {
                    model.ContentImageString = model.ContentImageString.Replace("data:image/png;base64,", "");
                    contentImage = Convert.FromBase64String(model.ContentImageString);
                }
                else
                {
                    contentImage = Convert.FromBase64String(blankImage);
                }
                gNewsLetterManager.CopyArticle(model.TemplateId, model.ArticleId, model.Title, OfficeSequence, model.Content, contentImage);
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
        public JsonResult SaveNewsletterEditor([FromBody] gSaveUserTemplateModel model)
        {
            try
            {

                if (model.ContentImageString != "data:,")
                {
                    model.ContentImageString = model.ContentImageString.Replace("data:image/png;base64,", "");
                    byte[] byteArray = Convert.FromBase64String(model.ContentImageString);
                    model.ContentImage = byteArray;
                }
                else
                {
                    model.ContentImage = Convert.FromBase64String(blankImage);
                }
                model.Office_Sequence = OfficeSequence;
                gSaveUserTemplate obj = new gSaveUserTemplate()
                {
                    ContentImage = model.ContentImage,
                    EmbeddedNewsletter = model.EmbeddedNewsletter,
                    IsDefault = model.IsDefault,
                    IsParadigmNewsletter = model.IsParadigmNewsletter,
                    LetterID = model.LetterID,
                    MainBodymarkup = model.MainBodymarkup,
                    ModificationDate = model.ModificationDate,
                    Office_Sequence = model.Office_Sequence,
                    TemplateSourceMarkup = model.TemplateSourceMarkup,
                    TemplateTitle = model.TemplateTitle,
                    ThumbnailPath = model.ThumbnailPath,
                    TypeID = model.TypeID,
                    CategoryID = model.CategoryID

                };

                return Json(gNewsLetterManager.SaveUserNewsTemplate(obj));
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

        #region cookie
        public string Get(string key)
        {
            return Request.Cookies[key];
        }
        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        public void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);
            Response.Cookies.Append(key, value, option);
        }
        /// <summary>  
        /// Delete the key  
        /// </summary>  
        /// <param name="key">Key</param>  
        public void Remove(string key)
        {
            Response.Cookies.Delete(key);
        }
        #endregion
    }
}