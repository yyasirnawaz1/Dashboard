﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTables.AspNetCore.Mvc.Binder;
using LTCDataManager.Covid;
using LTCDataModel.Covid;
using Microsoft.AspNetCore.Mvc;

namespace LTC_Covid.Controllers
{
    public class SubscribersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Get([DataTablesRequest] DataTablesRequest requestModel)
        {
            List<gCovidSubscriber> objViewModelList = new List<gCovidSubscriber>();

            

            objViewModelList = gCovidManager.GetSubscribers();

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
                   Id = e.ID,
                   FirstName = e.FirstName + " " + e.LastName,
                   EmailAddress = e.EmailAddress,
                   LastSubscriptionStatusUpdated = e.LastSubscriptionStatusUpdated,


               })
               .ToDataTablesResponse(requestModel, totalCount, filteredCount));
        }
    }
}