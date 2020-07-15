using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using LTC_Covid.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using LTCDataManager.Office;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using LTC_Covid.Helper;
using LTCDataModel.Covid;

namespace LTC_Covid.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<BusinessUserInfo> _signInManager;
        private readonly UserManager<BusinessUserInfo> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        public class MustBeTrueAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                return value is bool && (bool)value;
            }
        }
        public RegisterModel(
            UserManager<BusinessUserInfo> userManager,
            SignInManager<BusinessUserInfo> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }


            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }


            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }



            [Required]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }


            [Display(Name = "Address")]
            public string AddressLine1 { get; set; }

            [Display(Name = "")]
            public string AddressLine2 { get; set; }

            [Display(Name = "")]
            public string AddressLine3 { get; set; }


            [Display(Name = "City")]
            public string City { get; set; }


            [Display(Name = "Province")]
            public string Province { get; set; }

        

            [Display(Name = "Country")]
            public string Country { get; set; }


            [Display(Name = "Postal Code")]
            public string PostalCode { get; set; }

            [Required]
            [MustBeTrue(ErrorMessage = "Terms of use is Required")]
            // [Range(typeof(bool), "true", "true", ErrorMessage = "Terms of use is Required")]
            [Display(Name = "Terms of use")]
            public bool IsTermCheck { get; set; }

            [Required]
            [MustBeTrue(ErrorMessage = "Privacy policy is Required")]
            //[Range(typeof(bool), "true", "true", ErrorMessage = "Privacy policy is Required")]
            [Display(Name = "Privacy policy")]
            public bool IsProfileCheck { get; set; }

            [Required]
            [MustBeTrue(ErrorMessage = "Acknowledgement is Required")]
            //[Range(typeof(bool), "true", "true", ErrorMessage = "Acknowledgement is Required")]
            [Display(Name = "Acknowledgement")]
            public bool IsAcknowledgeCheck { get; set; }

            //[Required]
            //[Display(Name = "Office")]
            //public int? Office_Sequence { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            //GenerateDropdownData();
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new BusinessUserInfo
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    PhoneNumber = Input.PhoneNumber,
                    AddressLine1 = Input.AddressLine1,
                    AddressLine2 = Input.AddressLine2,
                    AddressLine3 = Input.AddressLine3,
                    City = Input.City,
                    Province = Input.Province,
                    Country = Input.Country,
                    PostalCode = Input.PostalCode,
                    Office_Sequence = 0,
                    CustomID = Common.GenerateCustomID(),
                    API = Common.GenerateCustomID(),
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //return LocalRedirect(returnUrl);
                    return RedirectToPage("./RegisterationConfirmation");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
          //  GenerateDropdownData();
            // If we got this far, something failed, redisplay form
            return Page();
        }

        private void GenerateDropdownData()
        {
            var selectedList = new List<int>();

            TempData["OfficeList"] = gOfficeManager.GetAllOffices().Select(i => new SelectListItem()
            {
                Text = i.ClinicName + " (" + i.Office_Number + ")",
                Value = (i.Office_Sequence != null ? i.Office_Sequence.ToString() : ""),
                Selected = selectedList.Any(x => x == i.Office_Sequence)
            });

        }
    }
}
