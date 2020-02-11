#pragma checksum "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Newsletter\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "23f5b2a832597b00ca14c582210c284326c3b555"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Newsletter_Index), @"mvc.1.0.view", @"/Views/Newsletter/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Newsletter/Index.cshtml", typeof(AspNetCore.Views_Newsletter_Index))]
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
#line 1 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\_ViewImports.cshtml"
using LTCDashboard;

#line default
#line hidden
#line 2 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\_ViewImports.cshtml"
using LTCDashboard.Models;

#line default
#line hidden
#line 1 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Newsletter\Index.cshtml"
using Microsoft.Extensions.Configuration;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"23f5b2a832597b00ca14c582210c284326c3b555", @"/Views/Newsletter/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67cd85531b82a25b1d9f9c9f760a4e079ce716a1", @"/Views/_ViewImports.cshtml")]
    public class Views_Newsletter_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Newsletter\Index.cshtml"
  
    ViewBag.Title = "Index";
    Layout = "~/Views/Shared/Layouts/_NewsletterLayout.cshtml";


#line default
#line hidden
            BeginContext(185, 503, true);
            WriteLiteral(@"

<style>
    #tblParadigm_filter, #tblParadigm_info, #tblMarketing_filter, #tblMarketing_info {
        display: none;
    }

    .dotted, .k-in {
        white-space: nowrap;
        width: 180px;
        overflow: hidden;
        text-overflow: ellipsis;
    }

    .contentSpace {
        display: flex;
        justify-content: space-between;
        align-items: center;
    }

        .contentSpace::before, .contentSpace::after {
            width: 130px;
        }

    ");
            EndContext();
            BeginContext(689, 209, true);
            WriteLiteral("@media (min-width: 768px) {\r\n        .seven-cols .col-md-1,\r\n        .seven-cols .col-sm-1,\r\n        .seven-cols .col-lg-1 {\r\n            width: 100%;\r\n            text-align: center;\r\n        }\r\n    }\r\n\r\n    ");
            EndContext();
            BeginContext(899, 239, true);
            WriteLiteral("@media (min-width: 576px) {\r\n        .seven-cols .col-md-1,\r\n        .seven-cols .col-sm-1,\r\n        .seven-cols .col-lg-1 {\r\n            -ms-flex: 0 0 30%;\r\n            flex: 0 0 30%;\r\n            max-width: 30%;\r\n        }\r\n    }\r\n\r\n    ");
            EndContext();
            BeginContext(1139, 272, true);
            WriteLiteral(@"@media (min-width: 992px) {
        .seven-cols .col-md-1,
        .seven-cols .col-sm-1,
        .seven-cols .col-lg-1 {
            -ms-flex: 0 0 30%;
            flex: 0 0 30%;
            max-width: 30%;
            text-align: center;
        }
    }

    ");
            EndContext();
            BeginContext(1412, 14118, true);
            WriteLiteral(@"@media (min-width: 1200px) {
        .seven-cols .col-md-1,
        .seven-cols .col-sm-1,
        .seven-cols .col-lg-1 {
            -ms-flex: 0 0 14.2857%;
            flex: 0 0 14.2857%;
            max-width: 14.2857%;
            text-align: center;
        }
    }

    .transparentBg {
        background-color: transparent;
    }

    .icon-checkmark-circle {
        margin-left: 1rem;
    }

    .truncate {
        white-space: nowrap;
        width: 185px;
        overflow: hidden;
        text-overflow: ellipsis;
        font-size: 12px;
    }

    .card-title {
        font-size: 15px !important;
    }

    .btn {
        font-size: 14px;
    }

    .dropdown-menu {
        font-size: 14px;
    }

    body {
        font-size: 13px !important;
    }
</style>


