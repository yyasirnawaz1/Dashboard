#pragma checksum "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Layouts\_OfficeManagmentLayout.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fc515c2304a9c8407ee7820fa38c3394326ad340"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Layouts__OfficeManagmentLayout), @"mvc.1.0.view", @"/Views/Shared/Layouts/_OfficeManagmentLayout.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Layouts/_OfficeManagmentLayout.cshtml", typeof(AspNetCore.Views_Shared_Layouts__OfficeManagmentLayout))]
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
#line 1 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Layouts\_OfficeManagmentLayout.cshtml"
using Microsoft.Extensions.Configuration;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fc515c2304a9c8407ee7820fa38c3394326ad340", @"/Views/Shared/Layouts/_OfficeManagmentLayout.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67cd85531b82a25b1d9f9c9f760a4e079ce716a1", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Layouts__OfficeManagmentLayout : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/bundling/css/commonStyle.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Resources/Limitless/global_assets/js/demo_pages/dashboard.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/bundling/js/commonScripts1.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/bundling/js/commonScripts2.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Scripts/OfficeManagement/OfficeManagementLayout.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(81, 37, true);
            WriteLiteral("\r\n<!DOCTYPE html>\r\n<html lang=\"en\">\r\n");
            EndContext();
            BeginContext(118, 618, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fc515c2304a9c8407ee7820fa38c3394326ad3406104", async() => {
                BeginContext(124, 339, true);
                WriteLiteral(@"
    <meta charset=""utf-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1, shrink-to-fit=no"">
    <title>LTC Dental</title>
    <link href=""https://fonts.googleapis.com/css?family=Roboto:400,300,100,500,700,900"" rel=""stylesheet"" type=""text/css"">
    ");
                EndContext();
                BeginContext(463, 67, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "fc515c2304a9c8407ee7820fa38c3394326ad3406839", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(530, 8, true);
                WriteLiteral("\r\n\r\n    ");
                EndContext();
                BeginContext(538, 86, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fc515c2304a9c8407ee7820fa38c3394326ad3408175", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(624, 8, true);
                WriteLiteral("\r\n\r\n    ");
                EndContext();
                BeginContext(633, 40, false);
#line 16 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Layouts\_OfficeManagmentLayout.cshtml"
Write(RenderSection("styles", required: false));

#line default
#line hidden
                EndContext();
                BeginContext(673, 8, true);
                WriteLiteral("\r\n\r\n    ");
                EndContext();
                BeginContext(682, 45, false);
#line 18 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Layouts\_OfficeManagmentLayout.cshtml"
Write(RenderSection("headscripts", required: false));

#line default
#line hidden
                EndContext();
                BeginContext(727, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(736, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(738, 4260, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fc515c2304a9c8407ee7820fa38c3394326ad34011002", async() => {
                BeginContext(744, 992, true);
                WriteLiteral(@"
    <input type=""hidden"" id=""hdnIsOfficeManagement"" />
    <div class=""navbar navbar-expand-md navbar-dark"">
        <span class=""applicationName"">
            Logic Tech Corp
        </span>
        <div class=""d-md-none"">
            <button class=""navbar-toggler"" type=""button"" data-toggle=""collapse"" data-target=""#navbar-mobile"">
                <i class=""icon-tree5""></i>
            </button>
            <button class=""navbar-toggler sidebar-mobile-main-toggle"" type=""button"">
                <i class=""icon-paragraph-justify3""></i>
            </button>
        </div>
        <div class=""collapse navbar-collapse"" id=""navbar-mobile"">
            <ul class=""navbar-nav"">
                <li class=""nav-item"">
                    <a href=""#"" class=""navbar-nav-link sidebar-control sidebar-main-toggle d-none d-md-block"">
                        <i class=""icon-paragraph-justify3""></i>
                    </a>
                </li>
            </ul>
            ");
                EndContext();
                BeginContext(1737, 57, false);
#line 42 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Layouts\_OfficeManagmentLayout.cshtml"
       Write(Html.Partial("~/Views/Shared/Menu/_MenuSelection.cshtml"));

#line default
#line hidden
                EndContext();
                BeginContext(1794, 84, true);
                WriteLiteral("\r\n\r\n            <span class=\"badge ml-md-3 mr-md-auto\">&nbsp;</span>\r\n\r\n            ");
                EndContext();
                BeginContext(1879, 56, false);
#line 46 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Layouts\_OfficeManagmentLayout.cshtml"
       Write(Html.Partial("~/Views/Shared/Menu/_TopRightMenu.cshtml"));

#line default
#line hidden
                EndContext();
                BeginContext(1935, 1127, true);
                WriteLiteral(@"
        </div>
    </div>
    <!-- /main navbar -->
    <!-- Page content -->
    <div class=""page-content"">
        <!-- Main sidebar -->
        <div class=""sidebar sidebar-dark sidebar-main sidebar-expand-md"">
            <!-- Sidebar mobile toggler -->
            <div class=""sidebar-mobile-toggler text-center"">
                <a href=""#"" class=""sidebar-mobile-main-toggle"">
                    <i class=""icon-arrow-left8""></i>
                </a>
                Navigation
                <a href=""#"" class=""sidebar-mobile-expand"">
                    <i class=""icon-screen-full""></i>
                    <i class=""icon-screen-normal""></i>
                </a>
            </div>
            <!-- /sidebar mobile toggler -->
            <!-- Sidebar content -->
            <div class=""sidebar-content"">
                <!-- Main navigation -->
                <div class=""card card-sidebar-mobile"">
                    <ul class=""nav nav-sidebar"" data-nav-type=""accordion"">
             ");
                WriteLiteral("           <!-- Main -->\r\n                        <li class=\"nav-item\">\r\n                            <a");
                EndContext();
                BeginWriteAttribute("href", " href=\"", 3062, "\"", 3108, 1);
#line 73 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Layouts\_OfficeManagmentLayout.cshtml"
WriteAttributeValue("", 3069, Url.Action("Index","OfficeManagement"), 3069, 39, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(3109, 618, true);
                WriteLiteral(@" class=""nav-link active"">
                                <i class=""icon-home4""></i>
                                <span>
                                    Office Management
                                </span>
                            </a>
                        </li>
                        <!-- /page kits -->
                    </ul>
                </div>
                <!-- /main navigation -->
            </div>
            <!-- /sidebar content -->

        </div>
        <!-- /main sidebar -->
        <!-- Main content -->
        <div class=""content-wrapper"">
            ");
                EndContext();
                BeginContext(3728, 12, false);
#line 91 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Layouts\_OfficeManagmentLayout.cshtml"
       Write(RenderBody());

#line default
#line hidden
                EndContext();
                BeginContext(3740, 16, true);
                WriteLiteral("\r\n\r\n            ");
                EndContext();
                BeginContext(3757, 52, false);
#line 93 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Layouts\_OfficeManagmentLayout.cshtml"
       Write(Html.Partial("~/Views/Shared/Footer/_Footer.cshtml"));

#line default
#line hidden
                EndContext();
                BeginContext(3809, 719, true);
                WriteLiteral(@"
        </div>
        <!-- /main content -->
    </div>

    <div id=""thePreviewPanel"" class=""modal fade"" tabindex=""-1"" data-backdrop=""static"" data-keyboard=""false"">
        <div class=""modal-dialog"">
            <div class=""modal-content"">
                <div class=""modal-header"">
                    <button type=""button"" class=""close"" data-dismiss=""modal"">&times;</button>
                </div>
                <div class=""modal-body"">
                    <iframe id=""iframeThePreview"" allowfullscreen style=""min-height:400px; width:100%;""></iframe>
                </div>
                <div class=""modal-footer"">

                </div>
            </div>
        </div>
    </div>

    ");
                EndContext();
                BeginContext(4529, 57, false);
#line 114 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Layouts\_OfficeManagmentLayout.cshtml"
Write(Html.Partial("~/Views/Shared/PartialViews/_About.cshtml"));

#line default
#line hidden
                EndContext();
                BeginContext(4586, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(4593, 66, false);
#line 115 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Layouts\_OfficeManagmentLayout.cshtml"
Write(Html.Partial("~/Views/Shared/PartialViews/_ChangePassword.cshtml"));

#line default
#line hidden
                EndContext();
                BeginContext(4659, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(4666, 59, false);
#line 116 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Layouts\_OfficeManagmentLayout.cshtml"
Write(Html.Partial("~/Views/Shared/PartialViews/_Profile.cshtml"));

#line default
#line hidden
                EndContext();
                BeginContext(4725, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(4731, 59, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fc515c2304a9c8407ee7820fa38c3394326ad34018330", async() => {
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
                BeginContext(4790, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(4796, 59, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fc515c2304a9c8407ee7820fa38c3394326ad34019586", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(4855, 8, true);
                WriteLiteral("\r\n\r\n    ");
                EndContext();
                BeginContext(4864, 41, false);
#line 120 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Shared\Layouts\_OfficeManagmentLayout.cshtml"
Write(RenderSection("scripts", required: false));

#line default
#line hidden
                EndContext();
                BeginContext(4905, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(4911, 76, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fc515c2304a9c8407ee7820fa38c3394326ad34021232", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(4987, 4, true);
                WriteLiteral("\r\n\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4998, 11, true);
            WriteLiteral("\r\n</html>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IConfiguration Configuration { get; private set; }
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
