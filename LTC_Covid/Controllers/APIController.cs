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
using LTCDataModel.Covid;
using LTCDataManager.Covid;
using System.Text;
using DataTables.AspNetCore.Mvc.Binder;
using LTC_Covid.Helper;
using LTCDataModel.User;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http2;
namespace LTC_Covid.Controllers
{
    public class APIController : Controller
    {
        private ConfigSettings _configuration;
        private Mapping _mapping;
        private gOfficeSummaryManager _gOfficeSummaryManager;
        private readonly UserManager<BusinessUserInfo> _userManager;
        private readonly SignInManager<BusinessUserInfo> _signInManager;
        private readonly IEmailSender _emailSender;

        public APIController(IOptions<ConfigSettings> configuration,
            IOptions<Mapping> mapping,
            UserManager<BusinessUserInfo> userManager,
            SignInManager<BusinessUserInfo> signInManager,
            IEmailSender emailSender)
        {
            _configuration = configuration.Value;
            _mapping = mapping.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _gOfficeSummaryManager = new gOfficeSummaryManager(configuration);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/createuser")]
        public async Task<IActionResult> CreateUser(string api = "", int office = 0, string email = "")
        {
            string error = "";
            try
            {
                var password = Common.GeneratePassword();
                var user = new BusinessUserInfo
                {
                    UserName = email,
                    Email = email,
                    Office_Sequence = office,
                    CustomID = Common.GenerateCustomID(),
                    API = api,
                };
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>. <br /> Your current password is " + password + "<br /> Please make sure to change your password.");

                    return Json(new { data = true, Message = "" });

                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        error += err.Description + " , ";
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return Json(new { data = false, Message = error });
        }

        [AllowAnonymous]
        [Route("/createsubscriber")]
        [HttpGet]
        public IActionResult CreateSubscriber(string api = "", int office = 0, string email = "", string lastname = "", string firstname = "", string salutation = "", int pno = 0)
        {
            string error = "";
            try
            {
                var subscriber = gCovidManager.GetSubscriberByPatientNumberAndOfficeSequence(pno, office);
                if (subscriber != null)
                {
                    var sub = gCovidManager.GetByEmail(email);
                    if (sub != null)
                    {
                        if (sub.ID != subscriber.ID)// check if different user exists with same email
                        {
                            return Json(new
                            {
                                Message = "Another subscriber with same email already exists",
                                success = false,
                            });
                        }
                    }

                }
                else
                {
                    var sub = gCovidManager.GetByEmail(email);
                    if (sub != null)
                    {
                        return Json(new
                        {
                            Message = "Another subscriber with same email already exists",
                            success = false,
                        });
                    }
                }

                int id = 0;
                if (subscriber != null)
                {
                    id = subscriber.ID;
                }

                //upsert sub
                gCovidManager.SaveSubscriber(new gCovidSubscriber
                {
                    ID = id,
                    Office_Sequence = office,
                    Salutation = salutation,
                    LastName = lastname,
                    FirstName = firstname,
                    EmailAddress = email,
                    LastSubscriptionStatusUpdated = DateTime.Now,
                    BusinessInfo_ID = 1,
                    SubscriptionStatus = true,
                    PatientNumber = pno,
                    CustomID = Common.GenerateCustomID()
                });

                return Json(new { data = true, Message = "" });

            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return Json(new
            {
                data = false,
                Message = error
            });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Test()
        {
            await _emailSender.SendEmailAsync("command.yasir@gmail.com", "Confirm your email",
                        $"Please confirm your account by <a href='aaa'>clicking here</a>.");

            return Json(true);
        }
    }
}