using LTCDashboard.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTC_Dashboard.Helper
{
    public class ModuleRestrictionActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var moduleRestriction = context.HttpContext.Request.Cookies["ModuleRestriction"];
                if (string.IsNullOrEmpty(moduleRestriction))
                {

                }
                else
                {
                    var controllerName = context.RouteData.Values["Controller"].ToString();

                    if (!IsInModule(moduleRestriction, controllerName))
                    {
                        context.Result = new RedirectResult("/"+moduleRestriction + "/Index");
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        private bool IsInModule(string moduleName, string controllerName)
        {

            var homeController = "Home";
            if (controllerName.Equals(moduleName, StringComparison.OrdinalIgnoreCase)
                || controllerName.Equals(homeController, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (moduleName.Equals("newsletter", StringComparison.OrdinalIgnoreCase))
            {
                //newsletter page has this controller
                if (controllerName.Equals("Subscribers", StringComparison.OrdinalIgnoreCase)
                    || controllerName.Equals("Report", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (moduleName.Equals("dashboard", StringComparison.OrdinalIgnoreCase))
            {

            }
            else if (moduleName.Equals("newsletter", StringComparison.OrdinalIgnoreCase))
            {

            }
            else if (moduleName.Equals("newsletter", StringComparison.OrdinalIgnoreCase))
            {

            }

            return true;
        }
    }
}
