#pragma checksum "D:\LTC Git\Dashboard\LTCOfficePortal\Views\Shared\PartialViews\_ChangePassword.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e4e10b9ecaa9736e85b2c1acd09380115d11941b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_PartialViews__ChangePassword), @"mvc.1.0.view", @"/Views/Shared/PartialViews/_ChangePassword.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/PartialViews/_ChangePassword.cshtml", typeof(AspNetCore.Views_Shared_PartialViews__ChangePassword))]
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
#line 1 "D:\LTC Git\Dashboard\LTCOfficePortal\Views\_ViewImports.cshtml"
using LTCOfficePortal;

#line default
#line hidden
#line 2 "D:\LTC Git\Dashboard\LTCOfficePortal\Views\_ViewImports.cshtml"
using LTCOfficePortal.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e4e10b9ecaa9736e85b2c1acd09380115d11941b", @"/Views/Shared/PartialViews/_ChangePassword.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3fa387cf9a6468341429476b2066ddf7cb96e685", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_PartialViews__ChangePassword : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "D:\LTC Git\Dashboard\LTCOfficePortal\Views\Shared\PartialViews\_ChangePassword.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(29, 947, true);
            WriteLiteral(@"
<div id=""changePasswordModal"" class=""modal fade"" tabindex=""-1"" data-backdrop=""static"" data-keyboard=""false"">
    <div class=""modal-dialog"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"">Change Password</h5>

                <button onclick=""Layout.updatePassword();"" name=""btnUpdatePassword"" id=""btnUpdatePassword"" class=""btn btn-success margin-right-5 pull-right titleButtonsDefault "">Save</button>

                <button type=""button"" class=""close"" data-dismiss=""modal"">&times;</button>
            </div>
            <div class=""modal-body"">
                <div class=""container-fluid"">
                    <div class=""row"">
                        <div class=""col-lg-6 col-md-6"">
                            <label class=""control-label"">User Name / Email</label>
                            <input type=""email"" name=""txtCurrentEmail"" id=""txtCurrentEmail""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 976, "\"", 1003, 1);
#line 21 "D:\LTC Git\Dashboard\LTCOfficePortal\Views\Shared\PartialViews\_ChangePassword.cshtml"
WriteAttributeValue("", 984, User.Identity.Name, 984, 19, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1004, 1228, true);
            WriteLiteral(@" class=""form-control disabled"" readonly />
                        </div>
                        <div class=""col-lg-6 col-md-6"">
                            <label class=""control-label"">Current Password</label>
                            <input type=""password"" name=""txtCurrentPassword"" id=""txtCurrentPassword"" value="""" class=""form-control"" />
                        </div>
                    </div>
                    <div class=""row"">

                        <div class=""col-lg-6 col-md-6"">
                            <label class=""control-label"">New Password</label>
                            <input type=""password"" name=""txtNewPassword"" id=""txtNewPassword"" value="""" class=""form-control"" />
                        </div>
                        <div class=""col-lg-6 col-md-6"">
                            <label class=""control-label"">Confirm Password</label>
                            <input type=""password"" name=""txtConfirmPassword"" id=""txtConfirmPassword"" value="""" class=""form-control"" />
  ");
            WriteLiteral("                      </div>\r\n                    </div>\r\n\r\n                </div>\r\n\r\n            </div>\r\n            <div class=\"modal-footer\">\r\n\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
