#pragma checksum "F:\LTCDashboard\LTCDashboard\LTCDashboard\Views\NewsLetter\_PreDefinedNewsLetterPartialLogin.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fec7907bb263c08cee4e196c857aef428df24780"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_NewsLetter__PreDefinedNewsLetterPartialLogin), @"mvc.1.0.view", @"/Views/NewsLetter/_PreDefinedNewsLetterPartialLogin.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/NewsLetter/_PreDefinedNewsLetterPartialLogin.cshtml", typeof(AspNetCore.Views_NewsLetter__PreDefinedNewsLetterPartialLogin))]
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
#line 1 "F:\LTCDashboard\LTCDashboard\LTCDashboard\Views\NewsLetter\_PreDefinedNewsLetterPartialLogin.cshtml"
using Microsoft.Extensions.Configuration;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fec7907bb263c08cee4e196c857aef428df24780", @"/Views/NewsLetter/_PreDefinedNewsLetterPartialLogin.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67cd85531b82a25b1d9f9c9f760a4e079ce716a1", @"/Views/_ViewImports.cshtml")]
    public class Views_NewsLetter__PreDefinedNewsLetterPartialLogin : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(81, 24, true);
            WriteLiteral("\r\n<link rel=\"stylesheet\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 105, "\"", 250, 2);
            WriteAttributeValue("", 112, "https://cdnjs.cloudflare.com/ajax/libs/normalize/5.0.0/normalize.min.css?v=", 112, 75, true);
#line 4 "F:\LTCDashboard\LTCDashboard\LTCDashboard\Views\NewsLetter\_PreDefinedNewsLetterPartialLogin.cshtml"
WriteAttributeValue("", 187, Configuration.GetSection("Configuration")["staticFileVersion"], 187, 63, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(251, 12, true);
            WriteLiteral(">\r\n\r\n<script");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 263, "\"", 408, 2);
            WriteAttributeValue("", 269, "https://cdnjs.cloudflare.com/ajax/libs/prefixfree/1.0.7/prefixfree.min.js?v=", 269, 76, true);
