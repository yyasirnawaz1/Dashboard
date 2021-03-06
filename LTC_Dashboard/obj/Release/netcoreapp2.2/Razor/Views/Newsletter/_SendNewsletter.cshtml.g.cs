#pragma checksum "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Newsletter\_SendNewsletter.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ac752b613961354cf7e213dd4a0a86ba0ef7798c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Newsletter__SendNewsletter), @"mvc.1.0.view", @"/Views/Newsletter/_SendNewsletter.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Newsletter/_SendNewsletter.cshtml", typeof(AspNetCore.Views_Newsletter__SendNewsletter))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\_ViewImports.cshtml"
using LTCDashboard;

#line default
#line hidden
#line 2 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\_ViewImports.cshtml"
using LTCDashboard.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ac752b613961354cf7e213dd4a0a86ba0ef7798c", @"/Views/Newsletter/_SendNewsletter.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67cd85531b82a25b1d9f9c9f760a4e079ce716a1", @"/Views/_ViewImports.cshtml")]
    public class Views_Newsletter__SendNewsletter : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LTCDataModel.Subscriber.SubscriptionViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Newsletter\_SendNewsletter.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(81, 508, true);
            WriteLiteral(@"<style>
    .k-content {
        min-height: 0px !important;
    }

    .col-lg-9 {
        margin-top: 8px;
    }

    .col-lg-3 {
        text-align: right;
    }
</style>
<div class=""modal-dialog"">
    <div class=""modal-content"">
        <div class=""modal-header"">
            <h4 class=""modal-title"">Send Newsletter</h4>
            <button type=""button"" class=""close"" data-dismiss=""modal"" aria-hidden=""true"">X</button>

        </div>
        <div class=""modal-body"">

            ");
            EndContext();
            BeginContext(590, 23, false);
#line 27 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Newsletter\_SendNewsletter.cshtml"
       Write(Html.AntiForgeryToken());

#line default
#line hidden
            EndContext();
            BeginContext(613, 14, true);
            WriteLiteral("\r\n            ");
            EndContext();
            BeginContext(628, 64, false);
#line 28 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Newsletter\_SendNewsletter.cshtml"
       Write(Html.ValidationSummary(true, "", new { @class = "text-danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(692, 2522, true);
            WriteLiteral(@"
            <fieldset class=""mb-3"">
                <div class=""form-group row"">
                    <label class=""col-form-label col-lg-3""> Server Time</label>
                    <div class=""col-lg-9"">
                        <label id=""lblserverTime"" class=""col-md-9 control-label forcetext-left"">
                            Server Time
                        </label>
                    </div>
                </div>
                <div class=""form-group row"">
                    <label class=""col-form-label col-lg-3"">Schedule for</label>
                    <div class=""col-lg-9"">
                        <div class=""mt-radio-inline"">
                            <label class=""mt-radio"" onclick=""Newsletter.scheduleFor('now')"">
                                <input type=""radio"" name=""rbSchedule"" id=""rbNew"" value=""now"" checked> Now
                                <span></span>
                            </label>
                            <label class=""mt-radio"" onclick=""Newsletter.sched");
            WriteLiteral(@"uleFor('future')"">
                                <input type=""radio"" name=""rbSchedule"" id=""rbFuture"" value=""future""> Future
                                <span></span>
                            </label>
                        </div>
                    </div>
                </div>
                <div class=""form-group row"">
                    <label class=""col-form-label col-lg-3"">Schedule</label>
                    <div class=""col-lg-9"">
                        <input id=""sendNewsletterDTP"" title=""datetimepicker"" class=""form-control"" />
                    </div>
                </div>
                <div class=""form-group row"">
                    <label class=""col-form-label col-lg-3"">Send as</label>
                    <div class=""col-lg-9"">
                        <div class=""mt-radio-inline"">
                            <label class=""mt-radio"" onclick=""Newsletter.sendAs('subscriber')"">
                                <input type=""radio"" name=""rbSendAs"" id=""rbSubscribers"" va");
            WriteLiteral(@"lue=""subscribers"" checked> Send to subscribers
                                <span></span>
                            </label>
                             <label class=""mt-radio"" onclick=""Newsletter.sendAs('email')"">
                                    <input type=""radio"" name=""rbSendAs"" id=""rbSingleEmail"" value=""singleemail""> Send to email
                                    <span></span>
                                </label> 
                            ");
            EndContext();
            BeginContext(3215, 101, false);
#line 71 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Newsletter\_SendNewsletter.cshtml"
                       Write(Html.TextBox("txtSendNewsletterEmail", null, new { @class = "form-control", @placeholder = "Email" }));

#line default
#line hidden
            EndContext();
            BeginContext(3316, 403, true);
            WriteLiteral(@" 
                        </div>
                    </div>
                </div>

            </fieldset>

            <div class=""modal-footer"">
                <button type=""button"" data-dismiss=""modal"" class=""btn btn-danger"">Cancel</button>
                <button type=""button"" class=""btn btn-primary"" id=""btnSend"" onclick=""Newsletter.sendNewsletter()"">Send</button>
            </div>
");
            EndContext();
            BeginContext(3738, 38, true);
            WriteLiteral("        </div>\r\n\r\n    </div>\r\n</div>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LTCDataModel.Subscriber.SubscriptionViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
