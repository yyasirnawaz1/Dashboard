#pragma checksum "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Form\_PrivateFormPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d83b4cecbbbaff54208fbe78c68f3347c31bd0d2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Form__PrivateFormPartial), @"mvc.1.0.view", @"/Views/Form/_PrivateFormPartial.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Form/_PrivateFormPartial.cshtml", typeof(AspNetCore.Views_Form__PrivateFormPartial))]
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
#line 1 "D:\Office\Github\Dashboard\LTC_Dashboard\Views\Form\_PrivateFormPartial.cshtml"
using Microsoft.Extensions.Configuration;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d83b4cecbbbaff54208fbe78c68f3347c31bd0d2", @"/Views/Form/_PrivateFormPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67cd85531b82a25b1d9f9c9f760a4e079ce716a1", @"/Views/_ViewImports.cshtml")]
    public class Views_Form__PrivateFormPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(81, 5798, true);
            WriteLiteral(@"

<div id=""divFormList"" class=""row card"">
    <div class=""col-md-12"">
        <div class=""block block-condensed noWhite"">
            <div class=""block-content"">
                <div class=""row row-margin"">
                    <div class=""col-lg-12 col-md-12"">
                        <button id=""btnPrivateFormToogle"" title=""Preview"" class=""btn btn-primary pull-right isPreviewHidden"" onclick=""Layout.togglePreviewButton(this,'privateFormListToggleParent')"">
                            <span class=""fa fa-eye""></span>
                        </button>
                        <button id=""btnPrivateFormCreateNew"" type=""button"" class=""btn btn-success margin-right-5 pull-right titleButtonsDefault"" onclick=""FormControls.createNewForm();"">Create New Form</button>
                        <button id=""btnPrivateFormShowTemplates"" type=""button"" class=""btn btn-success margin-right-5 pull-right titleButtonsDefault"" data-toggle=""modal"" data-target=""#publicFormModal"" onclick=""FormControls.showPublicTemplates();"">Sh");
            WriteLiteral(@"ow Templates</button>
                    </div>
                </div>
                <div id=""privateFormListToggleParent"" class=""row toggleDivParent toggleDivHidden"">
                    <div id=""mainDivPrivateForm"" class=""panel-left col-lg-12 col-md-12 col-sm-12 col-xs-12"">
                        <table id=""tblPrivateFormForms"" class=""table datatable-basic table-striped datatable-extended"">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Description</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                    <div class=""splitter"">
                    </div>
                    <div class=""panel-right toogleDivMain"">
                        <div class=""toogleDivTitle"">
                            <h2>
                       ");
            WriteLiteral(@"         preview
                            </h2>
                        </div>
                        <div class=""toogleDivContent"">
                            <iframe id=""iframePreviewPrivateForm""></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<div class=""row card"" id=""divFormDesigner"" style=""display:none;"">
    <div class=""col-lg-12 col-md-12"">
        <div class=""row row-margin"">
            <div class=""col-md-9"">
                <input type=""hidden"" name=""hdsid"" id=""hdsid"" value=""-1"" />
                <input type=""text"" name=""txtformname"" id=""txtformname"" class=""form-control pull-left"" placeholder=""Form Name"" value="""" />
            </div>
            <div class=""col-md-3"">
                <button type=""button"" class=""btn btn-primary margin-right-5 pull-left"" onclick=""FormControls.showFormList();"">Cancel</button>
                <button id=""btnSaveFormBottom"" type=""button"" class=""bt");
            WriteLiteral(@"n btn-success margin-right-5 pull-left "" onclick=""FormControls.saveFormDesign();"">Save</button>
            </div>
        </div>
        <div class=""row toggleDivParent toggleDivHidden"">
            <div class=""col-lg-12 col-md-12 col-sm-12 col-xs-12"">
                <div id=""fb-editor""></div>
            </div>
        </div>
    </div>
</div>

<div class=""modal fade"" id=""publicFormModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""modal-default-header"" data-backdrop=""static"" data-keyboard=""false"">
    <div class=""modal-dialog modal-lg"" role=""document"">
        <div class=""modal-content block minModelSize"">
            <div class=""modal-header"">
                <h4 class=""modal-title"" id=""modal-default-header"">Rendering View</h4>
                <div class=""pull-right"">
                    <button title=""Preview"" class=""btn btn-primary pull-right isPreviewHidden"" onclick=""Layout.togglePreviewButton(this,'privateSurveySelectionPublicToggleParent')"">
                        <span class=""fa");
            WriteLiteral(@" fa-eye""></span>
                    </button>

                    <button type=""button"" class=""btn btn-success margin-right-5 pull-right titleButtons"" onclick=""FormControls.duplicatePublicForm();"">Duplicate Form</button>
                </div>
                <button type=""button"" class=""close"" data-dismiss=""modal"">&times;</button>
            </div>
            <div class=""modal-body"">
                <div id=""privateSurveySelectionPublicToggleParent"" class=""row toggleDivParent toggleDivHidden"">
                    <div class=""panel-left col-lg-12 col-md-12 col-sm-12 col-xs-12"">
                        <table id=""tblPublicFormForms"" class=""table datatable-basic table-striped datatable-extended"">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Name</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
");
            WriteLiteral(@"                        </table>
                    </div>
                    <div class=""splitter"">
                    </div>
                    <div class=""panel-right toogleDivMain"">
                        <div class=""toogleDivTitle"">
                            <h2>
                                preview
                            </h2>
                        </div>
                        <div class=""toogleDivContent"">
                            <iframe id=""iframePreviewPrivateFormPublicList""></iframe>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</div>

");
            EndContext();
            DefineSection("scripts", async() => {
                BeginContext(5896, 214, true);
                WriteLiteral("\r\n    <link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/normalize/5.0.0/normalize.min.css\">\r\n    <script src=\"https://cdnjs.cloudflare.com/ajax/libs/prefixfree/1.0.7/prefixfree.min.js\"></script>\r\n");
                EndContext();
            }
            );
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
