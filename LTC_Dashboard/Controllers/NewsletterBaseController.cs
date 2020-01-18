using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using LTCDataManager.Office;

namespace LTCDashboard.Controllers
{
    //[CustomAuthorize]
    public class NewsletterBaseController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
         
        private string webRootPath;
        int _UserId;
        int _OfficeNumber;
        string _OfficeName;
        public NewsletterBaseController(IHostingEnvironment hostingEnvironment )
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
        public int OfficeNumber
        {
            get
            {
                int.TryParse(User.FindFirstValue("OfficeNumber"), out var officeNumber);
                return officeNumber;
            }
        }
        public string OfficeName
        {
            get
            {
                var office = gOfficeManager.GetOfficeName(OfficeSequence);
                if (office != null)
                {

                    return office.Business_Name;
                }
                else
                {
                    return string.Empty;

                }
            }
        }


        
        public int CurrentBranchId
        {
            get
            {
                //return (User.Identity.IsAuthenticated) ? User.Identity.GetUserId() : string.Empty;
                return 1; //pending

            }
        }










    }
}