using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTCDashboard.Models;
using LTCDataManager.Review;
using LTCDataModel.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LTCDashboard.Controllers
{
    public class OfficeManagementController : BaseController
    {
        private ConfigSettings _configuration;
        public OfficeManagementController(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}