using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LTC_Covid.Models;
using LTCDataManager.Office;
using LTCDataManager.User;
using LTCDataModel.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using LTCDataManager.Summary;
using LTCDataModel.Office;
using Microsoft.AspNetCore.Identity;
using LTC_Covid.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using LTCDataModel.Covid;
using LTCDataManager.Covid;
using System.Text;
using DataTables.AspNetCore.Mvc.Binder;
using LTC_Covid.Helper;
using LTCDataModel.User;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http2;
using LTCDataManager.Email;
using Microsoft.AspNetCore.Hosting;
using LTCDataManager;
using System.Configuration;
using Microsoft.AspNetCore.DataProtection;

namespace LTC_Covid.Controllers
{

    public class HomeController : BaseController
    {

        private readonly IOptions<EmailManager.ElasticEmail> _email;
        private ConfigSettings _configuration;
        private Mapping _mapping;
        private gOfficeSummaryManager _gOfficeSummaryManager;
        private readonly UserManager<BusinessUserInfo> _userManager;
        private readonly SignInManager<BusinessUserInfo> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IHostingEnvironment _webHostEnvironment;
        private readonly IDataProtector _protector;

        public HomeController(IDataProtectionProvider provider, IOptions<ConfigSettings> configuration,
            IOptions<Mapping> mapping,
            UserManager<BusinessUserInfo> userManager,
            IEmailSender emailSender,
            SignInManager<BusinessUserInfo> signInManager, IHostingEnvironment webHostEnvironment, IOptions<EmailManager.ElasticEmail> email, DataProtectionPurposeStrings dataProtectionPurposeStrings) : base(webHostEnvironment)
        {
            _configuration = configuration.Value;
            _mapping = mapping.Value;
            _email = email;
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
            _gOfficeSummaryManager = new gOfficeSummaryManager(configuration);
            _protector = provider.CreateProtector(dataProtectionPurposeStrings.SubAndQueueID);
        }


        public ActionResult CovidForm(string subscriberId, string formId, bool IsNew, string queueId)
        {
            gFormCovidEntryViewModel form = new gFormCovidEntryViewModel();
            var subId = Convert.ToInt32(_protector.Unprotect(subscriberId));
            var frmId = Convert.ToInt32(_protector.Unprotect(formId));
            if (IsNew)
            {
                form = new gFormCovidEntryViewModel();
                form.FormID = frmId;
                form.SubscriberID = subId;
            }
            else
            {
                var id = Convert.ToInt32(_protector.Unprotect(queueId));
                form = gCovidManager.GetCovidFormByQueueId(id);
            }
            var subDetails = gCovidManager.GetSubscriberById(subId);
            form.FirstName = subDetails.FirstName;
            form.LastName = subDetails.LastName;
            form.LoggedInUser = UserName;
            return View(form);
        }


        //[AllowAnonymous]
        public ActionResult DeleteForm([FromBody]IdModel model)
        {
            var Id = Convert.ToInt32(_protector.Unprotect(model.Id));
            gCovidManager.Delete(Id);
            var json = new
            {
                success = true
            };
            return Json(json);
        }
        //[AllowAnonymous]
        public ActionResult CovidFormView(string queueId)
        {
            var Id = Convert.ToInt32(_protector.Unprotect(queueId));
            var form = gCovidManager.GetCovidFormByQueueId(Id);
            return View(form);
        }

        //public ActionResult CovidFormOntarioView(int subscriberId)
        //{
        //    var form = gCovidManager.GetFormInfo(subscriberId, 2);

        //    return View(form);
        //}

        //[AllowAnonymous]
        public ActionResult ViewForms()
        {

            return View();
        }
        //[AllowAnonymous]
        [HttpGet]
        public ActionResult GetAllTypes()
        {

            try
            {
                List<gFormCovidType> objViewModelList = new List<gFormCovidType>();
                objViewModelList = gCovidManager.GetAllTypes();
                return Json(objViewModelList
   .Select(e => new
   {
       ID = _protector.Protect(e.ID.ToString()),
       FormID = _protector.Protect(e.Form_ID.ToString()),
       Covid_Form_Description = e.Covid_Form_Description

   }));

            }
            catch (Exception)
            {
                return null;
            }
        }

