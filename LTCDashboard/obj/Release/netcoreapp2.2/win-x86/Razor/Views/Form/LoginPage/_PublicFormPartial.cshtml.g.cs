#pragma checksum "C:\Users\Yasir\source\repos\LTCDashboard\LTCDashboard\Views\Form\LoginPage\_PublicFormPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c3d8840687982e066038616bbde0456a06eec95e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Form_LoginPage__PublicFormPartial), @"mvc.1.0.view", @"/Views/Form/LoginPage/_PublicFormPartial.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Form/LoginPage/_PublicFormPartial.cshtml", typeof(AspNetCore.Views_Form_LoginPage__PublicFormPartial))]
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
#line 1 "C:\Users\Yasir\source\repos\LTCDashboard\LTCDashboard\Views\_ViewImports.cshtml"
using LTCDashboard;

#line default
#line hidden
#line 2 "C:\Users\Yasir\source\repos\LTCDashboard\LTCDashboard\Views\_ViewImports.cshtml"
using LTCDashboard.Models;

#line default
#line hidden
#line 1 "C:\Users\Yasir\source\repos\LTCDashboard\LTCDashboard\Views\Form\LoginPage\_PublicFormPartial.cshtml"
using Microsoft.Extensions.Configuration;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c3d8840687982e066038616bbde0456a06eec95e", @"/Views/Form/LoginPage/_PublicFormPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67cd85531b82a25b1d9f9c9f760a4e079ce716a1", @"/Views/_ViewImports.cshtml")]
    public class Views_Form_LoginPage__PublicFormPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(81, 24, true);
            WriteLiteral("\r\n<link rel=\"stylesheet\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 105, "\"", 250, 2);
            WriteAttributeValue("", 112, "https://cdnjs.cloudflare.com/ajax/libs/normalize/5.0.0/normalize.min.css?v=", 112, 75, true);
#line 4 "C:\Users\Yasir\source\repos\LTCDashboard\LTCDashboard\Views\Form\LoginPage\_PublicFormPartial.cshtml"
WriteAttributeValue("", 187, Configuration.GetSection("Configuration")["staticFileVersion"], 187, 63, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(251, 12, true);
            WriteLiteral(">\r\n\r\n<script");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 263, "\"", 408, 2);
            WriteAttributeValue("", 269, "https://cdnjs.cloudflare.com/ajax/libs/prefixfree/1.0.7/prefixfree.min.js?v=", 269, 76, true);
#line 6 "C:\Users\Yasir\source\repos\LTCDashboard\LTCDashboard\Views\Form\LoginPage\_PublicFormPartial.cshtml"
WriteAttributeValue("", 345, Configuration.GetSection("Configuration")["staticFileVersion"], 345, 63, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(409, 4177, true);
            WriteLiteral(@"></script>


<div id=""PublicFormloginModal"" class=""modal fade"" tabindex=""-1"" data-backdrop=""static"" data-keyboard=""false"">
    <div class=""modal-dialog modal-full"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"">Public Form</h5>

                <button id=""btnPublicFormToggle"" title=""Preview"" class=""btn btn-primary pull-right isPreviewHidden divPublicFormList"" onclick=""Layout.togglePreviewButton(this,'publicFormListToggleParent')"">
                    <span class=""fa fa-eye""></span>
                </button>
                <button id=""publicFormCreateNew"" type=""button"" class=""btn btn-success margin-right-5 pull-right titleButtonsDefault divPublicFormList"" onclick=""PublicFormControls.createNewForm();"">Create New Form</button>
                <button type=""button"" class=""btn btn-success margin-right-5 pull-right divPublicFormDesigner"" onclick=""PublicFormControls.saveFormDesign();"">Save</button>
                <button type=""butt");
            WriteLiteral(@"on"" class=""btn btn-primary margin-right-5 pull-right divPublicFormDesigner"" onclick=""PublicFormControls.showFormList();"">Cancel</button>
                <input type=""text"" name=""txtformname"" id=""txtPublicformname"" class=""form-control pull-right textboxMinWidth divPublicFormDesigner"" placeholder=""Form Name"" value="""" />

                <button type=""button"" class=""close"" data-dismiss=""modal"" onclick=""PublicFormControls.showFormList();"">&times;</button>
            </div>
            <div class=""modal-body"">
                <div class=""row divPublicFormList"">
                    <div class=""col-md-12"">
                        <div class=""block-content"">
                            <div id=""publicFormListToggleParent"" class=""row toggleDivParent toggleDivHidden"">
                                <div class=""panel-left col-lg-12 col-md-12 col-sm-12 col-xs-12"">
                                    <div id=""divPublicFormMainTable"" class=""col-md-12"">
                                        <table id=""tblPub");
            WriteLiteral(@"licForm"" class=""table datatable-basic table-striped datatable-extended"" style=""width:100%"">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Description</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                </div>
                                <div class=""splitter"">
                                </div>
                                <div class=""panel-right toogleDivMain"">
                                    <div class=""toogleDivTitle"">
                                        <h2>
                                            preview
                                       ");
            WriteLiteral(@" </h2>
                                    </div>
                                    <div class=""toogleDivContent"">
                                        <iframe id=""iframePreviewPublicForm""></iframe>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class=""divPublicFormDesigner"" style=""display:none;"">
                    <input type=""hidden"" name=""hdPublicsid"" id=""hdsPublicid"" value=""-1"" />
                    <div class=""row toggleDivParent toggleDivHidden"">
                        <div class=""col-md-12"">
                            <div id=""fbPublicForm-editor""></div>
                        </div>
                    </div>

                </div>
                <div class=""row"">
                    <div class=""col-md-12"">

                    </div>
                </div>
            </div>
            <div ");
            WriteLiteral("class=\"modal-footer\">\r\n\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
