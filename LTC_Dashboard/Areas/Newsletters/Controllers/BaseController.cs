using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using LTCDataManager.Office;

namespace LTC_Dashboard.Areas.Newsletters.Controllers
{
    //[CustomAuthorize]
    public class BaseController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
         
        private string webRootPath;
        int _UserId;
        int _OfficeNumber;
        string _OfficeName;
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


        //public int CurrentLoggedInUserId
        //{
        //    get
        //    {
        //        //int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);
        //        //return userId;
        //        var office = gOfficeManager.GetOffice(User.Identity.Name);
        //        if (office != null)
        //        {
        //            _UserId = office.DoctorID;
        //            _OfficeNumber = office.Office_Number;
        //        }
        //        else
        //        {
        //            _UserId = 0;
        //            _OfficeNumber = 0;

        //        }
        //        return _UserId;

        //    }
        //}
        //public string OfficeName
        //{
        //    get
        //    {
        //        var office = gOfficeManager.GetOfficeName(CurrentOfficeId);
        //        if (office != null)
        //        {

        //            _OfficeName = office.Business_Name;
        //        }
        //        else
        //        {

        //            _OfficeName= string.Empty;

        //        }
        //        return _OfficeName;
        //    }
        //}

        //public int CurrentOfficeId
        //{
        //    get
        //    {
        //        int.TryParse(User.FindFirstValue("OfficeSequence"), out var officeSequence);
        //        return officeSequence;
        //    }
        //}
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