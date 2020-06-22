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
using Microsoft.AspNetCore.Hosting;
using LTCDataManager;
using LTCDataManager.Email;

namespace LTC_Covid.Controllers
{
    public class HomeController : BaseController
    {
        private const string Html = @"Dear &Subscriber& <br/>Please click the link below to view the Pre - Screen Form.
<br/>
<br/>


&Link&
<br/>
<br/>
Regards
<br/>
LTC";
        private readonly IOptions<EmailManager.ElasticEmail> _email;
        private ConfigSettings _configuration;
        private Mapping _mapping;
        private gOfficeSummaryManager _gOfficeSummaryManager;
        private readonly UserManager<BusinessUserInfo> _userManager;
        private readonly SignInManager<BusinessUserInfo> _signInManager;
        private readonly IHostingEnvironment _webHostEnvironment;

        public HomeController(IOptions<ConfigSettings> configuration,
            IOptions<Mapping> mapping,
            UserManager<BusinessUserInfo> userManager,
            SignInManager<BusinessUserInfo> signInManager, IHostingEnvironment webHostEnvironment, IOptions<EmailManager.ElasticEmail> email) : base(webHostEnvironment)
        {
            _configuration = configuration.Value;
            _mapping = mapping.Value;
            _email = email;
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
            _gOfficeSummaryManager = new gOfficeSummaryManager(configuration);
        }


        [AllowAnonymous]
        public ActionResult CovidForm(int subscriberId, int formId)
        {

            var form = gCovidManager.GetFormInfo(subscriberId, formId);
            var subDetails = gCovidManager.GetSubscriberById(subscriberId);
            form.FirstName = subDetails.FirstName;
            form.LastName = subDetails.LastName;

            return View(form);
        }
        [AllowAnonymous]
        public ActionResult CovidFormOntario(int subscriberId)
        {
            var form = gCovidManager.GetFormInfo(subscriberId, 2);

            return View(form);
        }
        [AllowAnonymous]
        public ActionResult DeleteForm([FromBody]IdModel model)
        {
            gCovidManager.Delete(model.Id);
            var json = new
            {
                success = true
            };
            return Json(json);
        }
        [AllowAnonymous]
        public ActionResult CovidFormView(int subscriberId, int formId)
        {
            var form = gCovidManager.GetFormInfo(subscriberId, formId);
            return View(form);
        }
        [AllowAnonymous]
        public ActionResult CovidFormOntarioView(int subscriberId)
        {
            var form = gCovidManager.GetFormInfo(subscriberId, 2);

            return View(form);
        }

        [AllowAnonymous]
        public ActionResult ViewForms()
        {

            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetAllTypes()
        {

            try
            {
                List<gFormCovidType> objViewModelList = new List<gFormCovidType>();
                objViewModelList = gCovidManager.GetAllTypes();

                return Json(objViewModelList);

            }
            catch (Exception)
            {
                return null;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SendEmail([FromBody]gEmailModel model)
        {
            try
            {
                var subscriber = gCovidManager.GetSubscriberById(model.Id);
                if (subscriber != null)
                {
                    var form = gCovidManager.GetFormInfo(model.QueueId, model.Id);
                    if (form != null)
                    {
                        string[] msgTo = new[]
                          {subscriber.EmailAddress};
                        var url = "https://localhost:44380/COVID-prescreen/API=12121123&FormID="+model.QueueId+"&CustomeID=" + subscriber.CustomID;

                        var emailHtml = Html.Replace("&Subscriber&", subscriber.FirstName + " " + subscriber.LastName).Replace("&Link&", url) ;

                        var id = EmailManager.Send("Covid-Form",
                          msgTo,
                          Html,
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
        [AllowAnonymous]
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
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Upsert([FromBody]gFormCovidEntry model)
        {
            try
            {
                model.BusinessInfo_ID = 1;

                if (model.QueueID < 1)
                    model.CustomID = Common.GenerateCustomID();


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

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetForms([DataTablesRequest] DataTablesRequest requestModel)
        {

            var objViewModelList = gCovidManager.GetCovidForms();

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
                   Id = e.QueueID,
                   FullName = e.FirstName + " " + e.LastName,
                   FormName = e.Covid_Form_Description,
                   PreScreenDate = e.IsPreScreen == true ? e.PreScreenDate.ToString("yyyy-MM-dd") : "-",
                   IsPreScreen = e.IsPreScreen,
                   InPersonScreenDate = e.IsInPersonScreen == true ? e.InPersonScreenDate.ToString("yyyy-MM-dd") : "-",
                   IsInPersonScreen = e.IsInPersonScreen,
                   FormID = e.FormID,
                   SubscriberID = e.SubscriberID
               })
               .ToDataTablesResponse(requestModel, totalCount, filteredCount));
        }

    }
}
