#pragma checksum "/Users/myasirnawaz/Documents/Projects/Dashboard/LTCDashboard/Views/Dashboard/RecareNTreatment.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "72e6ce77240d7c3dd47fd8eec367f7cb6bfb41fc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Dashboard_RecareNTreatment), @"mvc.1.0.view", @"/Views/Dashboard/RecareNTreatment.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Dashboard/RecareNTreatment.cshtml", typeof(AspNetCore.Views_Dashboard_RecareNTreatment))]
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
#line 1 "/Users/myasirnawaz/Documents/Projects/Dashboard/LTCDashboard/Views/_ViewImports.cshtml"
using LTCDashboard;

#line default
#line hidden
#line 2 "/Users/myasirnawaz/Documents/Projects/Dashboard/LTCDashboard/Views/_ViewImports.cshtml"
using LTCDashboard.Models;

#line default
#line hidden
#line 1 "/Users/myasirnawaz/Documents/Projects/Dashboard/LTCDashboard/Views/Dashboard/RecareNTreatment.cshtml"
using Microsoft.Extensions.Configuration;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"72e6ce77240d7c3dd47fd8eec367f7cb6bfb41fc", @"/Views/Dashboard/RecareNTreatment.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2166ac7352159f215a0ab09cb8ee87cb308f596e", @"/Views/_ViewImports.cshtml")]
    public class Views_Dashboard_RecareNTreatment : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
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
#line 3 "/Users/myasirnawaz/Documents/Projects/Dashboard/LTCDashboard/Views/Dashboard/RecareNTreatment.cshtml"
  
    ViewBag.Title = "Recare andTreatment";

