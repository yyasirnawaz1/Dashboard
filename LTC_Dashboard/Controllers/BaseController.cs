﻿using System;
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
        public bool IsDefault
        {
            get
            {
                bool.TryParse(User.FindFirstValue("IsDefault"), out var isDefault);
                return isDefault;
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
                   
                    return office.ClinicName;
                }
                else
                {
                    return string.Empty;

                }
            }
        }


        public bool IsSystemAdmin
        {
            get
            {
                bool.TryParse(User.FindFirstValue("IsSystemAdmin"), out var isDefault);
                return isDefault;
            }
        }
        public bool IsAssignOfficeEnabled
        {
            get
            {
                bool.TryParse(User.FindFirstValue("IsAssignOfficeEnabled"), out var isDefault);
                return isDefault;
            }
        }
        public bool IsEditModuleEnabled
        {
            get
            {
                bool.TryParse(User.FindFirstValue("IsEditModuleEnabled"), out var isDefault);
                return isDefault;
            }
        }
        public bool IsEditUserEnabled
        {
            get
            {
                bool.TryParse(User.FindFirstValue("IsEditUserEnabled"), out var isDefault);
                return isDefault;
            }
        }

        //TODO: remove this method and get the connection string based on office id
        public string GetUserConnectionString()
        {
            return DbConfiguration.LtcDental;
        }

        

    }
}