using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTCDataManager.NewsLetter;
using LTCDataModel.Configurations;
using LTCDataModel.Form;
using LTCDataModel.NewsLetter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LTCDashboard.Controllers
{
    public class NewsLetterController : BaseController
    {
        private ConfigSettings _configuration;
        public NewsLetterController(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
        }


        public IActionResult Index()
        {
            return View();
        }

        #region Get
        [HttpGet]
        public ActionResult GetPreDefinedTemplates()
        {
            return Json(gNewsLetterManager.GetPreDefinedTemplates());
        }
        [HttpGet]
        public ActionResult GetUserDefinedTemplates(gData model)
        {
            return Json(gNewsLetterManager.GetUserDefinedTemplates(model.OfficeId));
        }
        [HttpGet]
        public ActionResult GetIndustries()
        {
            return Json(gNewsLetterManager.GetIndustries(OfficeSequence));
        }
        [HttpGet]
        public ActionResult GetSubIndustries()
        {
            return Json(gNewsLetterManager.GetSubIndustries(OfficeSequence));
        }
        [HttpGet]
        public ActionResult GetTemplateTypes()
        {
            return Json(gNewsLetterManager.GetTemplateTypes(OfficeSequence));
        }
        [HttpGet]
        public ActionResult GetShellTemplates()
        {
            return Json(gNewsLetterManager.GetShellTemplates());
        }
        #endregion

        #region Save
        [HttpPost]
        public ActionResult SaveNewsletterEditor(gSavePredefinedTemplate model)
        {
            model.Office_Sequence = OfficeSequence;
            gNewsLetterManager.SavePreNewsTemplate(model);
            return Json(null);
        }

        [HttpPost]
        public ActionResult SaveUserNewsletterEditor(gSaveUserTemplate model)
        {
            model.Office_Sequence = OfficeSequence;
            gNewsLetterManager.SaveUserNewsTemplate(model);
            return Json(new gSaveUserTemplate());
        }
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult DeletePredefinedTemplate(int id)
        {
            gNewsLetterManager.DeletePreDefinedTemplate(id);
            return Json(null);
        }
        [HttpPost]
        public ActionResult DeleteUserdefinedTemplate(int id)
        {
            gNewsLetterManager.DeleteUserDefinedTemplate(id);
            return Json(new gSaveUserTemplate());
        }
        #endregion
    }
}