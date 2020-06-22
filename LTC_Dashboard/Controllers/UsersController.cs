using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTCDashboard.Controllers;
using LTCDataManager.Office;
using LTCDataManager.Twilio;
using LTCDataManager.User;
using LTCDataModel.Configurations;
using LTCDataModel.Office;
using LTCDataModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace LTC_Dashboard.Controllers
{
    public class UsersController : BaseController
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TwilioSettings _twilioSettings;

        public UsersController(UserManager<ApplicationUser> userManager, IOptions<TwilioSettings> twilioSettings)
        {
            _userManager = userManager;
            _twilioSettings = twilioSettings.Value;
        }

        public ActionResult Index()
        {
            if (!IsSystemAdmin)
            {
                Response.Redirect("/", true);
            }
            var model = gUserModuleManager.GetAllUsers();

            return View(model);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            if (!IsSystemAdmin)
            {
                Response.Redirect("/", true);
            }
            var model = gUserModuleManager.GetUserById(id);

            return View(model);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            if (!IsSystemAdmin)
            {
                Response.Redirect("/", true);
            }
            GenerateDropdownData(null);
            return View();
        }

        public ActionResult CreateDefault()
        {
            if (!IsSystemAdmin)
            {
                Response.Redirect("/", true);
            }
            GenerateDropdownData(null);

            var model = new ApplicationUser();
            string emailDefault = Guid.NewGuid().ToString("N") + "@logictechcorp.com";
            string passwordDefault = Guid.NewGuid().ToString("N");
            model.Email = emailDefault;
            model.UserName = emailDefault;
            model.PasswordHash = passwordDefault;
            model.IsDefaultUser = true;

            return View("Create", model);
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ApplicationUser model, List<int> allowed_offices)
        {
            try
            {
                model.UserName = model.Email;
                model.EmailConfirmed = true;
                model.PhoneNumberConfirmed = true;
                model.PhoneNumber = model.AuthenticationPhone;
                model.IsEditModuleEnabled = false;
                model.IsEditUserEnabled = false;
                model.IsAssignOfficeEnabled = false;

                model.Office_Sequence = gOfficeManager.GetOfficeSequenceByOfficeNumber(model.Office_Number);
                var result = await _userManager.CreateAsync(model, model.PasswordHash);
                if (!result.Succeeded)
                {
                    if (result.Errors.Count() > 0)
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, item.Description);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Unable to update user");
                    }
                    GenerateDropdownData(null);
                    return View();

                }
                else
                {
                    if (IsAssignOfficeEnabled)
                        gOfficeManager.InsertAllowedOffices(model.Id, allowed_offices);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                GenerateDropdownData(null);
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            if (!IsSystemAdmin || !IsEditUserEnabled)
            {
                Response.Redirect("/", true);
            }
            var model = gUserModuleManager.GetUserById(id);

            GenerateDropdownData(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult SendSMS(int id)
        {
            if (!IsSystemAdmin)
            {
                Response.Redirect("/", true);
            }
            try
            {

                DataEncryptor keys = new DataEncryptor();

                var model = gUserModuleManager.GetUserById(id);
                if (!string.IsNullOrEmpty(model.AuthenticationPhone))
                {
                    string signinUrl = $"{_twilioSettings.Url}Identity/Account/Login?userid={model.Email}&pass={keys.EncryptString(model.PasswordHash)}";
                    TwilioManager.SendSms(_twilioSettings, model.AuthenticationPhone, signinUrl);
                    return Json(new { Success = true, Message = "" });
                }
                else
                {
                    return Json(new { Success = false, Message = "Phone Number Missing for the user" });
                }

            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }



        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string oldPassword, List<int> allowed_offices, ApplicationUser model)
        {
            if (!IsSystemAdmin || !IsEditUserEnabled)
            {
                Response.Redirect("/", true);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserName = model.Email;
                    model.EmailConfirmed = true;
                    model.PhoneNumberConfirmed = true;
                    model.IsEditModuleEnabled = false;
                    model.IsEditUserEnabled = false;
                    model.IsAssignOfficeEnabled = false;
                    model.Office_Sequence = gOfficeManager.GetOfficeSequenceByOfficeNumber(model.Office_Number);
                    gOfficeManager.UpdateUser(model);
                    if (IsAssignOfficeEnabled)
                        gOfficeManager.InsertAllowedOffices(model.Id, allowed_offices);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                GenerateDropdownData(id);

                return View("Edit", model);
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            if (!IsSystemAdmin || !IsEditUserEnabled)
            {
                Response.Redirect("/", true);
            }
            var model = gUserModuleManager.GetUserById(id);

            return View(model);
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            if (!IsSystemAdmin || !IsEditUserEnabled)
            {
                Response.Redirect("/", true);
            }
            try
            {

                var user = await _userManager.FindByIdAsync(id.ToString());
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {

                }
                else
                {

                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private void GenerateDropdownData(int? userId)
        {
            var selectedList = new List<int>();
            if (userId.HasValue)
            {
                selectedList = gOfficeManager.GetAuthenticatedOfficeListByUserId(userId.Value);
            }

            ViewBag.OfficeList = gOfficeManager.GetAllOffices().Select(i => new SelectListItem()
            {
                Text = i.ClinicName + " (" + i.Office_Number + ")",
                Value = (i.Office_Number != null ? i.Office_Number.ToString() : ""),
                Selected = selectedList.Any(x => x == i.Office_Number)
            });

        }

    }
}