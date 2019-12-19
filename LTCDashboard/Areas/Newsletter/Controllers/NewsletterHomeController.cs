using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LTCDashboard.Areas.Newsletter.Controllers
{
    public class NewsletterHomeController : Controller
    {

        public ActionResult GetSMSEmailUsage()
        {

            try
            {

            }
            catch (Exception)
            {
                // log exception
            }

            return PartialView("_SMSEmailUsageStatistics", null);
        }


        public JsonResult GetSMSUsageStatistics()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
            return Json(null);


        }

        public JsonResult GetLastTwelveMonthResourceUsage()
        {
            try
            {
            }
            catch (Exception ex)
            {

            }
            return Json(null);


        }


        public ActionResult GetScheduledNewsLetterStatistics(string category, string period)
        {
            try
            {

            }
            catch (Exception ex)
            {
                // log
            }
            return PartialView("_ScheduledNewsLettersStatistics", null);
        }
    }
}