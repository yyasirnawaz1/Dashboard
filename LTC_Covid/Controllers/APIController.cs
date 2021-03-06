﻿using System;
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
using Twilio.Rest.Video.V1.Room.Participant;
using System.Configuration;

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


                var duplicate = await _userManager.FindByEmailAsync(email);
                if (duplicate != null)
                {
                    return Json(new { Data = true, Operation = "Already Registered", CustomID = duplicate.CustomID });
                }
                else
                {
                    string customId = Common.GenerateCustomID();
                    var password = Common.GeneratePassword();
                    var user = new BusinessUserInfo
                    {
                        UserName = email,
                        Email = email,
                        Office_Sequence = office,
                        CustomID = customId,
                        API = api,
                    };

                    var result = await _userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code = code },
                            protocol: Request.Scheme);

                        //await _emailSender.SendEmailAsync(email, "Confirm your email",
                        //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>. <br /> Your current password is " + password + "<br /> Please make sure to change your password.");

                        return Json(new { Data = true, Operation = "Added", CustomID = user.CustomID });

                    }
                    else
                    {
                        foreach (var err in result.Errors)
                        {
                            error += err.Description + " , ";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return Json(new { Data = false, Operation = error });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/deleteuser")]
        public async Task<IActionResult> DeleteUser(string api = "", int office = 0, string email = "")
        {
            string errors = "";
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    if (user.Office_Sequence == office && user.API == api)
                    {
                        var result = await _userManager.DeleteAsync(user);
                        if (result.Succeeded)
                        {
                            return Json(new { Data = true, Operation = "Deleted", ID = user.CustomID });
                        }
                        else
                        {
                            foreach (var err in result.Errors)
                            {
                                errors += err.Description + " , ";
                            }
                        }
                    }
                    else
                    {
                        if (user.API != api)
                            errors = "Invalid API";
                        else if (user.Office_Sequence != office)
                            errors = "User Not Registere with office " + office;
                    }

                }
                else
                {
                    errors = "User Not Found";
                }

            }
            catch (Exception ex)
            {
                errors = ex.Message.ToString();
            }
            return Json(new { Data = false, Operation = errors });
        }



        [AllowAnonymous]
        [Route("/createsubscriber")]
        [HttpGet]
        public IActionResult CreateSubscriber(string api = "", int office = 0, string email = "", string lastname = "", string firstname = "", string salutation = "", int pno = 0, string cell = "", bool AddIfNotFound = true)
        {
            string error = "";
            try
            {
                var subscriber = gCovidManager.GetSubscriberByPatientNumberAndOfficeSequence(pno, office, email);
                if (subscriber != null)
                {
                    var sub = gCovidManager.GetByEmail(email);
                    if (sub != null)
                    {
                        if (sub.ID != subscriber.ID)// check if different user exists with same email
                        {
                            return Json(new
                            {
                                Operation = "Duplicate Email",
                                Data = false,
                                Message = "Email registered with another office",
                                CustomID = sub.CustomID
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
                            Operation = "Duplicate Email",
                            Data = true,
                            Message = "Email registered with another office",
                            CustomID = sub.CustomID
                        });
                    }
                }

                int id = 0;
                string customId = "";
                if (subscriber != null)
                {
                    id = subscriber.ID;
                    customId = subscriber.CustomID;
                }
                else
                {
                    customId = Common.GenerateCustomID();
                }

                var businessInfoId = gCovidManager.GetFirstUserIdByOffice(office);

                var newSubscriberDetail = new gCovidSubscriber
                {
                    ID = id,
                    Office_Sequence = office,
                    Salutation = salutation,
                    LastName = lastname,
                    FirstName = firstname,
                    EmailAddress = email,
                    LastSubscriptionStatusUpdated = DateTime.Now,
                    BusinessInfo_ID = businessInfoId,
                    SubscriptionStatus = true,
                    PatientNumber = pno,
                    CustomID = customId,
                    CellPhone = cell
                };

                var newid = 0;
                if ((!AddIfNotFound && id != 0) || AddIfNotFound)
                {
                    newid = gCovidManager.SaveSubscriber(newSubscriberDetail);
                    if (!AddIfNotFound && id != 0)
                    {
                        return Json(new { Data = true, Operation = "Updated", CustomID = customId });
                    }
                }
                else
                {
                    return Json(new { Data = true, Operation = "Not Found", CustomID = "" });
                }

                if (newid != id)
                {
                    return Json(new { Data = true, Operation = "Added", CustomID = customId });
                }
                else
                {
                    return Json(new { Data = true, Operation = "Already Registered", CustomID = customId });
                }


            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return Json(new
            {
                Data = false,
                Operation = error
            });
        }


        [AllowAnonymous]
        [Route("/deletesubscriber")]
        [HttpGet]
        public IActionResult DeleteSubscriber(string api = "", int office = 0, int pno = 0)
        {
            string error = "";
            try
            {
                var numberOfDeletedRecords = gCovidManager.DeleteSubscriberByPatientNumberAndOfficeSequence(pno, office);
                if (numberOfDeletedRecords > 0)
                {
                    return Json(new { Data = true, Operation = "Deleted" });
                }
                else
                {
                    error = "No Records Found";
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }

            return Json(new
            {
                Data = false,
                Operation = error
            });
        }


        [AllowAnonymous]
        [Route("/covidform")]
        public async Task<IActionResult> AutoLogin(string API, string CustomId)
        {
            var user = gCovidManager.GetUserByCustomIdANDApiKey(API, CustomId);
            if (user != null)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect("~/Home/ViewForms");
            }
            else
            {
                ViewData["Error Message"] = "Invalid Custom Id";
                return View("Error");
            }
        }


        [AllowAnonymous]
        [Route("/RetrieveCOVIDForm")]
        public IActionResult RetrieveCOVIDForm(string API, string FormCustomID = "", string CustomId = "", int? counter = null, int? fa = null)
        {
            string error = "";

            if (!string.IsNullOrEmpty(FormCustomID))
            {

                var formData = gCovidManager.GetCovidFormByCustomId(FormCustomID);
                if (formData != null)
                {
                    return Json(new
                    {
                        Data = true,
                        Operation = new
                        {
                            BusinessInfo_ID = formData.BusinessInfo_ID,
                            QueueID = formData.QueueID,
                            FormID = formData.FormID,
                            SubscriberID = formData.SubscriberID,
                            IsCOVIDPossible = formData.IsCOVIDPossible,
                            IsPreScreen = formData.IsPreScreen,
                            PreScreenDate = formData.PreScreenDate,
                            IsInPersonScreen = formData.IsInPersonScreen,
                            InPersonScreenDate = formData.InPersonScreenDate,
                            StorageInJson = formData.StorageInJson,
                            CustomID = formData.CustomID,
                            Counter = formData.Counter,
                            FormAction = formData.FormAction
                        }
                    });
                }
                else
                {
                    error = "Form doesn't exist";
                }

            }
            else if (!string.IsNullOrEmpty(CustomId) && counter.HasValue && fa.HasValue)
            {
                var formData = gCovidManager.GetFormEntryByCounterAndFormActionAndSubscriberCustomId(CustomId, counter.Value, fa.Value);
                if (formData != null)
                {
                    return Json(new
                    {
                        Data = true,
                        Operation = new
                        {
                            BusinessInfo_ID = formData.BusinessInfo_ID,
                            QueueID = formData.QueueID,
                            FormID = formData.FormID,
                            SubscriberID = formData.SubscriberID,
                            IsCOVIDPossible = formData.IsCOVIDPossible,
                            IsPreScreen = formData.IsPreScreen,
                            PreScreenDate = formData.PreScreenDate,
                            IsInPersonScreen = formData.IsInPersonScreen,
                            InPersonScreenDate = formData.InPersonScreenDate,
                            StorageInJson = formData.StorageInJson,
                            CustomID = formData.CustomID,
                            Counter = formData.Counter,
                            FormAction = formData.FormAction
                        }
                    });
                }
                else
                {
                    error = "Form doesn't exist";
                    //data false , form data not found
                }

            }
            else
            {
                error = "Required Parameters Missing";
            }

            return Json(new
            {
                Data = false,
                Operation = error
            });
        }

        [AllowAnonymous]
        [Route("/RetrieveForms")]
        [HttpGet]
        public IActionResult RetrieveForms(string api = "", string customid = "")
        {
            string error = "";
            try
            {
                if (!string.IsNullOrEmpty(customid))
                {
                    var formData = gCovidManager.GetCovidFormListByCustomId(customid);
                    if (formData != null)
                    {
                        return Json(new
                        {
                            Data = true,
                            Operation = formData
                        });
                    }
                    else
                    {
                        error = "Form doesn't exist";
                    }

                }
                else
                {
                    return Json(new
                    {
                        Data = true,
                        Message = "Missing CustomId",
                    });
                }

            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return Json(new
            {
                Data = false,
                Operation = error
            });
        }


        [AllowAnonymous]
        [Route("/RetrievePDF")]
        [HttpGet]
        public IActionResult RetrievePDF(string api = "", string formCustomId = "", string CustomId = "", int? counter = null, int? fa = null)
        {
            string error = "";
            try
            {
                if (!string.IsNullOrEmpty(formCustomId))
                {
                    var pdf = gCovidManager.GetPdfByFormCustomId(formCustomId);
                    if (pdf != null)
                    {
                        return Json(new
                        {
                            Data = true,
                            PDF = pdf
                        });
                    }
                    else
                    {
                        error = "PDF doesn't exist";
                    }
                }
                else if (!string.IsNullOrEmpty(CustomId) && counter.HasValue && fa.HasValue)
                {
                    var pdf = gCovidManager.GetPDFByCounterAndFormActionAndSubscriberCustomId(CustomId, counter.Value, fa.Value);
                    if (pdf != null)
                    {
                        return Json(new
                        {
                            Data = true,
                            PDF = pdf
                        });
                    }
                    else
                    {
                        error = "PDF doesn't exist";
                    }
                }
                else
                {
                    return Json(new
                    {
                        Data = true,
                        Message = "Missing Parameters",
                    });
                }

            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return Json(new
            {
                Data = false,
                Operation = error
            });
        }

        [AllowAnonymous]
        [Route("/ViewPDF")]
        [HttpGet]
        public IActionResult ViewPDF(string api = "", string formCustomId = "", string CustomId = "", int? counter = null, int? fa = null)
        {
            string error = "";
            try
            {
                if (!string.IsNullOrEmpty(formCustomId))
                {
                    var pdf = gCovidManager.GetPdfByFormCustomId(formCustomId);
                    if (pdf != null)
                    {
                        var content = new System.IO.MemoryStream(pdf);
                        var contentType = "application/pdf";

                        System.Net.Mime.ContentDisposition contentDisposition = new System.Net.Mime.ContentDisposition
                        {
                            FileName = formCustomId + ".pdf",
                            Inline = true
                        };
                        Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
                        return File(content, contentType);
                    }
                    else
                    {
                        error = "PDF doesn't exist";
                    }
                }
                else if (!string.IsNullOrEmpty(CustomId) && counter.HasValue && fa.HasValue)
                {
                    var pdf = gCovidManager.GetPDFByCounterAndFormActionAndSubscriberCustomId(CustomId, counter.Value, fa.Value);
                    if (pdf != null)
                    {
                        var content = new System.IO.MemoryStream(pdf);
                        var contentType = "application/pdf";

                        System.Net.Mime.ContentDisposition contentDisposition = new System.Net.Mime.ContentDisposition
                        {
                            FileName = formCustomId + ".pdf",
                            Inline = true
                        };
                        Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
                        return File(content, contentType);
                    }
                    else
                    {
                        error = "PDF doesn't exist";
                    }
                }
                else
                {
                    return Json(new
                    {
                        Data = true,
                        Message = "Missing Parameters",
                    });
                }

            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return Json(new
            {
                Data = false,
                Operation = error
            });
        }



    }
}