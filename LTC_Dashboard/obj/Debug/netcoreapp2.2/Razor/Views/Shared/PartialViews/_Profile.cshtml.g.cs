#pragma checksum "F:\Latest LTC GIT\Dashboard\LTC_Dashboard\Views\Shared\PartialViews\_Profile.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f986f1deb9a723b5b71bface7a436f2da3528153"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_PartialViews__Profile), @"mvc.1.0.view", @"/Views/Shared/PartialViews/_Profile.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/PartialViews/_Profile.cshtml", typeof(AspNetCore.Views_Shared_PartialViews__Profile))]
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
#line 1 "F:\Latest LTC GIT\Dashboard\LTC_Dashboard\Views\_ViewImports.cshtml"
using LTCDashboard;

#line default
#line hidden
#line 2 "F:\Latest LTC GIT\Dashboard\LTC_Dashboard\Views\_ViewImports.cshtml"
using LTCDashboard.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f986f1deb9a723b5b71bface7a436f2da3528153", @"/Views/Shared/PartialViews/_Profile.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67cd85531b82a25b1d9f9c9f760a4e079ce716a1", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_PartialViews__Profile : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "F:\Latest LTC GIT\Dashboard\LTC_Dashboard\Views\Shared\PartialViews\_Profile.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(29, 5214, true);
            WriteLiteral(@"
<div id=""userprofileModal"" class=""modal fade"" tabindex=""-1"" data-backdrop=""static"" data-keyboard=""false"">
    <div class=""modal-dialog"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"">User Profile</h5>

                <button onclick=""Layout.updateUserProfile();"" name=""btnUpdateProfile"" id=""btnUpdateProfile"" class=""btn btn-success margin-right-5 pull-right titleButtonsDefault "">Save</button>

                <button type=""button"" class=""close"" data-dismiss=""modal"">&times;</button>
            </div>
            <div class=""modal-body"">
                <input id=""txtDoctorId"" type=""hidden"" />
                <div class=""container-fluid"">
                    <div class=""row"">
                        <div class=""col-lg-6 col-md-6"">
                            <label class=""control-label mb-0"">Title</label>
                            <input type=""text"" name=""txtSalutation"" id=""txtSalutation"" value="""" class=""form-control"" />
 ");
            WriteLiteral(@"                       </div>
                        <div class=""col-lg-6 col-md-6"">
                            <label class=""control-label mb-0"">Initials</label>
                            <input type=""text"" name=""txtInitials"" id=""txtInitials"" value="""" class=""form-control"" />
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""col-lg-6 col-md-6"">
                            <label class=""control-label mt-2 mb-0"">First Name</label>
                            <input type=""text"" name=""txtFirstName"" id=""txtFirstName"" value="""" class=""form-control"" />
                        </div>
                        <div class=""col-lg-6 col-md-6"">
                            <label class=""control-label mt-2 mb-0"">Last Name</label>
                            <input type=""text"" name=""txtLastName"" id=""txtLastName"" value="""" class=""form-control"" />
                        </div>

                    </div>
                    <di");
            WriteLiteral(@"v class=""row"">
                        <div class=""col-lg-6 col-md-6"">
                            <label class=""control-label mt-2 mb-0"">Phone</label>
                            <input type=""text"" name=""txtPhone"" id=""txtPhone"" value="""" class=""form-control"" />

                        </div>
                        <div class=""col-lg-6 col-md-6"">
                            <label class=""control-label mt-2 mb-0"">Cell</label>
                            <input type=""text"" name=""txtFax"" id=""txtFax"" value="""" class=""form-control"" />

                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""col-lg-12 col-md-12"">
                            <label class=""control-label mt-2 mb-0"">Address Line 1 </label>
                            <input type=""text"" name=""txtAddressLine1"" id=""txtAddressLine1"" value="""" class=""form-control"" />
                        </div>

                    </div>
                    <div class=""row"">
 ");
            WriteLiteral(@"                       <div class=""col-lg-12 col-md-12"">
                            <label class=""control-label mt-2 mb-0"">Address Line 2 </label>
                            <input type=""text"" name=""txtAddressLine2"" id=""txtAddressLine2"" value="""" class=""form-control"" />
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""col-lg-12 col-md-12"">
                            <label class=""control-label mt-2 mb-0"">Address Line 3 </label>
                            <input type=""text"" name=""txtAddressLine3"" id=""txtAddressLine3"" value="""" class=""form-control"" />
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""col-lg-6 col-md-6"">
                            <label class=""control-label mt-2 mb-0"">City</label>
                            <input type=""text"" name=""txtCity"" id=""txtCity"" value="""" class=""form-control"" />

                        </");
            WriteLiteral(@"div>
                        <div class=""col-lg-6 col-md-6"">
                            <label class=""control-label mt-2 mb-0"">Province</label>
                            <input type=""text"" name=""txtProvince"" id=""txtProvince"" value="""" class=""form-control"" />

                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""col-lg-6 col-md-6"">
                            <label class=""control-label mt-2 mb-0"">Country</label>
                            <input type=""text"" name=""txtCountry"" id=""txtCountry"" value="""" class=""form-control"" />

                        </div>
                        <div class=""col-lg-6 col-md-6"">
                            <label class=""control-label"">Postal Code</label>
                            <input type=""text"" name=""txtPostalCode"" id=""txtPostalCode"" value="""" class=""form-control"" />

                        </div>
                    </div>
                </div>

            </div>
    ");
            WriteLiteral("        <div class=\"modal-footer\">\r\n\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
