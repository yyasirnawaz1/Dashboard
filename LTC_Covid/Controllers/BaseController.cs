using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using LTCDataManager.DataAccess;
using LTCDataManager.Office;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LTC_Covid.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public int UserId
        {
            get
            {
                int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);
                return userId;
            }
        }
        public string UserName
        {
            get
            {
                return User.FindFirstValue("Name");
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

    }
}