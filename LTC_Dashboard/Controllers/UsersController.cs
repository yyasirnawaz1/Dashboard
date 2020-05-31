using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTCDashboard.Controllers;
using LTCDataManager.Office;
using LTCDataManager.User;
using LTCDataModel.Office;
using LTCDataModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LTC_Dashboard.Controllers
{
    public class UsersController : BaseController
    {

        private readonly UserManager<ApplicationUser> _userManager;
        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            var model = gUserModuleManager.GetAllUsers();

            return View(model);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            var model = gUserModuleManager.GetUserById(id);

            return View(model);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            GenerateDropdownData(null);
            return View();
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
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                GenerateDropdownData(null);
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var model = gUserModuleManager.GetUserById(id);

            GenerateDropdownData(id);

            return View(model);
        }


        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string oldPassword, List<int> allowed_offices, ApplicationUser model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserName = model.Email;
                    model.EmailConfirmed = true;
                    model.PhoneNumberConfirmed = true;
                    model.Office_Sequence = gOfficeManager.GetOfficeSequenceByOfficeNumber(model.Office_Number);
                    gOfficeManager.UpdateUser(model);
                    gOfficeManager.InsertAllowedOffices(model.Id, allowed_offices);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                GenerateDropdownData(id);

                return View("Edit",model);
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            var model = gUserModuleManager.GetUserById(id);

            return View(model);
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
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
            if(userId.HasValue)
            {
                selectedList = gOfficeManager.GetAuthenticatedOfficeListByUserId(userId.Value);
            }

            ViewBag.OfficeList = gOfficeManager.GetAllOffices().Select(i => new SelectListItem()
            {
                Text = i.ClinicName + " (" + i.Office_Number + ")",
                Value = (i.Office_Number != null ? i.Office_Number.ToString() : ""),
                Selected = selectedList.Any(x=>x == i.Office_Number)
            });
        }

    }
}