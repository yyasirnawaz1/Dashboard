#pragma checksum "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Subscribers\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "81dafe5fdbe0bc21f3f968d83f78abf9df92a644"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Subscribers_Index), @"mvc.1.0.view", @"/Views/Subscribers/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Subscribers/Index.cshtml", typeof(AspNetCore.Views_Subscribers_Index))]
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
#line 1 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\_ViewImports.cshtml"
using LTCDashboard;

#line default
#line hidden
#line 2 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\_ViewImports.cshtml"
using LTCDashboard.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"81dafe5fdbe0bc21f3f968d83f78abf9df92a644", @"/Views/Subscribers/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67cd85531b82a25b1d9f9c9f760a4e079ce716a1", @"/Views/_ViewImports.cshtml")]
    public class Views_Subscribers_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Content/New folder/Vendor/DataTable/jquery.dataTables.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Content/ScriptsView/Subscribers.js?version=5"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Subscribers\Index.cshtml"
  
    ViewBag.Title = "Index";
    Layout = "~/Views/Shared/Layouts/_NewsletterLayout.cshtml";


#line default
#line hidden
            BeginContext(106, 405, true);
            WriteLiteral(@"<div class=""content"">
    <div class=""row"">
        <div class=""card"" style=""width:100%"">
            <div class=""card-header bg-white header-elements-inline"">
                <h6 class=""card-title font-variant-smallCaps"">Subscribers</h6>
                <div class=""header-elements"">
                    <div class=""list-icons"">
                        <button type=""button"" class=""btn btn-danger""");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 511, "\"", 598, 3);
            WriteAttributeValue("", 521, "Subscription.removeAllSubscription(\'", 521, 36, true);
#line 14 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Subscribers\Index.cshtml"
WriteAttributeValue("", 557, Url.Action("DeleteAll", "Subscribers"), 557, 39, false);

#line default
#line hidden
            WriteAttributeValue("", 596, "\')", 596, 2, true);
            EndWriteAttribute();
            BeginContext(599, 115, true);
            WriteLiteral(">Remove All</button>\r\n                        <button type=\"button\" id=\"btnNewSubscription\" class=\"btn btn-primary\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 714, "\"", 792, 3);
            WriteAttributeValue("", 724, "Subscription.newSubscription(\'", 724, 30, true);
#line 15 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Subscribers\Index.cshtml"
WriteAttributeValue("", 754, Url.Action("Create", "Subscribers"), 754, 36, false);

#line default
#line hidden
            WriteAttributeValue("", 790, "\')", 790, 2, true);
            EndWriteAttribute();
            BeginContext(793, 11, true);
            WriteLiteral(" data-url=\"");
            EndContext();
            BeginContext(805, 35, false);
#line 15 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Subscribers\Index.cshtml"
                                                                                                                                                                                  Write(Url.Action("Create", "Subscribers"));

#line default
#line hidden
            EndContext();
            BeginContext(840, 848, true);
            WriteLiteral(@""">Add Subscriber</button>

                        <a class=""list-icons-item"" data-action=""collapse""></a>
                    </div>
                </div>
            </div>
            <div class=""card-body"">
                <div class=""row"">
                    <div class=""col-md-12"">
                        <table id=""subscriberTable"" class=""table table-striped table-hover table-checkable order-column"" style=""width:100%;""></table>
                    </div>

                </div>
            </div>
        </div>
    </div>




    <div id=""modifySubscription"" class=""modal"" tabindex=""-1"" data-width=""400"" data-keyboard=""false"" role=""dialog"" aria-labelledby=""modifySubscription"" aria-hidden=""true"" data-backdrop=""static"">
        <div id=""modifySubscriptionContainer"">

        </div>
    </div>


</div>


");
            EndContext();
            BeginContext(1741, 455, true);
            WriteLiteral(@"<div id=""div_template"" style=""display:none"">
    <div id=""div_grid_actions"">
        <div class=""btn-group"">

            <div class=""list-icons"">
                <div class=""dropdown"">
                    <a href=""#"" class=""list-icons-item"" data-toggle=""dropdown"">
                        <i class=""icon-menu9""></i>
                    </a>
                    <div class=""dropdown-menu dropdown-menu-right"">
                        <a href=""#""");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 2196, "\"", 2309, 3);
            WriteAttributeValue("", 2206, "Subscription.toggleSubscription(\'", 2206, 33, true);
#line 56 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Subscribers\Index.cshtml"
WriteAttributeValue("", 2239, Url.Action("ToggleStatus", "Subscribers", new {  Id="__prm_id__" }), 2239, 68, false);

#line default
#line hidden
            WriteAttributeValue("", 2307, "\')", 2307, 2, true);
            EndWriteAttribute();
            BeginContext(2310, 99, true);
            WriteLiteral(" class=\"dropdown-item\"><i class=\"icon-shuffle\"></i> Toggle</a>\r\n                        <a href=\"#\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 2409, "\"", 2514, 3);
            WriteAttributeValue("", 2419, "Subscription.modifySubscription(\'", 2419, 33, true);
#line 57 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Subscribers\Index.cshtml"
WriteAttributeValue("", 2452, Url.Action("Edit", "Subscribers",new { Id= "__prm_id__" }), 2452, 59, false);

#line default
#line hidden
            WriteAttributeValue("", 2511, "\');", 2511, 3, true);
            EndWriteAttribute();
            BeginContext(2515, 153, true);
            WriteLiteral(" data-target=\"#modifySubscription\" data-toggle=\"modal\" class=\"dropdown-item\"><i class=\"icon-pencil7\"></i> Modify</a>\r\n                        <a href=\"#\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 2668, "\"", 2774, 3);
            WriteAttributeValue("", 2678, "Subscription.deleteSubscription(\'", 2678, 33, true);
#line 58 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Subscribers\Index.cshtml"
WriteAttributeValue("", 2711, Url.Action("Delete", "Subscribers",new { Id="__prm_id__" }), 2711, 60, false);

#line default
#line hidden
            WriteAttributeValue("", 2771, "\');", 2771, 3, true);
            EndWriteAttribute();
            BeginContext(2775, 168, true);
            WriteLiteral(" class=\"dropdown-item\"><i class=\"icon-x\"></i> Delete</a>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            EndContext();
            DefineSection("scripts", async() => {
                BeginContext(2960, 291, true);
                WriteLiteral(@"
    <script>
        $('#dashBoard').removeClass(""active"");
        $('#subscriber').removeClass(""active"");
        $('#newsLetter').removeClass(""active"");
        $('#scheduledNewsLetters').removeClass(""active"");
        $('#subscriber').addClass(""active"");
    </script>

 
    ");
                EndContext();
                BeginContext(3251, 86, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "81dafe5fdbe0bc21f3f968d83f78abf9df92a64411462", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3337, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(3343, 123, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "81dafe5fdbe0bc21f3f968d83f78abf9df92a64412719", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3466, 9, true);
                WriteLiteral("\r\n \r\n    ");
                EndContext();
                BeginContext(3475, 70, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "81dafe5fdbe0bc21f3f968d83f78abf9df92a64414067", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3545, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591