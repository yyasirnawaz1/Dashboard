using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTCAdminPortal.Data;
using LTCDataManager.User;
using LTCDataModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LTCAdminPortal.Controllers
{
    public class UsersController : Controller
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
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ApplicationUser model)
        {
            try
            {
                model.UserName = model.Email;
                model.EmailConfirmed = true;
                model.PhoneNumberConfirmed = true;

                var result = await _userManager.CreateAsync(model, model.PasswordHash);
                if (result.Succeeded)
                {

                }
                else
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


                    return View();

                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var model = gUserModuleManager.GetUserById(id);

            return View(model);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ApplicationUser model)
        {
            try
            {
                model.Id = id;
                model.UserName = model.Email;
                model.EmailConfirmed = true;
                model.PhoneNumberConfirmed = true;

                var result = await _userManager.UpdateAsync(model);
                if (result.Succeeded)
                {

                }
                else
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
                        

                    return View();

                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
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
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}