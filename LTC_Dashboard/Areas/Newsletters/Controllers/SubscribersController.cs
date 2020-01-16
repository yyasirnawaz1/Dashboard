using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using DataTables.AspNetCore.Mvc.Binder;
using LTCDataModel.Subscriber;
using Microsoft.AspNetCore.Authorization;
using LTCDataManager.Subscriber;

namespace LTC_Dashboard.Areas.Newsletters.Controllers
{

    [Authorize]
    [Area("Newsletters")]

    public class SubscribersController : BaseController
    {
        private IHttpContextAccessor _accessor;

 
        public SubscribersController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor accessor) : base(hostingEnvironment)
        {
            _accessor = accessor;
        
        }
        // GET: Subscribers
        public ActionResult Index()
        {
            @ViewBag.OfficeName = OfficeName;

            return View();
        }

        public ActionResult ModifySubscription()
        {
            return View("_ModifySubscription");
        }
       
        public ActionResult Get([DataTablesRequest] DataTablesRequest requestModel)
        {
            List<gSaveSubscriber> objViewModelList = new List<gSaveSubscriber>();

            SubscriberFilterParams parameters = new SubscriberFilterParams();
            parameters.DoctorID = UserId.ToString();

            objViewModelList = gSubscriber.GetAll(parameters);

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
                query = query.Where(s => s.Id.ToString().Contains(value) ||
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
                   Id = e.Id,
                   FirstName = e.FirstName + " " + e.LastName ,
                   EmailAddress = e.EmailAddress,
                   SubscriptionStatus = e.SubscriptionStatus,
                   

               })
               .ToDataTablesResponse(requestModel, totalCount, filteredCount));
        }



        #region NewSubscription
        [HttpGet]
        public ActionResult Create()
        {
            SubscriptionViewModel objModel = new SubscriptionViewModel();


            return PartialView("_CreatePartial", objModel);

        }


        [HttpPost]
        public ActionResult Create(gSaveSubscriber subscriptionViewModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    subscriptionViewModel.DoctorID = UserId;
                    subscriptionViewModel.AddedOn = DateTime.Now;
                    subscriptionViewModel.LastSubscriptionStatusUpdated = DateTime.Now;


                    gSubscriber.Add(subscriptionViewModel);
                    // return Content("success");
                    return Json(new ResponseViewModel() { StatusCode = 1, StatusMessage = "Record Saved Successfully" });
                }
                else
                {

                    return PartialView("_CreatePartial", subscriptionViewModel);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResponseViewModel() { StatusCode = -1, StatusMessage = "Error occured" });
            }


        }
        #endregion

        #region NewSubscription
        [HttpGet]
        public ActionResult Edit(IdModel model)
        {
            SubscriptionViewModel objModel = new SubscriptionViewModel();
            objModel = gSubscriber.GetById(model.Id);

            return PartialView("_EditPartial", objModel);

        }


        [HttpPost]
        public ActionResult Edit(gSaveSubscriber subscriptionViewModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    subscriptionViewModel.DoctorID = UserId;
                    subscriptionViewModel.LastSubscriptionStatusUpdated = DateTime.Now;
                    gSubscriber.Update(subscriptionViewModel);
                    // return Content("success");
                    return Json(new ResponseViewModel() { StatusCode = 1, StatusMessage = "Record Saved Successfully" });
                }
                else
                {

                    return PartialView("_EditPartial", subscriptionViewModel);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResponseViewModel() { StatusCode = -1, StatusMessage = "Error occured" });
            }


        }
        #endregion

        #region ToggleStatus
        [HttpPost]
        public ActionResult ToggleStatus(IdModel model)
        {
            gSubscriber.ToggleStatus(model.Id, UserId.ToString());
            // return Content("success");
            return Json(new ResponseViewModel() { StatusCode = 1, StatusMessage = "Record Saved Successfully" });


        }
        #endregion

        #region Delete


        [HttpPost]
        public ActionResult Delete(IdModel model)
        {
            gSubscriber.Delete(model.Id, false);
            var json = new
            {
                success = true
            };
            return Json(json);
            // return Json(new ResponseViewModel() { StatusCode = 1, StatusMessage = "Record Saved Successfully" });


        }
        [HttpPost]
        public ActionResult DeleteAll()
        {
            gSubscriber.Delete(UserId, true);
            return Json(new ResponseViewModel() { StatusCode = 1, StatusMessage = "Record Saved Successfully" });

        }
        #endregion
    }
}