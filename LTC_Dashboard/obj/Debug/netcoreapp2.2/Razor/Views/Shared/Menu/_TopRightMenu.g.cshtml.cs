#pragma checksum "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Shared\Menu\_TopRightMenu.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "da8554b53d609ec0fb20a292eb199f92192b9093"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Menu__TopRightMenu), @"mvc.1.0.view", @"/Views/Shared/Menu/_TopRightMenu.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Menu/_TopRightMenu.cshtml", typeof(AspNetCore.Views_Shared_Menu__TopRightMenu))]
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
#line 1 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Shared\Menu\_TopRightMenu.cshtml"
using System.Security.Claims;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"da8554b53d609ec0fb20a292eb199f92192b9093", @"/Views/Shared/Menu/_TopRightMenu.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67cd85531b82a25b1d9f9c9f760a4e079ce716a1", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Menu__TopRightMenu : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-inline"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "Identity", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "/Account/Logout", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-route-returnUrl", "/Identity/Account/Login", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(31, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Shared\Menu\_TopRightMenu.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(60, 480, true);
            WriteLiteral(@"
<ul class=""navbar-nav"">
    <li class=""nav-item dropdown"">
        <div class=""headerDate"">
            <div class=""input-group"">
                <input id=""dashboardCalendar"" name=""dashboardCalendar"" type=""text"" class=""form-control daterange-left"" value="""">
                <span class=""input-group-append"">
                    <span class=""input-group-text""><i class=""icon-calendar22""></i></span>
                </span>
            </div>
        </div>
    </li>
");
            EndContext();
#line 18 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Shared\Menu\_TopRightMenu.cshtml"
      

        if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {

#line default
#line hidden
            BeginContext(652, 257, true);
            WriteLiteral(@"            <li class=""nav-item dropdown dropdown-user"">
                <a href=""#"" class=""navbar-nav-link d-flex align-items-center dropdown-toggle"" data-toggle=""dropdown"">
                    <i class=""icon-user""></i> &nbsp;
                    <span>");
            EndContext();
            BeginContext(910, 27, false);
#line 25 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Shared\Menu\_TopRightMenu.cshtml"
                     Write(User.FindFirstValue("Name"));

#line default
#line hidden
            EndContext();
            BeginContext(937, 658, true);
            WriteLiteral(@"</span>
                </a>
                <div class=""dropdown-menu dropdown-menu-right"">
                    <a href=""#"" data-toggle=""modal"" data-target=""#userprofileModal"" class=""dropdown-item""><i class=""icon-user-plus""></i> User Profile</a>
                    <a href=""#"" data-toggle=""modal"" data-target=""#userpreferenceModal"" class=""dropdown-item""><i class=""icon-user-plus""></i> User Preference</a>
                    <a href=""#"" data-toggle=""modal"" data-target=""#changePasswordHashModal"" class=""dropdown-item""><i class=""icon-envelope""></i> Change PasswordHash</a>
                    <div class=""dropdown-divider""></div>
                    ");
            EndContext();
            BeginContext(1595, 260, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "da8554b53d609ec0fb20a292eb199f92192b90937187", async() => {
                BeginContext(1714, 134, true);
                WriteLiteral("\r\n                        <button type=\"submit\" class=\"dropdown-item\"><i class=\"icon-users\"></i> Logout</button>\r\n                    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Area = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Page = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-returnUrl", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["returnUrl"] = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1855, 239, true);
            WriteLiteral("\r\n                    <div class=\"dropdown-divider\"></div>\r\n                    <a href=\"#\" data-toggle=\"modal\" data-target=\"#aboutModal\" class=\"dropdown-item\"><i class=\"fa fa-tv\"></i> About</a>\r\n                </div>\r\n            </li>\r\n");
            EndContext();
#line 39 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Shared\Menu\_TopRightMenu.cshtml"
        }
        else
        {


#line default
#line hidden
            BeginContext(2132, 63, true);
            WriteLiteral("            <li class=\"nav-item dropdown \">\r\n                <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 2195, "\"", 2233, 1);
#line 44 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Shared\Menu\_TopRightMenu.cshtml"
WriteAttributeValue("", 2202, Url.Action("Login", "Account"), 2202, 31, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2234, 206, true);
            WriteLiteral(" class=\"navbar-nav-link dropdown-toggle caret-0\">\r\n                    <i class=\"icon-users\"></i> Login\r\n                    <span class=\"d-md-none ml-2\"></span>\r\n                </a>\r\n\r\n            </li>\r\n");
            EndContext();
#line 50 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Shared\Menu\_TopRightMenu.cshtml"
        }
    

#line default
#line hidden
            BeginContext(2458, 11, true);
            WriteLiteral("</ul>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
