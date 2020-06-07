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
        public ActionResult CovidForm() 
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult CovidFormView()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ViewForms()
        {
            return View();
        }

      
    }
}
