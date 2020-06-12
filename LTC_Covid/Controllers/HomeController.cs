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

namespace LTC_Covid.Controllers
{
    public class HomeController : BaseController
    {
        private ConfigSettings _configuration;
        private Mapping _mapping;
        private gOfficeSummaryManager _gOfficeSummaryManager;
        private readonly UserManager<BusinessUserInfo> _userManager;
        private readonly SignInManager<BusinessUserInfo> _signInManager;

        public HomeController(IOptions<ConfigSettings> configuration,
            IOptions<Mapping> mapping,
            UserManager<BusinessUserInfo> userManager,
            SignInManager<BusinessUserInfo> signInManager)
        {
            _configuration = configuration.Value;
            _mapping = mapping.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _gOfficeSummaryManager = new gOfficeSummaryManager(configuration);
        }

        [AllowAnonymous]
        public ActionResult CovidForm(int subscriberId)
        {

            var form = gCovidManager.GetFormInfo(subscriberId);

            return View(form);
        }
        [AllowAnonymous]
        public ActionResult CovidFormMOH(int subscriberId)
        {
            return View();
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
            // return Json(new ResponseViewModel() { StatusCode = 1, StatusMessage = "Record Saved Successfully" });


        }
        [AllowAnonymous]
        public ActionResult CovidFormView(int subscriberId)
        {
            var form = gCovidManager.GetFormInfo(subscriberId);

            return View(form);
        }

        [AllowAnonymous]
        public ActionResult ViewForms()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Upsert([FromBody]gFormCovidEntry model)
        {
            try
            {
                model.BusinessInfo_ID = 1;
                model.InPersonScreenDate = DateTime.Now;
                model.PreScreenDate = DateTime.Now;


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
            if (!string.IsNullOrEmpty(requestModel.Search?.Value))
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(s => s.InPersonScreenDate.ToString().Contains(value) ||
                                         s.FirstName.Contains(value) ||
                                         s.LastName.Contains(value) ||
                                         s.PreScreenDate.ToString().Contains(value) ||
                                         s.Covid_Form_Description.Contains(value));
            }

            filteredCount = query.Count();

            #endregion Filtering



            objViewModelList = query.Skip(requestModel.Start).Take(requestModel.Length).ToList();


            return Json(objViewModelList
               .Select(e => new
               {
                   Id = e.QueueID,
                   FullName = e.FirstName + " " + e.LastName,
                   FormName = e.Covid_Form_Description,
                   PreScreenDate = e.PreScreenDate,
                   IsPreScreen = e.IsPreScreen,
                   InPersonScreenDate = e.InPersonScreenDate,
                   IsInPersonScreen = e.IsInPersonScreen,
                   FormID = e.FormID,
                   SubscriberID = e.SubscriberID
               })
               .ToDataTablesResponse(requestModel, totalCount, filteredCount));
        }

    }
}
