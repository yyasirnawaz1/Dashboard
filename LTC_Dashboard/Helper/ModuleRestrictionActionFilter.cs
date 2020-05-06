using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
                var isAjaxRequest = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";
                if (context.HttpContext.Request.Cookies["ModuleRestriction"] != null)
                {
                    if (isAjaxRequest == false)
                    {
                        string moduleRestriction = (context.HttpContext.Request.Cookies["ModuleRestriction"]);

                        switch (moduleRestriction)
                        {
                            case "Newsletter":
                                if ((context.HttpContext.Request.Path.HasValue && context.HttpContext.Request.Path.Value != "/Report/Get")
                                    && context.Controller.ToString() != "LTC_Dashboard.Controllers.SubscribersController" &&
                                    context.Controller.ToString() != "LTC_Dashboard.Controllers.NewsletterController" &&
                                    context.Controller.ToString() != "LTC_Dashboard.Controllers.ImageManagementController")
                                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Newsletter" }, { "action", "Home" } });
                                //if (context.HttpContext.Request.Path.HasValue && context.HttpContext.Request.Path.Value != "/Newsletter/Home")

                                return;
                                break;
                            //case "Form":
                            //    if (context.HttpContext.Request.Path.HasValue && context.HttpContext.Request.Path.Value != "/Form/Index")
                            //        context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Form" }, { "action", "Index" } });
                            //    //LocalRedirect("/Form/Index");
                            //    break;
                            //case "Dashboard":
                            //    //LocalRedirect("/Dashboard/Index");
                            //    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Dashboard" }, { "action", "Index" } });
                            //    break;
                            default:
                                break;


                        }
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
    }
}
