using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTCDataManager.Dashboard;
using LTCDataModel.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LTC_Covid.Controllers
{
    public class AboutController : BaseController
    {
        private ConfigSettings _configuration;
        public AboutController(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}