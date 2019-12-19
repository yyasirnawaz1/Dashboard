using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LTCDataManager.Form;
using LTCDataManager.FormEntryHome;
using LTCDataManager.Office;
using LTCDataManager.SurveyManager;
using LTCDataManager.TodayAppointment;
using LTCDataModel.Configurations;
using LTCDataModel.FormEntryHome;
using Microsoft.AspNetCore.Mvc;
using LTCOfficePortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace LTCOfficePortal.Controllers
{
    public class HomeController : BaseController
    {
        private ConfigSettings _configuration;
        private gSurveyManager _gSurveyManager;
        private gFormManager _gFormManager;
        private gOfficeManager _gOfficeManager;

        public HomeController(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
            _gSurveyManager = new gSurveyManager(configuration);
            _gFormManager = new gFormManager(configuration);
            _gOfficeManager = new gOfficeManager(configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {

            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public IActionResult FormRender()
        {
            return View();
        }

        public IActionResult SurveyRender()
        {
            return View();
        }


        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Shared
        [HttpGet]
        public ActionResult GetProviders()
        {
            return Json(_gOfficeManager.GetProviders(GetUserConnectionString()));
        }

        [HttpGet]
        public ActionResult GetOfficeName()
        {
            return Json(gOfficeManager.GetOfficeName(GetUserConnectionString(), OfficeSequence));
        }
        #endregion

        #region today's appointment

        [HttpGet]
        public ActionResult GetTodayAppointment()
        {
            if (HttpContext.Request.Query["fromDate"].ToString() != "undefined" && HttpContext.Request.Query["toDate"].ToString() != "undefined")
                return Json(gTodayAppointmentManager.GetTodayAppointment(GetUserConnectionString(), OfficeSequence, HttpContext.Request.Query["fromDate"].ToString(), HttpContext.Request.Query["toDate"].ToString()));
            return Json(gTodayAppointmentManager.GetTodayAppointment(GetUserConnectionString(), OfficeSequence));
        }

        [HttpPost]
        public ActionResult AddPortalStatus(int AppId, int PortalAction, int Type)
        {
            //var AppId = Convert.ToInt32(Request.QueryString["AppId"]);
            return Json(gTodayAppointmentManager.AddPortalStatus(GetUserConnectionString(), AppId, PortalAction, Type));
        }

        [HttpGet]
        public ActionResult GetPrivateSurveys()
        {
            //var result = gSurveyManager.GetAllPrivateSurvey(OfficeSequence);
            return Json(_gSurveyManager.GetAllPrivateSurvey(OfficeSequence));
        }

        [HttpGet]
        public ActionResult GetPrivateForms()
        {
            return Json(_gFormManager.GetAllPrivateForm(OfficeSequence));
        }

        [HttpPost]
        public ActionResult AddFormQueueEntry(QueueEntryViewModel model)
        {
            return Json(gTodayAppointmentManager.AddFormQueueEntry(GetUserConnectionString(), new gAddQueueEntryModel
            {
                AppointmentCounter = model.AppointmentCounter,
                FormID = model.FormID,
                OfficeID = model.Office_Sequence,
                Type = model.Type,
                PatientNumber = model.PatientNumber,
                //UserId = GetUserId(),
                Office_Sequence = model.Office_Sequence
            }));
        }

        [HttpPost]
        public ActionResult AddSurveyQueueEntry(QueueEntryViewModel model)
        {
            return Json(gTodayAppointmentManager.AddSurveyQueueEntry(GetUserConnectionString(), new gAddQueueEntryModel
            {
                AppointmentCounter = model.AppointmentCounter,
                FormID = model.FormID,
                OfficeID = model.Office_Sequence,
                Type = model.Type,
                PatientNumber = model.PatientNumber,
                //UserId = GetUserId(),
                Office_Sequence = model.Office_Sequence
            }));
        }

        //TODO: change the pdf conversion code for .net core

        [HttpPost]
        public ActionResult SaveSurveyAndForm(FormAndSurveyViewModel model)
        {
            try
            {
                //var pdfContent = Utility.GeneratePdf(model.Content, Server.MapPath("~/Content/PdfTemplate/template.html"));
                //byte[] bytes = System.Text.Encoding.ASCII.GetBytes(pdfContent);
                //return Json(gTodayAppointmentManager.SaveSurveyAndForm(new gSaveSurveyAndFormModel
                //{
                //    AppointmentCounter = 0,
                //    FormID = model.FormID,
                //    Content = model.Content,
                //    Type = model.Type,
                //    Office_Sequence = model.Office_Sequence,
                //    PatientNumber = model.PatientNumber,
                //    PdfContent = pdfContent
                //}));
            }
            catch (Exception ex)
            {

            }
            return Json(false);


        }

        //TODO: find better plugin for .netcore

        //[HttpGet]
        //public FileResult DownloadFile(FormAndSurveyModel model)
        //{
        //    var pdfContent = Utility.GeneratePdfByte(model.Content, Server.MapPath("~/Content/PdfTemplate/template.html"));


        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("Content-Disposition", "attachment;filename=Receipt-test.pdf");
        //    Response.BinaryWrite(pdfContent);
        //    return File(pdfContent, "application/pdf");
        //}
        #endregion

        #region Search Patient

        //TODO: fix the json serializer code 
        [HttpGet]
        public ActionResult GetPatients()
        {
            //var serializer = new JavaScriptSerializer();

            //// For simplicity just use Int32's max value.
            //// You could always read the value from the config section mentioned above.
            //serializer.MaxJsonLength = Int32.MaxValue;

            //var resultData = gSearchPatientManager.GetPatients(GetUserConnectionString(), OfficeSequence);

            //var result = new ContentResult
            //{
            //    Content = serializer.Serialize(resultData),
            //    ContentType = "application/json"
            //};
            //return result;


            //remove this start
            return null;
            //remove this end



            //return Json(gSearchPatientManager.GetPatients());
        }
        #endregion

        #region Pending Form
        [HttpGet]
        public ActionResult GePendingtForms()
        {
            return Json(gPendingForm.GetForms(GetUserConnectionString(), OfficeSequence));
        }
        #endregion

        #region Pending Survey
        [HttpGet]
        public ActionResult GetPendingSurveys()
        {
            return Json(gPendingSurvey.GetSurveys(GetUserConnectionString(), OfficeSequence));
        }
        #endregion

        #region Render Form and Survey (anonymous requests)

        [HttpGet]
        [AllowAnonymous]
        //[ValidateInput(false)]
        public ActionResult GetPrivateDesignSurvey(int OfficeSequence, int FormId, int PatientNumber, int AppointmentCounter)
        {
            return Json(_gSurveyManager.GetSurveyDesign(FormId));
        }

        [HttpGet]
        [AllowAnonymous]
        //[ValidateInput(false)]
        public ActionResult GetPatientDetail(int OfficeSequence, int PatientNumber)
        {
            return Json(gSearchPatientManager.GetPatientDetail(GetUserConnectionString(), OfficeSequence, PatientNumber));
        }

        [HttpGet]
        [AllowAnonymous]
        //[ValidateInput(false)]
        public ActionResult GetPatientAddress(int OfficeSequence, int PatientNumber)
        {
            return Json(gSearchPatientManager.GetPatientAddress(GetUserConnectionString(), OfficeSequence, PatientNumber));
        }

        [HttpGet]
        [AllowAnonymous]
        //[ValidateInput(false)]
        public ActionResult GetPatientPhone(int OfficeSequence, int PatientNumber)
        {
            return Json(gSearchPatientManager.GetPatientPhone(GetUserConnectionString(), OfficeSequence, PatientNumber));
        }

        [HttpGet]
        [AllowAnonymous]
        //[ValidateInput(false)]
        public ActionResult GetPrivateDesignForm(int oid, int fid)
        {
            return Json(_gFormManager.GetFormDesign(fid));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetFormsPublicTags()
        {
            return Json(_gFormManager.GetPublicTags());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetSurveyPublicTags()
        {
            return Json(_gFormManager.GetPublicTags());
        }

        #endregion
    }
}
