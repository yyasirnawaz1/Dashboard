#pragma checksum "F:\LTC Git\Dashboard\LTCOfficePortal\Views\Home\About.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7e7bd28c41f854af101a00dd14b2b82196e9eca5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_About), @"mvc.1.0.view", @"/Views/Home/About.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/About.cshtml", typeof(AspNetCore.Views_Home_About))]
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
#line 1 "F:\LTC Git\Dashboard\LTCOfficePortal\Views\_ViewImports.cshtml"
using LTCOfficePortal;

#line default
#line hidden
#line 2 "F:\LTC Git\Dashboard\LTCOfficePortal\Views\_ViewImports.cshtml"
using LTCOfficePortal.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7e7bd28c41f854af101a00dd14b2b82196e9eca5", @"/Views/Home/About.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3fa387cf9a6468341429476b2066ddf7cb96e685", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_About : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "F:\LTC Git\Dashboard\LTCOfficePortal\Views\Home\About.cshtml"
  
    ViewBag.Title = "About";

#line default
#line hidden
            BeginContext(37, 4, true);
            WriteLiteral("<h2>");
            EndContext();
            BeginContext(42, 13, false);
#line 4 "F:\LTC Git\Dashboard\LTCOfficePortal\Views\Home\About.cshtml"
Write(ViewBag.Title);

#line default
#line hidden
            EndContext();
            BeginContext(55, 12, true);
            WriteLiteral(".</h2>\r\n<h3>");
            EndContext();
            BeginContext(68, 15, false);
#line 5 "F:\LTC Git\Dashboard\LTCOfficePortal\Views\Home\About.cshtml"
Write(ViewBag.Message);

#line default
#line hidden
            EndContext();
            BeginContext(83, 64, true);
            WriteLiteral("</h3>\r\n\r\n<p>Use this area to provide additional information.</p>");
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