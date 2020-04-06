using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LTCDashboard.Models;
using LTCDataManager.Office;
using LTCDataManager.User;
using LTCDataModel.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using LTCDataManager.Summary;

namespace LTCDashboard.Controllers
{
    public class HomeController : BaseController
    {
        private ConfigSettings _configuration;
        private Mapping _mapping;
        private gOfficeSummaryManager _gOfficeSummaryManager;
        public HomeController(IOptions<ConfigSettings> configuration,
            IOptions<Mapping> mapping)
        {
            _configuration = configuration.Value;
            _mapping = mapping.Value;
            _gOfficeSummaryManager = new gOfficeSummaryManager(configuration);
        }

        [HttpGet]
        public ActionResult GetUserPermissions()
        {
            return Json(gUserModuleManager.GetUserPermissions(UserId));
        }

        [HttpGet]
        public ActionResult GetBusinessNames()
        {
            //return Json(gOfficeManager.GetOfficeDetailByUserId(UserId));
            return Json(gOfficeManager.GetOfficeDetailByOfficeSequence(OfficeSequence));
        }

        [HttpGet]
        public ActionResult GetOffices()
        {
            return Json(gOfficeManager.GetOffices(UserId));
        }

        [HttpGet]
        public ActionResult GetProviders(string office_sequence)
        {
            return Json(gOfficeManager.GetProviders(UserId));
        }

        [HttpPost]
        public JsonResult LoadCardInformations()
        {
            try
            {
                return Json(new { Success = true, Data = _gOfficeSummaryManager.GetOfficeSummary(OfficeSequence) });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false });
            }
        }
    }
}
