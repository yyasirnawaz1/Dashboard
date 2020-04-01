#pragma checksum "F:\LTCDashboard\LTCDashboard\LTCDashboard\Views\Form\_FormTagPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f7b599b0cfda34838d9fda12379d917b14693607"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Form__FormTagPartial), @"mvc.1.0.view", @"/Views/Form/_FormTagPartial.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Form/_FormTagPartial.cshtml", typeof(AspNetCore.Views_Form__FormTagPartial))]
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
#line 1 "F:\LTCDashboard\LTCDashboard\LTCDashboard\Views\_ViewImports.cshtml"
using LTCDashboard;

#line default
#line hidden
#line 2 "F:\LTCDashboard\LTCDashboard\LTCDashboard\Views\_ViewImports.cshtml"
using LTCDashboard.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f7b599b0cfda34838d9fda12379d917b14693607", @"/Views/Form/_FormTagPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67cd85531b82a25b1d9f9c9f760a4e079ce716a1", @"/Views/_ViewImports.cshtml")]
    public class Views_Form__FormTagPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "-1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 3954, true);
            WriteLiteral(@"<div class=""row card"">
    <div class=""col-md-12"">
        <div class=""block block-condensed noWhite"">

            <div class=""block-content"">
                <div class=""row row-margin"">
                    <div class=""col-md-12"">
                        <button type=""button"" class=""btn btn-success margin-right-5 pull-right titleButtonsDefault"" onclick=""PrivateTag.createNewPrivateTag();"" @*data-toggle=""modal"" data-target=""#PrivateTagModal""*@>Create New Tag</button>
                    </div>
                </div>
                <div>
                    <div class=""pull-left col-md-12"">
                        <div>
                            <table id=""tblTag"" class=""table datatable-basic table-striped datatable-extended"">
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Description</th>
                                        <th>Category</th>
              ");
            WriteLiteral(@"                          <th>Action</th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<div class=""modal fade"" id=""PublicDuplicationModel"" tabindex=""-1"" role=""dialog"" aria-labelledby=""modal-default-header"" data-backdrop=""static"" data-keyboard=""false"">
    <div class=""modal-dialog modal-lg"" role=""document"">
        <div class=""modal-content block"">
            <div class=""modal-header"">
                <h4 class=""modal-title"">Public Tags</h4>
                <button type=""button"" class=""close"" data-dismiss=""modal"">&times;</button>
            </div>
            <div class=""block-divider""></div>
            <div class=""modal-body"">
                <table id=""tblPublicDuplicateTag"" class=""table datatable-basic table-striped datatable-extended"">
                    <thead>
            ");
            WriteLiteral(@"            <tr>
                            <th>ID</th>
                            <th>Description</th>
                            <th>Category</th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div class=""modal-footer"">
                <button type=""button"" class=""btn btn-danger margin-right-5 pull-right  "" data-dismiss=""modal"">Close</button>
                <button type=""button"" class=""btn btn-success margin-right-5 pull-right "" data-dismiss=""modal"" data-toggle=""modal"" data-target=""#renderPrivateTagModal"" onclick=""PrivateTag.copyPrivateTag()"">Copy</button>

            </div>
        </div>
    </div>
</div>

<div class=""modal fade"" id=""PrivateTagModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""modal-default-header"" data-backdrop=""static"" data-keyboard=""false"">
    <div class=""modal-dialog  modal-lg"" role=""document"">
        <div class=""modal-content block"">
            <div class=""modal-header"">
               ");
            WriteLiteral(@" <h4 class=""modal-title"" id=""renderPrivateTagModal-header"">Private Tag</h4>
                <button type=""button"" class=""btn btn-success margin-right-5 pull-right "" onclick=""PrivateTag.savePrivateTag()"">Save</button>
                <button type=""button"" class=""close"" data-dismiss=""modal"">&times;</button>
            </div>
            <div class=""block-divider""></div>
            <div class=""modal-body"">
                <div class=""col-md-12"">
                    <input type=""hidden"" id=""hdnPrivateTagId"" value="""" />
                    <div class=""form-group"">
                        <label class=""col-md-2 control-label"">Category</label>
                        <div class=""col-md-10"">
                            <select id=""ddlPrivateTagCategories"" class=""form-control"" onchange=""PrivateTag.onChangeddlPrivateTagCategories()"">
                                ");
            EndContext();
            BeginContext(3954, 49, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f7b599b0cfda34838d9fda12379d917b146936077695", async() => {
                BeginContext(3973, 21, true);
                WriteLiteral("---select category---");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4003, 507, true);
            WriteLiteral(@"
                            </select>
                        </div>
                    </div>
                    <div class=""form-group"">
                        <label class=""col-md-2 control-label"">Description</label>
                        <div class=""col-md-10"">
                            <input id=""txtPrivateTagDescription"" type=""text"" class=""form-control"">
                        </div>
                    </div>
                </div>
                <br />
            </div>
");
            EndContext();
            BeginContext(4582, 34, true);
            WriteLiteral("        </div>\r\n    </div>\r\n</div>");
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
