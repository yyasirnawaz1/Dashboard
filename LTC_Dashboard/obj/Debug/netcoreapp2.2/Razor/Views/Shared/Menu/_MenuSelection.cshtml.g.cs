#pragma checksum "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c3b53e6e9efb701613efb67b3682eb554dd01329"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Menu__MenuSelection), @"mvc.1.0.view", @"/Views/Shared/Menu/_MenuSelection.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Menu/_MenuSelection.cshtml", typeof(AspNetCore.Views_Shared_Menu__MenuSelection))]
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
#line 1 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
using System.Security.Claims;

#line default
#line hidden
#line 2 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
using LTCDashboard.Models;

#line default
#line hidden
#line 3 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
using LTCDataManager.User;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c3b53e6e9efb701613efb67b3682eb554dd01329", @"/Views/Shared/Menu/_MenuSelection.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67cd85531b82a25b1d9f9c9f760a4e079ce716a1", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Menu__MenuSelection : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(87, 342, true);
            WriteLiteral(@"

<ul class=""navbar-nav"">
    <li class=""nav-item dropdown"">
        <a href=""#"" class=""navbar-nav-link dropdown-toggle caret-0"" data-toggle=""dropdown"">
            <i class=""icon-git-compare""></i>
            <span class=""d-md-none ml-2"">Navigate</span>
        </a>
        <div class=""dropdown-menu dropdown-content wmin-md-350"">
");
            EndContext();
            BeginContext(580, 83, true);
            WriteLiteral("            <div class=\"dropdown-content-body dropdown-scrollable fullNavMenu\">\r\n\r\n");
            EndContext();
#line 18 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                  

                    int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);


#line default
#line hidden
            BeginContext(786, 47, true);
            WriteLiteral("                    <ul class=\"media-list\">\r\n\r\n");
            EndContext();
