using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LTCDashboard.Controllers
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

        public int OfficeSequence
        {
            get
            {
                int.TryParse(User.FindFirstValue("OfficeSequence"), out var officeSequence);
                return officeSequence;
            }
        }

        //TODO: remove this method and get the connection string based on office id
        public string GetUserConnectionString()
        {
            throw new Exception("Figure this part out, get the connection string based on the selected office");
        }

        

    }
}