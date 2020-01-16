

using DataTables.AspNetCore.Mvc.Binder;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using LTCDashboard.Data;
using LTCDataManager.NewsLetter;
using LTCDataModel.NewsLetter;
using Microsoft.AspNetCore.Authorization;


namespace LTC_Dashboard.Areas.Newsletters.Controllers
{
    [Authorize]
    [Area("Newsletters")]

    public class ReportsController : BaseController
    {
        private IHttpContextAccessor _accessor;

        private readonly UserManager<ApplicationUser> _userManager;
        public ReportsController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor accessor) : base(hostingEnvironment)
        {
            _accessor = accessor;

        }
        // GET: Reports
        public ActionResult Index()
        {
            @ViewBag.OfficeName = OfficeName;

            return View();
        }

        // GET: Reports
        public ActionResult ScheduledNewsLetters()
        {
            return View();
        }
       
        [HttpGet()]
        public IActionResult Get([DataTablesRequest] DataTablesRequest dataRequest)
        {
            IEnumerable<gPatientCallListView> products = gNewsLetterManager.GetPatientCallList(UserId).Where(p=>p.Status == 1);
            int recordsTotal = products.Count();
            int recordsFilterd = recordsTotal;

            if (!string.IsNullOrEmpty(dataRequest.Search?.Value))
            {
                products = products.Where(e => e.TemplateTitle.Contains(dataRequest.Search.Value));
                recordsFilterd = products.Count();
            }
            products = products.Skip(dataRequest.Start).Take(dataRequest.Length);


             

            products = products.Skip(dataRequest.Start).Take(dataRequest.Length).ToList();


            return Json(products
                .Select(e => new
                {
                    NewsletterId = e.NewsletterId,
                    Account = e.Account,
                    AppointDate = e.AppointDate,
                    TemplateBodymarkup = e.TemplateBodymarkup,
                    TemplateSourceMarkup = e.TemplateSourceMarkup,
                    TemplateTitle = e.TemplateTitle,
                    Status = e.Status

                })
                .ToDataTablesResponse(dataRequest, recordsTotal, recordsFilterd));
        }

         
    }
}