#line default
#line hidden
            BeginContext(127, 4695, true);
            WriteLiteral(@"


<div class=""card-body dashboard"">
    <div class=""row"">
        <!--Left Panel -->
        <div class=""col-md-12"" id=""middlearea"">
            <div class=""row"">
                <div class=""col-lg-3"">
                    <div class=""card bg-blue-400"">
                        <div class=""card-body dashboardCard pt-1 pb-1"">
                            <div class=""d-flex cardHeading"">
                                <h3 class=""font-weight-semibold mb-0"">Total Net Prod.</h3>
                                <div class=""list-icons ml-auto"">
                                    <a class=""list-icons-item"" data-action=""remove""></a>
                                </div>
                            </div>
                            <div class=""d-flex justify-content-center cardContent"">
                                <span class=""font-weight-semibold mb-0 font-size-xs"">$20,000</span>
                            </div>
                            <div class=""d-flex justify-content-end cardContent"">
                  ");
            WriteLiteral(@"              <input type=""button"" class=""btn bg-purple-400"" value=""Patient List"" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class=""col-lg-3"">
                    <div class=""card bg-teal-400"">
                        <div class=""card-body dashboardCard pt-1 pb-1"">
                            <div class=""d-flex cardHeading"">
                                <h3 class=""font-weight-semibold mb-0"">Total Net Recare Prod.</h3>
                                <div class=""list-icons ml-auto"">
                                    <a class=""list-icons-item"" data-action=""remove""></a>
                                </div>
                            </div>
                            <div class=""d-flex justify-content-center cardContent"">
                                <span class=""font-weight-semibold mb-0 font-size-xs"">$20</span>
                            </div>
                            <div class=""d-flex justify-cont");
            WriteLiteral(@"ent-end cardContent"">

                            </div>
                        </div>
                    </div>
                </div>
                <div class=""col-lg-3"">
                    <div class=""card bg-success"">
                        <div class=""card-body dashboardCard pt-1 pb-1"">
                            <div class=""d-flex cardHeading"">
                                <h3 class=""font-weight-semibold mb-0"">Total Net Hygene Prod.</h3>
                                <div class=""list-icons ml-auto"">
                                    <a class=""list-icons-item"" data-action=""remove""></a>
                                </div>
                            </div>
                            <div class=""d-flex justify-content-center cardContent"">
                                <span class=""font-weight-semibold mb-0 font-size-xs"">$20</span>
                            </div>
                            <div class=""d-flex justify-content-end cardContent"">

                            </div>
     ");
            WriteLiteral(@"                   </div>
                    </div>
                </div>
                <div class=""col-lg-3"">
                    <div class=""card bg-pink-400"">
                        <div class=""card-body dashboardCard pt-1 pb-1"">
                            <div class=""d-flex cardHeading"">
                                <h3 class=""font-weight-semibold mb-0"">TAvg. Production/Patient</h3>
                                <div class=""list-icons ml-auto"">
                                    <a class=""list-icons-item"" data-action=""remove""></a>
                                </div>
                            </div>
                            <div class=""d-flex justify-content-center cardContent"">
                                <span class=""font-weight-semibold mb-0 font-size-xs"">20</span>
                            </div>
                            <div class=""d-flex justify-content-end cardContent"">

                            </div>
                        </div>
                    </div>
        ");
            WriteLiteral(@"        </div>
            </div>
            <div class=""row"">
                <div class=""col-md-12"">
                    <div class=""card dashboardGraph"">
                        <div class=""card-body"">
                            <div class=""chart-container"">
                                <div class=""chart smallBarGraph"" id=""c3-line-chart""></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class=""row"">
                <div class=""col-md-12"">
                    <div class=""card"">
");
            EndContext();
            BeginContext(5312, 3830, true);
            WriteLiteral(@"                        <div class=""card-body"">
                            <div class=""table-responsive"">
                                <table class=""table"">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Production</th>
                                            <th>Percentage(%)</th>
                                            <th>Doller Value($)</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>1</td>
                                            <td>xxx</td>
                                            <td>xxx%</td>
                                            <td>xxx%</td>
                                        </tr>
                                        <tr>
                    ");
            WriteLiteral(@"                        <td>1</td>
                                            <td>xxx</td>
                                            <td>xxx%</td>
                                            <td>xxx%</td>
                                        </tr>
                                        <tr>
                                            <td>1</td>
                                            <td>xxx</td>
                                            <td>xxx%</td>
                                            <td>xxx%</td>
                                        </tr>
                                        <tr>
                                            <td>1</td>
                                            <td>xxx</td>
                                            <td>xxx%</td>
                                            <td>xxx%</td>
                                        </tr>
                                        <tr>
                                            <td>1</td>
                                ");
            WriteLiteral(@"            <td>xxx</td>
                                            <td>xxx%</td>
                                            <td>xxx%</td>
                                        </tr>
                                        <tr>
                                            <td>1</td>
                                            <td>xxx</td>
                                            <td>xxx%</td>
                                            <td>xxx%</td>
                                        </tr>
                                        <tr>
                                            <td>1</td>
                                            <td>xxx</td>
                                            <td>xxx%</td>
                                            <td>xxx%</td>
                                        </tr>
                                        <tr>
                                            <td>1</td>
                                            <td>xxx</td>
                                          ");
            WriteLiteral(@"  <td>xxx%</td>
                                            <td>xxx%</td>
                                        </tr>
                                        <tr>
                                            <td>1</td>
                                            <td>xxx</td>
                                            <td>xxx%</td>
                                            <td>xxx%</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--Right Panel to Put Dragable Widgets Timeline-->
    </div>
</div>




");
            EndContext();
            DefineSection("headscripts", async() => {
                BeginContext(9163, 33, true);
                WriteLiteral("\n    <!-- Theme JS files -->\n    ");
                EndContext();
                BeginContext(9196, 163, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "72e6ce77240d7c3dd47fd8eec367f7cb6bfb41fc12824", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                AddHtmlAttributeValue("", 9209, "~/Resources/Limitless/global_assets/js/plugins/visualization/d3/d3.min.js?v=", 9209, 76, true);
#line 193 "/Users/myasirnawaz/Documents/Projects/Dashboard/LTCDashboard/Views/Dashboard/RecareNTreatment.cshtml"
AddHtmlAttributeValue("", 9285, Configuration.GetSection("Configuration")["staticFileVersion"], 9285, 63, false);

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
                BeginContext(9359, 5, true);
                WriteLiteral("\n    ");
                EndContext();
                BeginContext(9364, 163, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "72e6ce77240d7c3dd47fd8eec367f7cb6bfb41fc14615", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                AddHtmlAttributeValue("", 9377, "~/Resources/Limitless/global_assets/js/plugins/visualization/c3/c3.min.js?v=", 9377, 76, true);
#line 194 "/Users/myasirnawaz/Documents/Projects/Dashboard/LTCDashboard/Views/Dashboard/RecareNTreatment.cshtml"
AddHtmlAttributeValue("", 9453, Configuration.GetSection("Configuration")["staticFileVersion"], 9453, 63, false);

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
                BeginContext(9527, 1, true);
                WriteLiteral("\n");
                EndContext();
                BeginContext(9702, 5998, true);
                WriteLiteral(@"

    <script type=""text/javascript"">
        function initializePieChart(id) {
            var donut_chart_element = document.getElementById(id);
            // Generate chart

            // Generate chart
            var donut_chart = c3.generate({
                bindto: donut_chart_element,
                size: { width: 170, height: 100 },
                color: {
                    pattern: ['#3F51B5', '#FF9800', '#4CAF50', '#00BCD4', '#F44336']
                },
                data: {
                    columns: [
                        ['data1', 30],
                        ['data2', 120],
                    ],
                    type: 'donut',
                    labels: false
                },
                donut: {
                    title: ""30%""
                },
                legend: {
                    show: false
                }
            });

            // Change data
            //setTimeout(function () {
            //    donut_chart.load({
            //        columns");
                WriteLiteral(@": [
            //            [""setosa"", 0.2, 0.2, 0.2, 0.2, 0.2, 0.4, 0.3, 0.2, 0.2, 0.1, 0.2, 0.2, 0.1, 0.1, 0.2, 0.4, 0.4, 0.3, 0.3, 0.3, 0.2, 0.4, 0.2, 0.5, 0.2, 0.2, 0.4, 0.2, 0.2, 0.2, 0.2, 0.4, 0.1, 0.2, 0.2, 0.2, 0.2, 0.1, 0.2, 0.2, 0.3, 0.3, 0.2, 0.6, 0.4, 0.3, 0.2, 0.2, 0.2, 0.2],
            //            [""versicolor"", 1.4, 1.5, 1.5, 1.3, 1.5, 1.3, 1.6, 1.0, 1.3, 1.4, 1.0, 1.5, 1.0, 1.4, 1.3, 1.4, 1.5, 1.0, 1.5, 1.1, 1.8, 1.3, 1.5, 1.2, 1.3, 1.4, 1.4, 1.7, 1.5, 1.0, 1.1, 1.0, 1.2, 1.6, 1.5, 1.6, 1.5, 1.3, 1.3, 1.3, 1.2, 1.4, 1.2, 1.0, 1.3, 1.2, 1.3, 1.3, 1.1, 1.3],
            //            [""virginica"", 2.5, 1.9, 2.1, 1.8, 2.2, 2.1, 1.7, 1.8, 1.8, 2.5, 2.0, 1.9, 2.1, 2.0, 2.4, 2.3, 1.8, 2.2, 2.3, 1.5, 2.3, 2.0, 2.0, 1.8, 2.1, 1.8, 1.8, 1.8, 2.1, 1.6, 1.9, 2.0, 2.2, 1.5, 1.4, 2.3, 2.4, 1.8, 1.8, 2.1, 2.4, 2.3, 1.9, 2.3, 2.5, 2.3, 1.9, 2.0, 2.3, 1.8],
            //        ]
            //    });
            //}, 4000);
            //setTimeout(function () {
            //    donut_chart.unload({
 ");
                WriteLiteral(@"           //        ids: 'data1'
            //    });
            //    donut_chart.unload({
            //        ids: 'data2'
            //    });
            //}, 8000);

            // Resize chart on sidebar width change
            $('.sidebar-control').on('click', function () {
                donut_chart.resize();
            });
        }

        function initializeBarChart(id) {
            var bar_chart_element = document.getElementById(id);
            var bar_chart = c3.generate({
                bindto: bar_chart_element,
                //size: { width: 170, height: 150 },
                data: {
                    columns: [
                        ['data1', 30, 200, 100, 200, 150, 250],
                        ['data2', 130, 100, 140, 200, 150, 50]
                    ],
                    type: 'bar'
                },
                color: {
                    pattern: ['#2196F3', '#FF9800', '#4CAF50']
                },
                bar: {
                    //width: {
        ");
                WriteLiteral(@"            //    ratio: 0.5
                    //}
                },
                axis: {
                    y: {
                        show: false
                    }
                },
                legend: {
                    show: false
                }
            });

            //// Change data
            //setTimeout(function () {
            //    bar_chart.load({
            //        columns: [
            //            ['data3', 130, -150, 200, 300, -200, 100]
            //        ]
            //    });
            //}, 6000);

            // Resize chart on sidebar width change
            $('.sidebar-control').on('click', function () {
                bar_chart.resize();
            });
        }

        function initializeLineChart(id) {
            var line_chart_element = document.getElementById(id);
            if (line_chart_element) {

                // Generate chart
                var line_chart = c3.generate({
                    bindto: line_chart_element,
      ");
                WriteLiteral(@"              point: {
                        r: 4
                    },
                    size: { height: 400 },
                    color: {
                        pattern: ['#4CAF50', '#F4511E', '#1E88E5']
                    },
                    data: {
                        columns: [
                            ['data1', 30, 200, 100, 400, 150, 250],
                            ['data2', 50, 20, 10, 40, 15, 25]
                        ],
                        type: 'spline'
                    },
                    grid: {
                        y: {
                            show: true
                        }
                    },
                    legend: {
                        show: false
                    }

                });

                //// Change data
                //setTimeout(function () {
                //    line_chart.load({
                //        columns: [
                //            ['data1', 230, 190, 300, 500, 300, 400]
                //        ]");
                WriteLiteral(@"
                //    });
                //}, 3000);
                //setTimeout(function () {
                //    line_chart.load({
                //        columns: [
                //            ['data3', 130, 150, 200, 300, 200, 100]
                //        ]
                //    });
                //}, 6000);
                //setTimeout(function () {
                //    line_chart.unload({
                //        ids: 'data1'
                //    });
                //}, 9000);

                // Resize chart on sidebar width change
                $('.sidebar-control').on('click', function () {
                    line_chart.resize();
                });
            }
        }

        $(document).ready(function () {
            //initializeBarChart('c3-bar-chart');
            initializeLineChart('c3-line-chart');
        });
    </script>
");
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
