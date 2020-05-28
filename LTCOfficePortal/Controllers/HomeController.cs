using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LTCDataManager;
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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
 
using System.IO;
using System.Text;
using LTCDataManager.Portal;

namespace LTCOfficePortal.Controllers
{
    public class HomeController : BaseController
    {
        private ConfigSettings _configuration;
        private gSurveyManager _gSurveyManager;
        private gFormManager _gFormManager;
        private gOfficeManager _gOfficeManager;
        private readonly IHostingEnvironment _webHostEnvironment;
      
       

        public HomeController(IOptions<ConfigSettings> configuration,IHostingEnvironment webHostEnvironment ):base(webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
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
            return Json(_gOfficeManager.GetProviders(GetUserConnectionStringDental()));
        }

        [HttpGet]
        public ActionResult GetOfficeName()
        {
            return Json(gOfficeManager.GetOfficeName(GetUserConnectionStringDental(), OfficeSequence));
        }
        #endregion

        #region today's appointment

        [HttpGet]
        public ActionResult GetTodayAppointment()
        {
            if (HttpContext.Request.Query["fromDate"].ToString() != "undefined" && HttpContext.Request.Query["toDate"].ToString() != "undefined")
                return Json(gTodayAppointmentManager.GetTodayAppointment(GetUserConnectionStringDental(), OfficeSequence, HttpContext.Request.Query["fromDate"].ToString(), HttpContext.Request.Query["toDate"].ToString()));
            return Json(gTodayAppointmentManager.GetTodayAppointment(GetUserConnectionStringDental(), OfficeSequence));
        }

        [HttpPost]
        public ActionResult AddPortalStatus(int AppId, int PortalAction, int Type)
        {
            //var AppId = Convert.ToInt32(Request.QueryString["AppId"]);
            return Json(gTodayAppointmentManager.AddPortalStatus(GetUserConnectionStringDental(), AppId, PortalAction, Type));
        }

        [HttpGet]
        public ActionResult GetPrivateSurveys()
        {
            //var result = gSurveyManager.GetAllPrivateSurvey(OfficeSequence);
            return Json(_gSurveyManager.GetAllPrivateSurvey(OfficeSequence,GetUserConnectionStringForms()));
        }

        [HttpGet]
        public ActionResult GetPrivateForms()
        {
            return Json(_gFormManager.GetAllPrivateForm(OfficeSequence,GetUserConnectionStringForms()));
        }

