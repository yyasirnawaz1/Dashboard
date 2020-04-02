using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTCDataManager.Form;
using LTCDataModel.Configurations;
using LTCDataModel.Form;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LTCDashboard.Controllers
{
    public class FormController : BaseController
    {
        private ConfigSettings _configuration;
        private gFormManager _gFormManager;
        public FormController(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
            _gFormManager = new gFormManager(configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FormDesigner()
        {
            return View();
        }

        public IActionResult Reports()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult RenderForm()
        {
            return View();
        }

        #region Json


        #region Private Form
        [HttpGet]
        [AllowAnonymous]
        //[ValidateInput(false)]
        public ActionResult GetPrivateDesign(int oid, int fid)
        {
            return Json(_gFormManager.GetFormDesign(fid));
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateInput(false)]
        public ActionResult SaveAnswer(gFormSavedModel data)
        {
            //_gFormManager.SaveFormAnswer(data);
            return Json(_gFormManager.SaveFormAnswer(data));
        }


        [HttpPost]
        public JsonResult SaveDesign(gPrivateFormModel data)
        {
            _gFormManager.SaveDesign(data);
            return Json(new { Success = true });
        }
        [HttpPost]
        public JsonResult DeleteDesign(int Id)
        {
            _gFormManager.DeleteDesign(Id);
            return Json(new { Success = true });
        }
        #endregion

        #region Public Form
        [HttpPost]
        public JsonResult SavePublicDesign(gPrivateFormModel data)
        {
            _gFormManager.SavePublicDesign(data, OfficeSequence);
            return Json(new { Success = true });
        }
        [HttpPost]
        public JsonResult DeletePublicDesign(int Id)
        {
            _gFormManager.DeletePublicDesign(Id);
            return Json(new { Success = true });
        }
        [HttpGet]
        public ActionResult GetPublicForms()
        {
            return Json(_gFormManager.GetAllPublicForm());
        }

        [HttpPost]
        public ActionResult GetPrivateForms(gData data)
        {
            return Json(_gFormManager.GetAllPrivateForm(data.OfficeId));
        }


        [HttpPost]
        public ActionResult GetFormsAnswers(gData data)
        {
            return Json(_gFormManager.GetFormsAnswers(GetUserConnectionString(), data.OfficeId));
        }

        [HttpPost]
        public ActionResult GetFormsReport(gData data)
        {
            return Json(_gFormManager.GetFormReport(data.OfficeId));
        }
        #endregion

        #region public Tags

        [HttpGet]
        public ActionResult GetPublicTags()
        {
            return Json(_gFormManager.GetPublicTags());
        }

        [HttpPost]
        public JsonResult GetCategories()
        {
            return Json(new { Success = true, data = _gFormManager.GetPublicCategories() });
        }

        [HttpPost]
        public JsonResult SavePublicTag(gPublicTagModel data)
        {
            _gFormManager.SavePublicTag(data);
            return Json(new { Success = true });
        }
        public JsonResult DeletePublicTag(int Id)
        {
            _gFormManager.DeletePublicTags(Id);
            return Json(new { Success = true });
        }
        #endregion

        #region private Tags

        [HttpPost]
        public ActionResult GetPrivateTags(gData data)
        {
            return Json(_gFormManager.GetPrivateTags(data.OfficeId));
        }


        [HttpPost]
        public JsonResult SavePrivateTag(gFormPrivateTag data)
        {
            _gFormManager.SavePrivateTag(data);
            return Json(new { Success = true });
        }

        [HttpPost]
        public JsonResult DeletePrivateTag(int Id)
        {
            _gFormManager.DeletePrivateTag(Id);
            return Json(new { Success = true });
        }

        #endregion

        #endregion
    }
}