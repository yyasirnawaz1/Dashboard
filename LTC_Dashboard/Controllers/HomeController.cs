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
using LTCDataModel.Office;
using Microsoft.AspNetCore.Identity;
using LTCDashboard.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LTCDashboard.Controllers
{
    public class HomeController : BaseController
    {
        private ConfigSettings _configuration;
        private Mapping _mapping;
        private gOfficeSummaryManager _gOfficeSummaryManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(IOptions<ConfigSettings> configuration,
            IOptions<Mapping> mapping,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _configuration = configuration.Value;
            _mapping = mapping.Value;
            _userManager = userManager;
            _signInManager = signInManager;
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


        [HttpPost]
        public JsonResult GetUserdata()
        {
            try
            {
                var data = gUserModuleManager.GetUserProfile(UserId);
                return Json(new { Success = true, Data = data });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false });
            }
        }


        [HttpPost]
        public JsonResult UpdateProfile(gUserProfile model)
        {
            try
            {
                gUserModuleManager.UpdateProfile(model);
                return Json(new { Success = true, Data = "" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Data = "unable to update profile" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> ChangePassword(string CurrentPassword, string newPassword)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Json(new { Success = false, Data = "Unable to find user" });
                }

                var changePasswordResult = await _userManager.ChangePasswordAsync(user, CurrentPassword, newPassword);
                if (!changePasswordResult.Succeeded)
                {
                    string errors = "";
                    foreach (var error in changePasswordResult.Errors)
                    {
                        errors += error.Description + "<br />";
                    }

                    return Json(new { Success = false, Data = errors });

                }
                else
                {
                    await _signInManager.RefreshSignInAsync(user);
                }

                return Json(new { Success = true, Data = "" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Data = "Unable to change password." });
            }
        }
    }
}
