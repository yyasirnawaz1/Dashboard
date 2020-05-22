using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LTCDataManager.DataAccess;
using LTCDataModel.Office;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LTCOfficePortal.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        private string webRootPath;
       
        public BaseController(IHostingEnvironment hostingEnvironment )
        {
            _hostingEnvironment = hostingEnvironment;
            webRootPath = _hostingEnvironment.WebRootPath;
             
        }
        public int UserId
        {
            get
            {
                int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);
                return userId;
            }
        }

        public int OfficeSequence
        {
            get
            {
                int.TryParse(User.FindFirstValue("OfficeSequence"), out var officeSequence);
                return officeSequence;
            }
        }

        public string GetUserConnectionStringDental()
        {
            return Request.Cookies["CDental"];
        }

        public string GetUserConnectionStringForms()
        {
            return Request.Cookies["CForm"];
        }
    }
}