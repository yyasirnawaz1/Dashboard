using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTables.AspNetCore.Mvc.Binder;
using LTC_Covid.Helper;
using LTC_Covid.Models;
using LTCDataManager.Covid;
using LTCDataModel.Covid;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LTC_Covid.Controllers
{

    public class SubscribersController : BaseController
    {
        private readonly IDataProtector _protector;
        public SubscribersController(IDataProtectionProvider provider, IHostingEnvironment hostingEnvironment, DataProtectionPurposeStrings dataProtectionPurposeStrings) : base(hostingEnvironment)
        {
            _protector = provider.CreateProtector(dataProtectionPurposeStrings.SubAndQueueID);

        }

        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Delete(IdModel model)
        {
            var subId = Convert.ToInt32(_protector.Unprotect(model.Id));
            gCovidManager.DeleteSubscriber(subId);
            var json = new
            {
                success = true
            };
            return Json(json);
            // return Json(new ResponseViewModel() { StatusCode = 1, StatusMessage = "Record Saved Successfully" });


        }

        [HttpGet]
        public ActionResult GetDetail(IdModel model)
        {
            var subId = Convert.ToInt32(_protector.Unprotect(model.Id));
            gCovidSubscriber objModel = new gCovidSubscriber();
            objModel = gCovidManager.GetSubscriberById(subId);
            var json = new
            {
                success = true,
                obj = objModel
            };
            return Json(json);

        }

        public ActionResult Upsert([FromBody]gCovidSubscriber model)
        {
            try
            {
                model.LastSubscriptionStatusUpdated = DateTime.Now;
                model.Office_Sequence = 0;
                model.BusinessInfo_ID = UserId;
                model.SubscriptionStatus = true;

                if (model.ID < 1)
                    model.CustomID = Common.GenerateCustomID();

                var sub = gCovidManager.GetByEmail(model.EmailAddress);
                if (sub != null && model.ID < 1)
                {
                    var json = new
                    {
                        Message = "Subscriber Already Exists",
                        success = false,
                    };
                    return Json(json);
                }
                else
                {

                    gCovidManager.SaveSubscriber(model);
                    var json = new
                    {
                        success = true,
                    };
                    return Json(json);

                }

            }
            catch (Exception)
            {
                return null;
            }


        }
        //[AllowAnonymous]
        [HttpGet]
        public ActionResult GetAll()
        {

            try
            {

                List<gCovidSubscriber> objViewModelList = new List<gCovidSubscriber>();
                objViewModelList = gCovidManager.GetSubscribers(UserId);

                return Json(objViewModelList
              .Select(e => new
              {
                  ID = _protector.Protect(e.ID.ToString()),
                  BusinessInfo_ID = _protector.Protect(e.BusinessInfo_ID.ToString()),
                  Salutation = e.Salutation,
                  FirstName = e.FirstName,
                  LastName = e.LastName,
                  MiddleInitial = e.MiddleInitial,
                  EmailAddress = e.EmailAddress,
                  SubscriptionStatus = e.SubscriptionStatus,
                  Office_Sequence = e.Office_Sequence,
                  PatientNumber = e.PatientNumber,


                  LastSubscriptionStatusUpdated = e.LastSubscriptionStatusUpdated,

                  CustomID = e.CustomID,

              }));

            }
            catch (Exception)
            {
                return null;
            }
        }
        public ActionResult Get([DataTablesRequest] DataTablesRequest requestModel)
        {
            List<gCovidSubscriber> objViewModelList = new List<gCovidSubscriber>();



            objViewModelList = gCovidManager.GetSubscribers(UserId);

            var totalCount = 0;
            var filteredCount = 0;



            var query = from s in objViewModelList
                        select s;
            totalCount = query.Count();

            #region Filtering
            //search Filters
            if (!string.IsNullOrEmpty(requestModel.Search?.Value))
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(s => s.ID.ToString().Contains(value) ||
                                         s.FirstName.Contains(value) ||
                                         s.LastName.Contains(value) ||
                                         s.EmailAddress.Contains(value)

                                   );
            }

            filteredCount = query.Count();

            #endregion Filtering



            objViewModelList = query.Skip(requestModel.Start).Take(requestModel.Length).ToList();




            //return Json(new DataTablesResponse(requestModel.Draw, filteredCount, totalCount, objViewModelList));
            return Json(objViewModelList
               .Select(e => new
               {
                   Id = _protector.Protect(e.ID.ToString()),
                   FirstName = e.FirstName + " " + e.LastName,
                   EmailAddress = e.EmailAddress,
                   LastSubscriptionStatusUpdated = e.LastSubscriptionStatusUpdated.ToString("yyyy-MM-dd"),


               })
               .ToDataTablesResponse(requestModel, totalCount, filteredCount));
        }
    }
}