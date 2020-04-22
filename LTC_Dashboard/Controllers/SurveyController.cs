using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTCDataManager.SurveyManager;
using LTCDataModel.Configurations;
using LTCDataModel.Form;
using LTCDataModel.Survey;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LTCDashboard.Controllers
{
    public class SurveyController : BaseController
    {
        private ConfigSettings _configuration;
        private gSurveyManager _gSurveyManager;
        public SurveyController(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
            _gSurveyManager = new gSurveyManager(configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SurveyDesigner()
        {
            return View();
        }

        public IActionResult Reports()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult RenderSurvey()
        {
            return View();
        }

        #region Json


        #region Private Survey
        [HttpGet]
        [AllowAnonymous]
        //[ValidateInput(false)]
        public ActionResult GetPrivateDesign(int oid, int fid)
        {
            return Json(_gSurveyManager.GetSurveyDesign(fid));
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateInput(false)]
        public ActionResult SaveAnswer(gFormSavedModel data)
        {
            //gSurveyManager.SaveSurveyAnswer(data);
            return Json(_gSurveyManager.SaveSurveyAnswer(data));
        }


        [HttpPost]
        public JsonResult SaveDesign(gPrivateFormModel data)
        {
            _gSurveyManager.SaveDesign(data);
            return Json(new { Success = true });
        }
        [HttpPost]
        public JsonResult DeleteDesign(int id)
        {
            _gSurveyManager.DeleteDesign(id);
            return Json(new { Success = true });
        }
        #endregion

        #region Public Survey
        [HttpPost]
        public JsonResult SavePublicDesign(gPrivateFormModel data)
        {
            _gSurveyManager.SavePublicDesign(data, OfficeSequence);
            return Json(new { Success = true });
        }
        [HttpPost]
        public JsonResult DeletePublicDesign(int Id)
        {
            _gSurveyManager.DeletePublicDesign(Id);
            return Json(new { Success = true });
        }
        [HttpGet]
        public ActionResult GetPublicSurveys()
        {
            return Json(_gSurveyManager.GetAllPublicSurvey());
        }

        [HttpPost]
        public ActionResult GetPrivateSurveys(LTCDataModel.Form.gData data)
        {
            return Json(_gSurveyManager.GetAllPrivateSurvey(data.OfficeId));
        }


        [HttpPost]
        public ActionResult GetSurveysAnswers(LTCDataModel.Form.gData data)
        {
            //TODO: getconnectionstring change needed. 
            //return Json(gSurveyManager.GetSurveysAnswers(GetUserConnectionString(), data.OfficeId));
            return Json(false);
        }

        [HttpPost]
        public ActionResult GetSurveysReport(LTCDataModel.Form.gData data)
        {
            return Json(_gSurveyManager.GetSurveyReport(data.OfficeId));
        }
        #endregion




        #region public Tags

        [HttpGet]
        public ActionResult GetPublicTags()
        {
            return Json(_gSurveyManager.GetPublicTags());
        }

        [HttpPost]
        public JsonResult GetCategories()
        {
            return Json(new { Success = true, data = _gSurveyManager.GetPublicCategories() });
        }

        [HttpPost]
        public JsonResult SavePublicTag(LTCDataModel.Form.gPublicTagModel data)
        {
            _gSurveyManager.SavePublicTag(data);
            return Json(new { Success = true });
        }
        public JsonResult DeletePublicTag(int id)
        {
            _gSurveyManager.DeletePublicTags(id);
            return Json(new { Success = true });
        }
        #endregion

        #region private Tags

        [HttpPost]
        public ActionResult GetPrivateTags(LTCDataModel.Form.gData data)
        {
            return Json(_gSurveyManager.GetPrivateTags(data.OfficeId));
        }


        [HttpPost]
        public JsonResult SavePrivateTag(gFormPrivateTag data)
        {
            _gSurveyManager.SavePrivateTag(data);
            return Json(new { Success = true });
        }

        [HttpPost]
        public JsonResult DeletePrivateTag(int id)
        {
            _gSurveyManager.DeletePrivateTag(id);
            return Json(new { Success = true });
        }

        #endregion

        #endregion
    }
}