#line 6 "F:\LTCDashboard\LTCDashboard\LTCDashboard\Views\NewsLetter\_PreDefinedNewsLetterPartialLogin.cshtml"
WriteAttributeValue("", 345, Configuration.GetSection("Configuration")["staticFileVersion"], 345, 63, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(409, 1142, true);
            WriteLiteral(@"></script>
<div class=""modal fade"" id=""PreDefinedNewsLetter"" tabindex=""-1"" role=""dialog"" aria-labelledby=""modal-default-header"" data-backdrop=""static"" data-keyboard=""false"">
    <div class=""modal-dialog modal-full"" role=""document"">
        <div class=""modal-content block"">
            <div class=""modal-header"">
                <h4 class=""modal-title"">Public Newsletters</h4>

                <button id=""btnPublicNewsLetterToggle"" title=""Preview"" class=""btn btn-primary pull-right isPreviewHidden divNewsLetterList"" onclick=""Layout.togglePreviewButton(this,'preDefinedNewsletterList')"">
                    <span class=""fa fa-eye""></span>
                </button>
                <button id=""publicNewsLetterCreateNew"" type=""button"" class=""btn btn-success margin-right-5 pull-right titleButtonsDefault divNewsLetterList"" onclick=""PreNewsLetterControls.showCreateNewModal();"">Create New Template</button>
                <select class=""bs-select form-control pull-right newsletter-industry-ddl divNewsLetterList");
            WriteLiteral("\" id=\"ddlNewsLetterIndustries\" onchange=\"PreNewsLetterControls.searchPredifinedNLOnIndustry();\">\r\n                    ");
            EndContext();
            BeginContext(1551, 41, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fec7907bb263c08cee4e196c857aef428df247806310", async() => {
                BeginContext(1569, 14, true);
                WriteLiteral("All Industries");
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
            BeginContext(1592, 6919, true);
            WriteLiteral(@"
                </select>


                <button type=""button"" class=""btn red margin-right-5 btn-success pull-right templateEditordiv"" onclick=""Newsletter.presaveNewsletter();"">Save</button>

                <button id=""btnTemplateReset"" type=""button"" class=""btn margin-right-5 dark btn-danger pull-right templateEditordiv"" onclick=""Newsletter.resetNewsletterEditor()"">Reset</button>
                <button type=""button"" class=""btn btn-primary margin-right-5 pull-right templateEditordiv"" onclick=""Newsletter.cancelNewsletterEditor()"">Cancel</button>

                <button type=""button"" class=""close"" data-dismiss=""modal"">&times;</button>
            </div>

            <div class=""block-divider""></div>

            <div class=""modal-body"">
                <div class=""row divNewsLetterList"">
                    <div class=""col-md-12"">
                        <div class=""block-content"">
                            <div id=""preDefinedNewsletterList"" class=""row toggleDivParent toggleDivHidden"">");
            WriteLiteral(@"
                                <div class=""panel-left col-lg-12 col-md-12 col-sm-12 col-xs-12"">
                                    <table id=""tblPrivateNewsLetterForms"" class=""table datatable-basic table-striped datatable-extended"">
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
            WriteLiteral(@"                      preview
                                        </h2>
                                    </div>
                                    <div class=""toogleDivContent"">
                                        <iframe id=""previewPreNewsLetter""></iframe>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div style=""display:none;"" class=""row templateEditordiv"">
                    <div class=""col-md-12"">
                        <div class=""block block-condensed noWhite"">
                            <div class=""block-content"">
                                <div class=""row"">
                                    <div class=""col-md-12 col-sm-12"">
                                        <div class=""createNewsletterDiv margin-bottom-5"">
                                            Base Template
                         ");
            WriteLiteral(@"                   <select id=""ddlShellTemplates"" class=""form-control input-sm  pull-left"" onchange=""Newsletter.ddlShellTemplate_OnChange()""></select>
                                        </div>
                                        <div class=""createNewsletterDiv margin-bottom-5"">
                                            Template Type
                                            <select id=""ddlTemplatesTypes"" class=""form-control input-sm"" onchange=""Newsletter.ddlTemplatesTypes_OnChange()""></select>
                                        </div>
                                        <div class=""createNewsletterDiv margin-bottom-5"">
                                            Industry
                                            <select id=""ddlIndustries"" class=""form-control input-sm"" onchange=""Newsletter.ddlIndustries_OnChange()""></select>
                                        </div>
                                        <div class=""createNewsletterDiv margin-bottom-5"">
                ");
            WriteLiteral(@"                            Sub-Industry
                                            <select id=""ddlSubIndustries"" class=""form-control input-sm""></select>
                                        </div>
                                    </div>
                                </div>

                                <div id=""preDefinedNewsletterToggleCreate"" class="""">

                                    <ul class=""nav nav-tabs"">
                                        <li class=""active""><a data-toggle=""tab"" href=""#designer"">Designer</a></li>
                                        <li><a data-toggle=""tab"" href=""#preview"">Preview</a></li>
                                    </ul>

                                    <div class=""tab-content"">
                                        <div id=""designer"" class=""tab-pane fade in active"">
                                            <div class=""col-lg-12 col-md-12 col-sm-12 col-xs-12"">
                                                <div class=""row tog");
            WriteLiteral(@"gleDivParent"">
                                                    <div class=""panel-left droppableLeftMinWidth col-lg-9"">
                                                        <div id=""editorWrapper"">
                                                            <textarea id=""templateEditor"" cols=""30"" rows=""40""></textarea>
                                                        </div>
                                                    </div>

                                                    <div class=""splitter""></div>

                                                    <div class=""panel-right droppableRightMinWidth droppableUl"">

                                                        <ul class=""nav nav-tabs"">
                                                            <li class=""active""><a data-toggle=""tab"" href=""#home"">News Letter</a></li>
                                                            <li><a data-toggle=""tab"" href=""#menu1"">Email</a></li>
                                   ");
            WriteLiteral(@"                     </ul>

                                                        <div class=""tab-content"">
                                                            <div id=""home"" class=""tab-pane fade in active"">
                                                                <div id=""NewsletterTreeview"" class=""newslettertreeview""></div>
                                                            </div>
                                                            <div id=""menu1"" class=""tab-pane fade"">
                                                                <div id=""EmailTreeview"" class=""newslettertreeview""></div>
                                                            </div>
                                                        </div>

");
            EndContext();
            BeginContext(8754, 907, true);
            WriteLiteral(@"                                                        <div id=""OfficeImageTreeview"" class=""newslettertreeview""></div>
                                                        <div class=""newslettertreeview"" style=""display:none;"">
                                                            <a id=""ImageUploadButton"" style=""width:100%;"">Image Management</a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id=""preview"" class=""tab-pane fade"">

                                            <iframe id=""livePreview""></iframe>

                                        </div>
                                    </div>




");
            EndContext();
            BeginContext(10298, 1673, true);
            WriteLiteral(@"                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


<div class=""modal fade"" id=""newsletterSaveModel"" tabindex=""-1"" role=""dialog"" aria-labelledby=""modal-default-header"" data-backdrop=""static"" data-keyboard=""false"">
    <div class=""modal-dialog"" role=""document"">
        <div class=""modal-content block"">
            <div class=""modal-header"">
                <h4 class=""modal-title"" id=""renderSurveyModal-header"">Save Newsletter</h4>
                <button id=""btnSave"" type=""button"" class=""btn red margin-right-5 btn-success pull-right"" style=""display:none;"" onclick=""Newsletter.saveNewsletterEditor(true)"">Save</button>
                <button id=""btnModify"" type=""button"" class=""btn red margin-right-5 btn-success pull-right"" style=""display:none;"" onclick=""Newsletter.saveNewsletterEditor(false)"">Save</button>

                <button type=""but");
            WriteLiteral(@"ton"" class=""close"" data-dismiss=""modal"">&times;</button>
            </div>
            <div class=""block-divider""></div>
            <div class=""modal-body"">
                <div class=""row"">
                    <div class=""col-md-2"">
                        Title
                    </div>
                    <div class=""col-md-10"">
                        <div class=""margin-bottom-5"">
                            <input id=""txtTemplateTitle"" class=""form-control input-sm"" placeholder=""Tile"" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>");
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
