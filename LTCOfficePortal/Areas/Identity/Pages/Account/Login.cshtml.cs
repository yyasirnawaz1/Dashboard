﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using LTCOfficePortal.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using LTCDataManager.User;
using LTCDataModel.Configurations;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace LTCOfficePortal.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ApplicationSettings _applicationSettings;

        public LoginModel(SignInManager<ApplicationUser> signInManager,
            IOptions<ApplicationSettings> applicationSettings,
            ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _applicationSettings = applicationSettings.Value;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            Response.Cookies.Delete("CDental");
            Response.Cookies.Delete("CForm");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    SetupConnectionCookie(Input.Email);
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private void SetupConnectionCookie(string email)
        {
            #region Cookie
           
            var setting = gUserModuleManager.GetConnectionString(email);
            var connectionStringTemplate = "Server={0};userid="+ _applicationSettings.UserName + ";password=" + _applicationSettings.Password + ";database={1};Port={2};Convert Zero Datetime=True;SslMode=none;Connection Timeout=190;";

            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),
                IsEssential = true
            };
            // get office detail here

            Response.Cookies.Append("CDental", string.Format(connectionStringTemplate,setting.Dental_DB_IP,setting.Dental_DB_Name,setting.Dental_DB_Port), options);
            Response.Cookies.Append("CForm", string.Format(connectionStringTemplate, setting.Form_DB_IP, setting.Form_DB_Name, setting.Form_DB_Port), options);

            #endregion
        }
    }
}
