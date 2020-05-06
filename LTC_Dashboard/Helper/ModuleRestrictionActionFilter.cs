using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
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
            // This method will be called on every ajax call as well. 
            // We will need to allow access to Home Controller because we have few shared methods defined in it
            //********************
            // Just compare the controllername with your cookie value. 
            // We have to make sure that the guid values are same as our controller values
            //********************
            try
            {
                // Please a debugger here
                // this method will get called on each 
                var controllerName = context.Controller.ToString();
                var actionName = context.ActionDescriptor.DisplayName;

               // helpers/common.cs use that class to get and set cookies

                var cookie = context.HttpContext.Request.Cookies["moduleRestriction"].ToString();
            }
            catch (Exception ex)
            {

            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }
    }
}
