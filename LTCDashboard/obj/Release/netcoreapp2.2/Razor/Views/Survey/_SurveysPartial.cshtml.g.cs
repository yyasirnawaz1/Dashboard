#pragma checksum "C:\Users\Yasir\source\repos\LTCDashboard\LTCDashboard\Views\Survey\_SurveysPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d14295aeb4587eee0df32559443111da4a1ba8e0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Survey__SurveysPartial), @"mvc.1.0.view", @"/Views/Survey/_SurveysPartial.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Survey/_SurveysPartial.cshtml", typeof(AspNetCore.Views_Survey__SurveysPartial))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d14295aeb4587eee0df32559443111da4a1ba8e0", @"/Views/Survey/_SurveysPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67cd85531b82a25b1d9f9c9f760a4e079ce716a1", @"/Views/_ViewImports.cshtml")]
    public class Views_Survey__SurveysPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 1341, true);
            WriteLiteral(@"<div class=""row card"">
    <div class=""col-md-12"">
        <div class=""block block-condensed noWhite"">
            <div class=""block-content"">
                <table id=""tblSurveys"" class=""table datatable-basic table-striped datatable-extended"">
                    <thead>
                    <tr>
                        <th>ID</th>
                        <th>FormID</th>
                        <th>Patient Number</th>
                        <th>Action</th>
                    </tr>
                    </thead>
                </table>
            </div>
        </div>

    </div>
</div>

<div class=""modal fade"" id=""renderSurveysModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""modal-default-header"" data-backdrop=""static"" data-keyboard=""false"">
    <div class=""modal-dialog  modal-lg"" role=""document"">
        <div class=""modal-content block minModelSize"">
            <div class=""modal-header"">
                <h4 class=""modal-title"" id=""renderSurveysModal-header"">Rendering View</h4>");
            WriteLiteral(@"
                <button type=""button"" class=""close"" data-dismiss=""modal"">&times;</button>
            </div>
            <div class=""block-divider""></div>
            <div class=""modal-body"">
                <div id=""renderSurveys"">
                </div>
            </div>
        </div>
    </div>
</div>");
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