#line 24 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                         if (gUserModuleManager.GetAuthenticationModule(userId, "dashboard"))
                        {

#line default
#line hidden
            BeginContext(955, 528, true);
            WriteLiteral(@"                            <li class=""media"" onclick=""Layout.openMenu('dashboard');"">
                                <div class=""mr-2"">
                                    <a href=""#"" class=""btn bg-transparent border-primary text-primary rounded-round border-2 btn-icon""><i class=""icon-meter2""></i></a>
                                </div>
                                <div class=""media-body"">
                                    Dashboard
                                </div>
                            </li>
");
            EndContext();
#line 34 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                        }
                        else
                        {

#line default
#line hidden
            BeginContext(1567, 539, true);
            WriteLiteral(@"                            <li class=""media"" onclick=""alert('Selected option not subscribed');"">
                                <div class=""mr-2"">
                                    <a href=""#"" class=""btn bg-transparent border-primary text-primary rounded-round border-2 btn-icon""><i class=""icon-meter2""></i></a>
                                </div>
                                <div class=""media-body"">
                                    Dashboard
                                </div>
                            </li>
");
            EndContext();
#line 45 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"

                        }

#line default
#line hidden
            BeginContext(2135, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 48 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                         if (gUserModuleManager.GetAuthenticationModule(userId, "officemanagement"))
                        {

#line default
#line hidden
            BeginContext(2266, 538, true);
            WriteLiteral(@"                            <li class=""media"" onclick=""Layout.openMenu('officemanagement');"">
                                <div class=""mr-2"">
                                    <a href=""#"" class=""btn bg-transparent border-warning text-warning rounded-round border-2 btn-icon""><i class=""icon-atom""></i></a>
                                </div>
                                <div class=""media-body"">
                                    Paradigm Cloud
                                </div>
                            </li>
");
            EndContext();
#line 58 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                        }
                        else
                        {

#line default
#line hidden
            BeginContext(2888, 542, true);
            WriteLiteral(@"                            <li class=""media"" onclick=""alert('Selected option not subscribed');"">
                                <div class=""mr-2"">
                                    <a href=""#"" class=""btn bg-transparent border-warning text-warning rounded-round border-2 btn-icon""><i class=""icon-atom""></i></a>
                                </div>
                                <div class=""media-body"">
                                    Paradigm Cloud
                                </div>
                            </li>
");
            EndContext();
#line 69 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                        }

#line default
#line hidden
            BeginContext(3457, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 71 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                         if (gUserModuleManager.GetAuthenticationModule(userId, "report"))
                        {

#line default
#line hidden
            BeginContext(3578, 524, true);
            WriteLiteral(@"                            <li class=""media"" onclick=""Layout.openMenu('report');"">
                                <div class=""mr-2"">
                                    <a href=""#"" class=""btn bg-transparent border-info text-info rounded-round border-2 btn-icon""><i class=""icon-printer""></i></a>
                                </div>
                                <div class=""media-body"">
                                    Office Portal
                                </div>
                            </li>
");
            EndContext();
#line 81 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                        }
                        else
                        {

#line default
#line hidden
            BeginContext(4186, 538, true);
            WriteLiteral(@"                            <li class=""media"" onclick=""alert('Selected option not subscribed');"">
                                <div class=""mr-2"">
                                    <a href=""#"" class=""btn bg-transparent border-info text-info rounded-round border-2 btn-icon""><i class=""icon-printer""></i></a>
                                </div>
                                <div class=""media-body"">
                                    Office Portal
                                </div>
                            </li>
");
            EndContext();
#line 92 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                        }

#line default
#line hidden
            BeginContext(4751, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 94 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                         if (gUserModuleManager.GetAuthenticationModule(userId, "form"))
                        {

#line default
#line hidden
            BeginContext(4870, 523, true);
            WriteLiteral(@"                            <li class=""media"" onclick=""Layout.openMenu('form');"">
                                <div class=""mr-2"">
                                    <a href=""#"" class=""btn bg-transparent border-success text-success rounded-round border-2 btn-icon""><i class=""icon-pencil3""></i></a>
                                </div>
                                <div class=""media-body"">
                                    Designer
                                </div>
                            </li>
");
            EndContext();
#line 104 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                        }
                        else
                        {

#line default
#line hidden
            BeginContext(5477, 539, true);
            WriteLiteral(@"                            <li class=""media"" onclick=""alert('Selected option not subscribed');"">
                                <div class=""mr-2"">
                                    <a href=""#"" class=""btn bg-transparent border-success text-success rounded-round border-2 btn-icon""><i class=""icon-pencil3""></i></a>
                                </div>
                                <div class=""media-body"">
                                    Designer
                                </div>
                            </li>
");
            EndContext();
#line 115 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                        }

#line default
#line hidden
            BeginContext(6043, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 117 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                         if (gUserModuleManager.GetAuthenticationModule(userId, "newsletter"))
                        {

#line default
#line hidden
            BeginContext(6168, 532, true);
            WriteLiteral(@"                            <li class=""media"" onclick=""Layout.openMenu('newsletter');"">
                                <div class=""mr-2"">
                                    <a href=""#"" class=""btn bg-transparent border-primary text-primary rounded-round border-2 btn-icon""><i class=""icon-magazine""></i></a>
                                </div>
                                <div class=""media-body"">
                                    Newsletter
                                </div>
                            </li>
");
            EndContext();
#line 127 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                        }
                        else
                        {

#line default
#line hidden
            BeginContext(6784, 542, true);
            WriteLiteral(@"                            <li class=""media"" onclick=""alert('Selected option not subscribed');"">
                                <div class=""mr-2"">
                                    <a href=""#"" class=""btn bg-transparent border-primary text-primary rounded-round border-2 btn-icon""><i class=""icon-magazine""></i></a>
                                </div>
                                <div class=""media-body"">
                                    Newsletter
                                </div>
                            </li>
");
            EndContext();
#line 138 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                        }

#line default
#line hidden
            BeginContext(7353, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 140 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                         if (gUserModuleManager.GetAuthenticationModule(userId, "report"))
                        {

#line default
#line hidden
            BeginContext(7474, 515, true);
            WriteLiteral(@"                            <li class=""media"" onclick=""Layout.openMenu('report');"">
                                <div class=""mr-2"">
                                    <a href=""#"" class=""btn bg-transparent border-info text-info rounded-round border-2 btn-icon""><i class=""icon-mail5""></i></a>
                                </div>
                                <div class=""media-body"">
                                    Review
                                </div>
                            </li>
");
            EndContext();
#line 150 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                        }
                        else
                        {

#line default
#line hidden
            BeginContext(8073, 529, true);
            WriteLiteral(@"                            <li class=""media"" onclick=""alert('Selected option not subscribed');"">
                                <div class=""mr-2"">
                                    <a href=""#"" class=""btn bg-transparent border-info text-info rounded-round border-2 btn-icon""><i class=""icon-mail5""></i></a>
                                </div>
                                <div class=""media-body"">
                                    Review
                                </div>
                            </li>
");
            EndContext();
#line 161 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                        }

#line default
#line hidden
            BeginContext(8629, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
#line 164 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                         if (gUserModuleManager.GetAuthenticationModule(userId, "survey"))
                        {

#line default
#line hidden
            BeginContext(8752, 521, true);
            WriteLiteral(@"                            <li class=""media"" onclick=""Layout.openMenu('survey');"">
                                <div class=""mr-2"">
                                    <a href=""#"" class=""btn bg-transparent border-success text-success rounded-round border-2 btn-icon""><i class=""icon-stack""></i></a>
                                </div>
                                <div class=""media-body"">
                                    Survey
                                </div>
                            </li>
");
            EndContext();
#line 174 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                        }
                        else
                        {

#line default
#line hidden
            BeginContext(9357, 535, true);
            WriteLiteral(@"                            <li class=""media"" onclick=""alert('Selected option not subscribed');"">
                                <div class=""mr-2"">
                                    <a href=""#"" class=""btn bg-transparent border-success text-success rounded-round border-2 btn-icon""><i class=""icon-stack""></i></a>
                                </div>
                                <div class=""media-body"">
                                    Survey
                                </div>
                            </li>
");
            EndContext();
#line 185 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                        }

#line default
#line hidden
            BeginContext(9919, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 187 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                         if (gUserModuleManager.GetAuthenticationModule(userId, "report"))
                        {

#line default
#line hidden
            BeginContext(10040, 526, true);
            WriteLiteral(@"                            <li class=""media"" onclick=""Layout.openMenu('report');"">
                                <div class=""mr-2"">
                                    <a href=""#"" class=""btn bg-transparent border-primary text-primary rounded-round border-2 btn-icon""><i class=""icon-users2""></i></a>
                                </div>
                                <div class=""media-body"">
                                    My Contact
                                </div>
                            </li>
");
            EndContext();
#line 197 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                        }
                        else
                        {


#line default
#line hidden
            BeginContext(10652, 540, true);
            WriteLiteral(@"                            <li class=""media"" onclick=""alert('Selected option not subscribed');"">
                                <div class=""mr-2"">
                                    <a href=""#"" class=""btn bg-transparent border-primary text-primary rounded-round border-2 btn-icon""><i class=""icon-users2""></i></a>
                                </div>
                                <div class=""media-body"">
                                    My Contact
                                </div>
                            </li>
");
            EndContext();
#line 209 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Menu\_MenuSelection.cshtml"
                        }

#line default
#line hidden
            BeginContext(11219, 27, true);
            WriteLiteral("                    </ul>\r\n");
            EndContext();
            BeginContext(11265, 52, true);
            WriteLiteral("            </div>\r\n        </div>\r\n    </li>\r\n</ul>");
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
