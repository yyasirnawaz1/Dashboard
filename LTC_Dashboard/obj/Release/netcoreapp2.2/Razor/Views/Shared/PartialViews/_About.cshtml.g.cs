#pragma checksum "D:\LTC Git\Dashboard\LTC_Dashboard\Views\Shared\PartialViews\_About.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ef58fd58dd893548d36adbde164a1dbc06673927"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_PartialViews__About), @"mvc.1.0.view", @"/Views/Shared/PartialViews/_About.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/PartialViews/_About.cshtml", typeof(AspNetCore.Views_Shared_PartialViews__About))]
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
#line 1 "D:\LTC Git\Dashboard\LTC_Dashboard\Views\_ViewImports.cshtml"
using LTCDashboard;

#line default
#line hidden
#line 2 "D:\LTC Git\Dashboard\LTC_Dashboard\Views\_ViewImports.cshtml"
using LTCDashboard.Models;

#line default
#line hidden
#line 1 "D:\LTC Git\Dashboard\LTC_Dashboard\Views\Shared\PartialViews\_About.cshtml"
using Microsoft.Extensions.Configuration;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef58fd58dd893548d36adbde164a1dbc06673927", @"/Views/Shared/PartialViews/_About.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67cd85531b82a25b1d9f9c9f760a4e079ce716a1", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_PartialViews__About : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(81, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 4 "D:\LTC Git\Dashboard\LTC_Dashboard\Views\Shared\PartialViews\_About.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(110, 752, true);
            WriteLiteral(@"
<div id=""aboutModal"" class=""modal fade"" tabindex=""-1"" data-backdrop=""static"" data-keyboard=""false"">
    <div class=""modal-dialog"">
        <div class=""modal-content"">
            <div class=""modal-header bg-primary"">
                <h5 class=""modal-title"">About</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"">&times;</button>
            </div>
            <div class=""modal-body"">
                <h5>Logic Tech Corp.</h5>
                <table>
                    <tr>
                        <td style=""width:130px;"">
                            <strong>
                                Compile Date:
                            </strong>
                        </td>
                        <td>");
            EndContext();
            BeginContext(863, 56, false);
#line 24 "D:\LTC Git\Dashboard\LTC_Dashboard\Views\Shared\PartialViews\_About.cshtml"
                       Write(Configuration.GetSection("Configuration")["compileDate"]);

#line default
#line hidden
            EndContext();
            BeginContext(919, 297, true);
            WriteLiteral(@"</td>
                    </tr>
                    <tr>
                        <td style=""width:130px;"">
                            <strong>
                                Compile Version:
                            </strong>
                        </td>
                        <td>");
            EndContext();
            BeginContext(1217, 59, false);
#line 32 "D:\LTC Git\Dashboard\LTC_Dashboard\Views\Shared\PartialViews\_About.cshtml"
                       Write(Configuration.GetSection("Configuration")["compileVersion"]);

#line default
#line hidden
            EndContext();
            BeginContext(1276, 176, true);
            WriteLiteral("</td>\r\n                    </tr>\r\n                </table>\r\n            </div>\r\n            <div class=\"modal-footer\">\r\n\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
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