        [HttpPost]
        public ActionResult AddFormQueueEntry(QueueEntryViewModel model)
        {
            return Json(gTodayAppointmentManager.AddFormQueueEntry(GetUserConnectionStringForms(), new gAddQueueEntryModel
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
            return Json(gTodayAppointmentManager.AddSurveyQueueEntry(GetUserConnectionStringForms(), new gAddQueueEntryModel
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
                string webRootPath = _webHostEnvironment.WebRootPath;
                var htmlFile = webRootPath + "/PdfTemplate/template.html";
                var pdfContent = Utility.GeneratePdf(model.Content, htmlFile);
                
                return Json(gTodayAppointmentManager.SaveSurveyAndForm(new gSaveSurveyAndFormModel
                {
                    AppointmentCounter = 0,
                    FormID = model.FormID,
                    Content = model.Content,
                    Type = model.Type,
                    Office_Sequence = model.Office_Sequence,
                    PatientNumber = model.PatientNumber,
                    PdfContent = pdfContent,
                    IsSurveyForm= model.IsSurveyForm
                }, GetUserConnectionStringForms()));
            }
            catch (Exception ex)
            {

            }
            return Json(false);


        }
       
         [HttpGet]
        public IActionResult createFile()
        {
            return DownloadFile();
        }
         
       [HttpGet]
        public FileResult  DownloadFile()
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            var htmlFile = webRootPath + "/PdfTemplate/template.html";
            var content1 = "[{\"type\":\"autocomplete\",\"label\":\"Autocomplete\",\"className\":\"form-control\",\"name\":\"autocomplete-1576153483105\",\"TagId\":\"0\",\"values\":[{\"label\":\"Option 1\",\"value\":\"option-1\"},{\"label\":\"Option 2\",\"value\":\"option-2\"},{\"label\":\"Option 3\",\"value\":\"option-3\"}],\"userData\":[\"option-3\"]},{\"type\":\"button\",\"label\":\"Button\",\"subtype\":\"button\",\"name\":\"button-1576153485999\",\"TagId\":\"0\"},{\"type\":\"checkbox-group\",\"label\":\"Checkbox Group\",\"name\":\"checkbox-group-1576153486998\",\"TagId\":\"0\",\"values\":[{\"label\":\"Option 1\",\"value\":\"option-1\",\"selected\":true}],\"userData\":[\"option-1\"]},{\"type\":\"date\",\"label\":\"Date Field\",\"className\":\"form-control\",\"name\":\"date-1576153488463\",\"TagId\":\"0\",\"userData\":[\"2019-12-12\"]},{\"type\":\"file\",\"label\":\"File Upload\",\"className\":\"form-control\",\"name\":\"file-1576153489477\",\"subtype\":\"file\",\"TagId\":\"0\"},{\"type\":\"header\",\"subtype\":\"h1\",\"label\":\"Header\",\"TagId\":\"0\"},{\"type\":\"hidden\",\"name\":\"hidden-1576153491574\",\"TagId\":\"0\",\"userData\":[\"\"]},{\"type\":\"number\",\"label\":\"Number\",\"className\":\"form-control\",\"name\":\"number-1576153492705\",\"TagId\":\"0\",\"userData\":[\"222\"]},{\"type\":\"paragraph\",\"subtype\":\"p\",\"label\":\"Paragraph\",\"TagId\":\"0\"},{\"type\":\"textarea\",\"label\":\"Text Area\",\"className\":\"form-control\",\"name\":\"textarea-1576153500361\",\"subtype\":\"textarea\",\"TagId\":\"0\",\"userData\":[\"sdsdsd\"]},{\"type\":\"NewLine\",\"label\":\"\",\"name\":\"NewLine-1576153502624\",\"TagId\":\"0\"},{\"type\":\"starRating\",\"label\":\"Star Rating\",\"name\":\"starRating-1576153503988\",\"TagId\":\"0\"},{\"type\":\"LineSeprator\",\"label\":\"\",\"name\":\"LineSeprator-1576153501459\",\"TagId\":\"0\"},{\"type\":\"text\",\"label\":\"Text Field\",\"className\":\"form-control\",\"name\":\"text-1576153498797\",\"subtype\":\"text\",\"TagId\":\"0\",\"userData\":[\"sdsdsd\"]},{\"type\":\"radio-group\",\"label\":\"Radio Group\",\"name\":\"radio-group-1576153494919\",\"TagId\":\"0\",\"values\":[{\"label\":\"Option 1\",\"value\":\"option-1\"},{\"label\":\"Option 2\",\"value\":\"option-2\"},{\"label\":\"Option 3\",\"value\":\"option-3\"}],\"userData\":[\"option-1\"]},{\"type\":\"select\",\"label\":\"Select\",\"className\":\"form-control\",\"name\":\"select-1576153497582\",\"TagId\":\"0\",\"values\":[{\"label\":\"Option 1\",\"value\":\"option-1\",\"selected\":true},{\"label\":\"Option 2\",\"value\":\"option-2\"},{\"label\":\"Option 3\",\"value\":\"option-3\"}],\"userData\":[\"option-3\"]}]";
            var pdfContent = Utility.GeneratePdfArray(content1, htmlFile);
          
            var contentType = "application/pdf";
            var fileName = "Receipt-test.pdf";
            return File(pdfContent, contentType, fileName);
        }
       
        #endregion

        #region Search Patient

        //TODO: fix the json serializer code 
        [HttpPost]
            public string GetPatients(string office_sequence, string sEcho, int iDisplayStart, int iDisplayLength, string sSearch)
        {

            var resultData = gSearchPatientManager.GetPatients(GetUserConnectionStringDental(), OfficeSequence, sSearch,
                iDisplayStart,
                iDisplayLength);

            var result = new ContentResult
            {
                Content = JsonConvert.SerializeObject(resultData, Formatting.Indented),
                ContentType = "application/json"
            };
           

            int count = gSearchPatientManager.GetPatientCount(GetUserConnectionStringDental(), OfficeSequence, sSearch);

            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("{");
            sb.Append("\"sEcho\": ");
            sb.Append(sEcho);
            sb.Append(",");
            sb.Append("\"iTotalRecords\": ");
            sb.Append(count);
            sb.Append(",");
            sb.Append("\"iTotalDisplayRecords\": ");
            sb.Append(count);
            sb.Append(",");
            sb.Append("\"aaData\": ");
            //JsonConvert.SerializeObject(viewModel.data)
            sb.Append(JsonConvert.SerializeObject(resultData, Formatting.Indented));
            sb.Append("}");
            return sb.ToString();
           // return Json(new { obj = viewModel });

        }
        //TODO: fix the json serializer code 
        //[HttpGet]
        //public ActionResult GetPatients2()
        //{
        //    //var serializer = new JavaScriptSerializer();
        //    //object o = JsonConvert.DeserializeObject(json1);
            
        //    //// For simplicity just use Int32's max value.
        //    //// You could always read the value from the config section mentioned above.
        //    //serializer.MaxJsonLength = Int32.MaxValue;

        //    var resultData = gSearchPatientManager.GetPatients(GetUserConnectionString(), OfficeSequence);

        //    var result = new ContentResult
        //    {
        //        Content = JsonConvert.SerializeObject(resultData, Formatting.Indented),
        //        ContentType = "application/json"
        //    };
        //    return result;


        //    //remove this start
        //    //return null;
        //    //remove this end



        //  //  return Json(gSearchPatientManager.GetPatients());
        //}
        #endregion

        #region Pending Form
        [HttpGet]
        public ActionResult GePendingtForms()
        {
            return Json(gPendingForm.GetForms(GetUserConnectionStringDental(), GetUserConnectionStringForms(), OfficeSequence));
        }
        #endregion

        #region Pending Survey
        [HttpGet]
        public ActionResult GetPendingSurveys()
        {
            return Json(gPendingSurvey.GetSurveys(GetUserConnectionStringDental(), GetUserConnectionStringForms(), OfficeSequence));
        }
        #endregion

        #region Render Form and Survey (anonymous requests)

        [HttpGet]
        [AllowAnonymous]
        //[ValidateInput(false)]
        public ActionResult GetPrivateDesignSurvey(int OfficeSequence, int FormId, int PatientNumber, int AppointmentCounter)
        {
            return Json(_gSurveyManager.GetSurveyDesign(FormId, GetUserConnectionStringForms()));
        }

        [HttpGet]
        [AllowAnonymous]
        //[ValidateInput(false)]
        public ActionResult GetPatientDetail(int OfficeSequence, int PatientNumber)
        {
            return Json(gSearchPatientManager.GetPatientDetail(GetUserConnectionStringDental(), OfficeSequence, PatientNumber));
        }

        [HttpGet]
        [AllowAnonymous]
        //[ValidateInput(false)]
        public ActionResult GetPatientAddress(int OfficeSequence, int PatientNumber)
        {
            return Json(gSearchPatientManager.GetPatientAddress(GetUserConnectionStringDental(), OfficeSequence, PatientNumber));
        }

        [HttpGet]
        [AllowAnonymous]
        //[ValidateInput(false)]
        public ActionResult GetPatientPhone(int OfficeSequence, int PatientNumber)
        {
            return Json(gSearchPatientManager.GetPatientPhone(GetUserConnectionStringDental(), OfficeSequence, PatientNumber));
        }

        [HttpGet]
        [AllowAnonymous]
        //[ValidateInput(false)]
        public ActionResult GetPrivateDesignForm(int oid, int fid)
        {
            return Json(_gFormManager.GetFormDesign(fid,GetUserConnectionStringForms()));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetFormsPublicTags()
        {
            return Json(_gFormManager.GetPublicTags(GetUserConnectionStringForms()));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetSurveyPublicTags()
        {
            return Json(_gSurveyManager.GetPublicTags(GetUserConnectionStringForms()));
        }

        #endregion
    }
}
