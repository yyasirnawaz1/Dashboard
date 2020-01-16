using System;
using System.Globalization;
using System.Linq;
using LTCDataManager.NewsLetter;
using LTCDataModel.Enums;
using LTCDataModel.Newsletter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LTC_Dashboard.Areas.Newsletters.Controllers
{
    [Authorize]
    [Area("Newsletters")]
    public class HomeController : BaseController
    {
        private IHttpContextAccessor _accessor;

        public HomeController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor accessor) : base(hostingEnvironment)
        {
            _accessor = accessor;

        }
         

        public ActionResult Index()
        {
            @ViewBag.OfficeName = OfficeName;
           
            return View();
        }

         
 

      


        public ActionResult GetScheduledNewsLetterStatistics(string category, string period)
        {
            ScheduledNewsLetterStatisticsViewModel result = new ScheduledNewsLetterStatisticsViewModel();
            try
            {
                int intCategory = 3;
                if (category == "Sent")
                {
                    intCategory = 2;
                } else if (category == "Scheduled")
                {
                    intCategory = 1;
                }

                result.ScheduledNewsLetter = gNewsLetterManager.GetDashboard(intCategory, period, CurrentOfficeId);
                foreach (var item in result.ScheduledNewsLetter)
                {
                    item.SentTimeString = item.SentTime.ToString(@"yyyy-MM-dd hh:mm tt", new CultureInfo("en-US"));
                }

                result.ScheduledNewsLetter = result.ScheduledNewsLetter.OrderByDescending(c => c.SentTime).ToList();
                result.Scheduled = result.ScheduledNewsLetter.Count(s => s.Status == ScheduledNewsLetterStatus.Scheduled);
                result.Sent = result.ScheduledNewsLetter.Count(s => s.Status == ScheduledNewsLetterStatus.Sent);
            }
            catch (Exception ex)
            {
                // log
            }
            return PartialView("_ScheduledNewsLettersStatistics", result);
        }

    }
}