        //[AllowAnonymous]
        [HttpPost]
        public ActionResult SendEmail([FromBody]gEmailModel model)
        {
            try
            {
                var subId = Convert.ToInt32(_protector.Unprotect(model.Id));
                var queueId = Convert.ToInt32(_protector.Unprotect(model.QueueId));

                string webRootPath = _webHostEnvironment.WebRootPath;
                var htmlFile = webRootPath + "/Html/email.html";
                var htmlPage = System.IO.File.ReadAllText(htmlFile);
                var subscriber = gCovidManager.GetSubscriberById(subId);

                if (subscriber != null)
                {
                    var form = gCovidManager.GetFormInfo(queueId, subId);
                    if (form != null)
                    {
                        string[] msgTo = new[]
                          {subscriber.EmailAddress};
                        var url = _configuration.ServerAddress + "/COVID-prescreen?API=12121123&FormID=" + queueId + "&CustomeID=" + subscriber.CustomID;

                        htmlPage = htmlPage.Replace("&Subscriber&", subscriber.FirstName + " " + subscriber.LastName).Replace("&Link&", url);

                        var id = EmailManager.Send("Covid-Form",
                          msgTo,
                          htmlPage,
                          new EmailManager.ElasticEmail
                          {
                              Email = _email.Value.Email,
                              FromName = _email.Value.FromName,
                              APIKey = _email.Value.APIKey
                          });

                        return Json(new
                        {
                            success = true,
                        });

                    }
                }

                return Json(new
                {
                    success = false,

                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,

                });
            }


        }
        //[AllowAnonymous]
        [HttpPost]
        public ActionResult SavePdf([FromBody]gformInPdfInputModel model)
        {
            try
            {
                gformInPdf form = new gformInPdf();

                string webRootPath = _webHostEnvironment.WebRootPath;
                var htmlFile = webRootPath + "/Html/form.html";
                form.PDF = Utility.GenerateFormPdf(model.PDF, htmlFile);
                form.QueueID = model.QueueID;
                form.FromTable = 1;

                int Id = gCovidManager.SavePdf(form);
                var json = new
                {
                    success = true,
                    Id = Id
                };
                return Json(json);

            }
            catch (Exception ex)
            {
                return null;
            }


        }
        //[AllowAnonymous]
        [HttpPost]
        public ActionResult Upsert([FromBody]gFormCovidEntry model)
        {
            try
            {
                model.BusinessInfo_ID = OfficeSequence;

                if (model.QueueID < 1)
                    model.CustomID = Common.GenerateCustomID();

                if (model.SubscriberID == 0)
                {
                    var subId = gCovidManager.SaveSubscriber(new gCovidSubscriber
                    {
                        BusinessInfo_ID = model.BusinessInfo_ID,
                        CustomID = Common.GenerateCustomID(),
                        EmailAddress = model.Email ?? ""
                    });
                    model.SubscriberID = subId;
                }

                int Id = gCovidManager.Save(model);
                var json = new
                {
                    success = true,
                    QueueId = Id
                };
                return Json(json);

            }
            catch (Exception ex)
            {
                return null;
            }


        }

        //[AllowAnonymous]
        [HttpGet]
        public ActionResult GetForms([DataTablesRequest] DataTablesRequest requestModel)
        {

            var objViewModelList = gCovidManager.GetCovidForms(OfficeSequence);

            var totalCount = 0;
            var filteredCount = 0;



            var query = from s in objViewModelList select s;
            totalCount = query.Count();

            #region Filtering
            //search Filters
            //if (!string.IsNullOrEmpty(requestModel.Search?.Value))
            //{
            //    var value = requestModel.Search.Value.Trim();
            //    query = query.Where(s => s.InPersonScreenDate.ToString().Contains(value) ||
            //                             s.FirstName.Contains(value) ||
            //                             s.LastName.Contains(value) ||
            //                             s.PreScreenDate.ToString().Contains(value) ||
            //                             s.Covid_Form_Description.Contains(value));
            //}

            filteredCount = query.Count();

            #endregion Filtering



            objViewModelList = query.Skip(requestModel.Start).Take(requestModel.Length).ToList();


            return Json(objViewModelList
               .Select(e => new
               {
                   //Convert.ToInt32(_protector.Unprotect(id))
                   Id = _protector.Protect(e.QueueID.ToString()),
                   FullName = e.FirstName + " " + e.LastName,
                   FormName = e.Covid_Form_Description,
                   PreScreenDate = e.IsPreScreen == true ? e.PreScreenDate.ToString("yyyy-MM-dd") : "-",
                   IsPreScreen = e.IsPreScreen,
                   InPersonScreenDate = e.IsInPersonScreen == true ? e.InPersonScreenDate.ToString("yyyy-MM-dd") : "-",
                   IsInPersonScreen = e.IsInPersonScreen,
                   FormID = _protector.Protect(e.FormID.ToString()),
                   SubscriberID = _protector.Protect(e.SubscriberID.ToString())
               })
               .ToDataTablesResponse(requestModel, totalCount, filteredCount));
        }


        public ActionResult Profile()
        {
            GenerateDropdownData();
            var model = gCovidManager.GetUserProfile(UserId);
            return View("Views/Shared/PartialViews/_Profile.cshtml", model);
        }

        public IActionResult UpdateProfile(BusinessUserInfo model)
        {
            try
            {
                gCovidManager.UpdateUserProfile(model);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        //API
        [AllowAnonymous]
        [Route("/covid-prescreen")]
        public ActionResult CovidFormPublic(int? formId, string api = "", string customId = "", string contact = "", int counter = 0, int fa = 0)
        {

            var subDetails = gCovidManager.GetSubscriberByCustomId(customId);
            if (subDetails != null)
            {

                var form = gCovidManager.GetFormInfo(subDetails.ID, formId.Value);
                if (form != null)
                {
                    form.FirstName = subDetails.FirstName;
                    form.LastName = subDetails.LastName;
                    form.CustomID = customId;


                    var id = gCovidManager.Save(new gFormCovidEntry
                    {
                        CustomID = Common.GenerateCustomID(),
                        FormID = form.FormID,
                        BusinessInfo_ID = OfficeSequence,
                        SubscriberID = subDetails.ID,
                    });

                    form.QueueID = id;

                    return View("CovidForm", form);
                }
            }
            return View("CovidForm", new gFormCovidEntryViewModel() { ContactMethod = contact, IsInPersonScreen = true });
        }



        private void GenerateDropdownData()
        {
            var selectedList = new List<int>();

            TempData["OfficeList"] = gOfficeManager.GetAllOffices().Select(i => new SelectListItem()
            {
                Text = i.ClinicName + " (" + i.Office_Number + ")",
                Value = (i.Office_Sequence != null ? i.Office_Sequence.ToString() : ""),
                Selected = selectedList.Any(x => x == i.Office_Sequence)
            });

        }

    }
}