<div class=""content"">
    <div class=""row"">
        <div class=""card"" style=""width:100%"">
            <div class=""card-header bg-white header-elements-inline"">
                <h6 class=""card-t");
            WriteLiteral(@"itle""></h6>
                <div class=""header-elements"">
                    <div class=""list-icons"">
                        <input id=""txtSearch"" onkeyup=""Newsletter.Search();"" class=""form-control"" placeholder=""Search ..."" />
                        <button type=""button"" id=""btnSelectOptions"" class=""btn btn-primary"" onclick=""Newsletter.userDefinedOptionChanged('create');"">Create New Template</button>
                        
                    </div>
                </div>
            </div>
            <div class=""card-body"">
                <div class=""row"">
                    <div class=""col-md-12"">
                        <div id=""templateList"" class=""m-heading-1 border-green m-bordered"">

                            <ul class=""nav nav-tabs nav-tabs-top"">
                                <li class=""nav-item""><a href=""#userDefinedTemplatesTab"" onclick=""javascript:Newsletter.clearTabSelection('user');"" class=""nav-link active"" data-toggle=""tab"">System Templates</a></li>
                  ");
            WriteLiteral(@"              <li class=""nav-item""><a href=""#marketingTemplatesTab"" onclick=""javascript:Newsletter.clearTabSelection('marketing');"" class=""nav-link"" data-toggle=""tab"">Marketing Templates</a></li>
                                <li class=""nav-item""><a href=""#SystemTemplatesTab"" onclick=""javascript:Newsletter.clearTabSelection('system');"" class=""nav-link"" data-toggle=""tab"">Design</a></li>
                                <li class=""nav-item""><a href=""#SystemArticlesTab"" onclick=""javascript:Newsletter.clearTabSelection('article');"" class=""nav-link"" data-toggle=""tab"">Articles</a></li>

                            </ul>
                            <div class=""tab-content card"">
                                <div class=""tab-pane fade tiles fade show active"" id=""userDefinedTemplatesTab"">
                                    <div class=""col-lg-2 moveToTopRight"">

                                    </div>
                                    <div id=""userDefineTemplateList"">
                               ");
            WriteLiteral(@"         <div class=""card-body"" style=""margin-left: 50px;margin-right: 50px;"">
                                            <div class=""row"">
                                                <div class=""col-md-1"" style=""margin-top: 10px;"">
                                                    <label>Category: </label>
                                                </div>
                                                <div class=""col-md-4"">
                                                    <select id=""ddlTemplatesTypes1"" class=""form-control"" onchange=""Newsletter.ddlTemplatesTypes1_OnChange()""></select>
                                                </div>

                                            </div>

                                            <table class=""table datatable-basic"" id=""tblParadigm"">
                                                <thead>
                                                    <tr>
                                                        <th>Name</th>
          ");
            WriteLiteral(@"                                              <th>Category</th>
                                                        <th>Default</th>
                                                        <th>Modification Date</th>
                                                        <th class=""text-center"">Actions</th>
                                                    </tr>
                                                </thead>
                                                <tbody id=""tblBody""></tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class=""tab-pane tiles horizontalScrollOnExpand"" id=""SystemTemplatesTab"">
                                    <div class=""col-lg-2 moveToTopRight"">

                                    </div>
                                    <div id=""SystemTemplateList"">
                           ");
            WriteLiteral(@"             <div class=""card-body"" style=""margin-left: 50px;margin-right: 50px;"">
                                            <table class=""table datatable"" id=""tblSystem"">
                                                <thead>
                                                    <tr>
                                                        <th>Name</th>
                                                        <th style=""display: none"">Category</th>
                                                        <th>Modification Date</th>

                                                        <th class=""text-center"">Actions</th>
                                                    </tr>
                                                </thead>
                                                <tbody id=""tblBodySystem""></tbody>
                                            </table>
                                        </div>

                                    </div>
                                </di");
            WriteLiteral(@"v>
                                <div class=""tab-pane tiles horizontalScrollOnExpand"" id=""marketingTemplatesTab"">
                                    <div class=""col-lg-2 moveToTopRight"">

                                    </div>
                                    <div id=""marketingTemplateList"">
                                        <button type=""button"" id=""btnDeleteSelectOptions"" class=""btn btn-primary"" style=""float: right; margin-top: 15px; margin-right: 15px; margin-bottom: 15px; "" disabled onclick=""Newsletter.deleteSelectedNewsletters();"">Delete</button>
                                        <div class=""card-body"" style=""margin-left: 50px;margin-right: 50px;"">
                                            <table class=""table datatable-basic"" id=""tblMarketing"">
                                                <thead>
                                                    <tr>
                                                        <th></th>
                                                 ");
            WriteLiteral(@"       <th>Name</th>
                                                        <th>Modification Date</th>

                                                        <th class=""text-center"">Actions</th>
                                                    </tr>
                                                </thead>
                                                <tbody id=""tblBodyMarketing""></tbody>
                                            </table>
                                        </div>

                                        <!-- /basic datatable -->


                                    </div>
                                </div>
                                <div class=""tab-pane tiles horizontalScrollOnExpand"" id=""SystemArticlesTab"">
                                    <div class=""col-lg-2 moveToTopRight"">

                                    </div>
                                    <div id=""SystemArticlesTabList"">
                                        <div class=""card-b");
            WriteLiteral(@"ody"" style=""margin-left: 50px;margin-right: 50px;"">
                                            <table class=""table datatable-basic"" id=""tblArticle"">
                                                <thead>
                                                    <tr>
                                                        <th>Name</th>
                                                        <th>Modification Date</th>

                                                        <th class=""text-center"">Actions</th>
                                                    </tr>
                                                </thead>
                                                <tbody id=""tblBodyArticle""></tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                </");
            WriteLiteral(@"div>

            </div>
        </div>
    </div>

    <div id=""previewNewsletterModel"" class=""modal fade"" tabindex=""-1"" data-width=""600"" data-keyboard=""false"" role=""dialog"" aria-labelledby=""previewNewsletterModel"" aria-hidden=""true"" data-backdrop=""static"">

        <div class=""modal-dialog  modal-full"">
            <div class=""modal-content"">
                <div class=""modal-header btn-primary"" style=""padding-bottom: 10px"">
                    <h4 class=""modal-title"">Preview</h4>
                    <button type=""button"" class=""close"" data-dismiss=""modal"" aria-hidden=""true""></button>
                </div>

                <div class=""modal-body"" style=""height: calc(100vh - 200px);overflow: hidden;"">


                    <iframe id=""previewContent"" class=""hide""></iframe>



                </div>
                <div class=""modal-footer"">
                    <button type=""button"" data-dismiss=""modal"" class=""btn btn-primary"">Close</button>
                </div>

            </di");
            WriteLiteral(@"v>
        </div>
    </div>
    <div id=""previewArticleModel"" class=""modal fade"" tabindex=""-1"" data-width=""600"" data-keyboard=""false"" role=""dialog"" aria-labelledby=""previewArticleModel"" aria-hidden=""true"" data-backdrop=""static"">

        <div class=""modal-dialog  modal-full"">
            <div class=""modal-content"">
                <div class=""modal-header btn-primary"" style=""padding-bottom: 10px"">
                    <h4 class=""modal-title"">Preview</h4>
                    <button type=""button"" class=""close"" data-dismiss=""modal"" aria-hidden=""true""></button>
                </div>

                <div class=""modal-body"" style=""height: calc(100vh - 200px);overflow: hidden;"">


                    <iframe id=""previewArticleContent"" class=""hide""></iframe>



                </div>
                <div class=""modal-footer"">
                    <button type=""button"" data-dismiss=""modal"" class=""btn btn-primary"">Close</button>
                </div>

            </div>
        </div>
    <");
            WriteLiteral(@"/div>
    <div id=""useArticle"" class=""modal"" tabindex=""-1"" data-width=""400"" data-keyboard=""false"" role=""dialog"" aria-labelledby=""useArticle"" aria-hidden=""true"" data-backdrop=""static"">
        <div id=""useArticleContainer"">

            <div class=""modal-dialog"">
                <div class=""modal-content"">
                    <div class=""modal-header"">
                        <h4 class=""modal-title"">Copy Template</h4>
                        <button type=""button"" class=""close"" data-dismiss=""modal"" aria-hidden=""true""></button>
                    </div>
                    <div class=""modal-body"">

                        <div class=""row"">
                            <div class=""col-md-12"">
                                <div class=""form-horizontal"" role=""form"">
                                    <div>
                                        <div class=""form-body"" style=""margin-top: 15px;"">
                                            <div class=""form-group"">
                                 ");
            WriteLiteral(@"               <div class=""col-md-9"">
                                                    <input id=""txtArticleTitle"" class=""form-control lead text-muted"" placeholder=""Enter the name of the new template"" />
                                                    <label class=""text-muted""style=""margin-top: 5px;margin-left: 15px;"">Template will save under Marketing Templates</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class=""form-body"">
                                            <div class=""form-group"">
                                                <label class=""col-md-3 control-label""></label>
                                                <div class=""col-md-9"">
                                                    <select id=""ddlTemplatesTypes2"" class=""form-control lead text-muted"" onchange=""Newsletter.ddlTemplatesTypes_OnChange()""></select>");
            WriteLiteral(@"
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class=""modal-footer"" style=""margin-top:20px"">
                        <button type=""button"" data-dismiss=""modal"" class=""cancel btn btn-lg btn-default"">Cancel</button>
                        <button type=""button"" class=""confirm btn btn-lg btn-primary"" id=""btnSaveArticle"" onclick=""Newsletter.saveArticleTemplate()"">Save</button>
                    </div>


                </div>
            </div>

        </div>
    </div>
    ");
            EndContext();
            BeginContext(15531, 57, false);
#line 339 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Newsletter\Index.cshtml"
Write(Html.Partial("~/Views/Newsletter/_EditNewsletter.cshtml"));

#line default
#line hidden
            EndContext();
            BeginContext(15588, 212, true);
            WriteLiteral("\r\n    <div id=\"sendNewsletterModel\" class=\"modal fade\" tabindex=\"-1\" data-width=\"400\" data-keyboard=\"false\" role=\"dialog\" aria-labelledby=\"sendNewsletterModel\" aria-hidden=\"true\" data-backdrop=\"static\">\r\n        ");
            EndContext();
            BeginContext(15801, 57, false);
#line 341 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Newsletter\Index.cshtml"
   Write(Html.Partial("~/Views/Newsletter/_SendNewsletter.cshtml"));

#line default
#line hidden
            EndContext();
            BeginContext(15858, 1061, true);
            WriteLiteral(@"
    </div>
    <div id=""previewNewsletterModel1"" class=""modal fade"" tabindex=""-1"" data-width=""600"" data-keyboard=""false"" role=""dialog"" aria-labelledby=""previewNewsletterModel1"" aria-hidden=""true"" data-backdrop=""static"">

        <div class=""modal-dialog  modal-full"">
            <div class=""modal-content"">
                <div class=""modal-header btn-primary"" style=""padding-bottom: 10px"">
                    <h4 class=""modal-title"">Preview</h4>
                    <button type=""button"" class=""close"" data-dismiss=""modal"" aria-hidden=""true""></button>
                </div>

                <div class=""modal-body"" style="" height: calc(100vh - 200px);overflow: hidden;"">


                    <iframe id=""editorPreview""></iframe>



                </div>
                <div class=""modal-footer"">
                    <button type=""button"" data-dismiss=""modal"" class=""btn btn-primary"">Close</button>
                </div>

            </div>
        </div>
    </div>
</div>
<div id=""dvHidd");
            WriteLiteral("en\" style=\"display:none\">\r\n</div>\r\n\r\n");
            EndContext();
            DefineSection("styles", async() => {
                BeginContext(16935, 8, true);
                WriteLiteral("\r\n\r\n    ");
                EndContext();
                BeginContext(16943, 142, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "23f5b2a832597b00ca14c582210c284326c3b55522764", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "href", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                AddHtmlAttributeValue("", 16955, "~/Content/kendo/styles/kendo.common.min.css?v=", 16955, 46, true);
#line 373 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Newsletter\Index.cshtml"
AddHtmlAttributeValue("", 17001, Configuration.GetSection("Configuration")["staticFileVersion"], 17001, 63, false);

#line default
#line hidden
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(17085, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(17091, 143, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "23f5b2a832597b00ca14c582210c284326c3b55524602", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "href", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                AddHtmlAttributeValue("", 17103, "~/Content/kendo/styles/kendo.default.min.css?v=", 17103, 47, true);
#line 374 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Newsletter\Index.cshtml"
AddHtmlAttributeValue("", 17150, Configuration.GetSection("Configuration")["staticFileVersion"], 17150, 63, false);

#line default
#line hidden
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(17234, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(17240, 150, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "23f5b2a832597b00ca14c582210c284326c3b55526441", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "href", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                AddHtmlAttributeValue("", 17252, "~/Content/kendo/styles/kendo.default.mobile.min.css?v=", 17252, 54, true);
#line 375 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Newsletter\Index.cshtml"
AddHtmlAttributeValue("", 17306, Configuration.GetSection("Configuration")["staticFileVersion"], 17306, 63, false);

#line default
#line hidden
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(17390, 4, true);
                WriteLiteral("\r\n\r\n");
                EndContext();
            }
            );
            BeginContext(17397, 6, true);
            WriteLiteral("\r\n\r\n\r\n");
            EndContext();
            DefineSection("scripts", async() => {
                BeginContext(17420, 640, true);
                WriteLiteral(@"
    <script>

        $('#dashBoard').removeClass(""active"");
        $('#subscriber').removeClass(""active"");
        $('#newsLetter').removeClass(""active"");
        $('#scheduledNewsLetters').removeClass(""active"");
        $('#newsLetter').addClass(""active"");



        $(window).bind(""load"", function () {
            $('#tblParadigm').DataTable({
                ""order"": [[3, ""desc""]],
                ""paging"": false,
            });



            $('#tblMarketing').DataTable({
                ""order"": [[1, ""desc""]],
                ""paging"": false,
            });




        });
    </script>

    ");
                EndContext();
                BeginContext(18060, 125, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "23f5b2a832597b00ca14c582210c284326c3b55529262", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                AddHtmlAttributeValue("", 18073, "~/Content/ScriptsView/Newsletter.js?v=", 18073, 38, true);
#line 411 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Newsletter\Index.cshtml"
AddHtmlAttributeValue("", 18111, Configuration.GetSection("Configuration")["staticFileVersion"], 18111, 63, false);

#line default
#line hidden
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(18185, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(18191, 125, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "23f5b2a832597b00ca14c582210c284326c3b55531012", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                AddHtmlAttributeValue("", 18204, "~/Content/kendo/js/kendo.all.min.js?v=", 18204, 38, true);
#line 412 "F:\LTC Git\Dashboard\LTC_Dashboard\Views\Newsletter\Index.cshtml"
AddHtmlAttributeValue("", 18242, Configuration.GetSection("Configuration")["staticFileVersion"], 18242, 63, false);

#line default
#line hidden
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(18316, 271, true);
                WriteLiteral(@"
    <script id=""treeview-template"" type=""text/kendo-ui-template"">
        # if (!item.items) { #
        <a class='k-icon k-i-close-outline' onclick=""Newsletter.DeleteImage('#= item.text #');"" href='\#'></a>
        # } #
        #: item.text #
    </script>


");
                EndContext();
            }
            );
            BeginContext(18590, 8, true);
            WriteLiteral("\r\n\r\n\r\n\r\n");
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
