using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTCDataManager.Dashboard;
using LTCDataModel.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LTCDashboard.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly gChartManager _gChartManager;
        private ConfigSettings _configuration;

        public DashboardController(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
            _gChartManager = new gChartManager(configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Production()
        {
            return View();
        }
        public IActionResult RecareNTreatment()
        {
            return View();
        }
        public IActionResult Other()
        {
            return View();
        }

        #region JSON Calls

        [HttpPost]
        public ActionResult GetCharts(string pageName)
        {
            try
            {
                var modelChartsAvailable = gDashboardChartManager.GetAllChartsByPageName(OfficeSequence, UserId, pageName);
                var UserCharts = gDashboardChartManager.GetUserCharts(OfficeSequence, UserId);

                return Json(new { Success = true, Data = modelChartsAvailable, UserCharts = UserCharts });
            }
            catch (Exception ex)
            {
                return Json(new { Sucess = false });
            }
        }


        [HttpPost]
        public ActionResult AddCard(int chartId)
        {
            try
            {
                gDashboardChartManager.AddUserChart(chartId, UserId, OfficeSequence);

                return Json(new { Success = true });
            }
            catch (Exception ex)
            {
                return Json(new { Sucess = false });
            }
        }

        public ActionResult UpdateCardFilterTypes(int chartId, string filterTypes)
        {
            try
            {
                gDashboardChartManager.UpdateCardFilterTypes(chartId, filterTypes, OfficeSequence);

                return Json(new { Success = true });
            }
            catch (Exception ex)
            {
                return Json(new { Sucess = false });
            }
        }


        [HttpPost]
        public ActionResult RemoveChart(int chartId)
        {
            try
            {
                gDashboardChartManager.DeleteChart(chartId, UserId, OfficeSequence);
                return Json(new { Success = true });
            }
            catch (Exception ex)
            {
                return Json(new { Sucess = false });
            }
        }

        [HttpPost]
        public ActionResult LoadChartData(string chartName, int[] offices, string[] providers, string startDate, string endDate, string types)
        {
            try
            {
                if (chartName == "NewPatient")
                {
                    var data = _gChartManager.GetNewPatient(offices, providers, startDate, endDate);
                    return Json(new { Success = true, Data = data, IsCurrency = false, hasMultipleRecords = false });
                }
                else if (chartName == "TotalNetProduction")
                {
                    var data = _gChartManager.GetTotalNetProduction(offices, providers, startDate, endDate, types);
                    return Json(new { Success = true, Data = data, IsCurrency = true, hasMultipleRecords = false });
                }
                else if (chartName == "TotalPaymentReceipt") //new
                {
                    var data = _gChartManager.GetTotalPaymentReceipt(offices, providers, startDate, endDate, types);
                    return Json(new { Success = true, Data = data, IsCurrency = true, hasMultipleRecords = false });
                }
                else if (chartName == "TotalNetPaymentReceipt") //new
                {
                    var data = _gChartManager.GetTotalNetPaymentReceipt(offices, providers, startDate, endDate, types);
                    return Json(new { Success = true, Data = data, IsCurrency = true, hasMultipleRecords = false });
                }
                else if (chartName == "TotalNetHygenistProduction")
                {
                    var data = _gChartManager.GetTotalHygenistProduction(offices, providers, startDate, endDate, types);
                    return Json(new { Success = true, Data = data, IsCurrency = true, hasMultipleRecords = false });
                }
                else if (chartName == "TotalNetDoctorProduction")
                {
                    var data = _gChartManager.GetTotalNetDoctorProduction(offices, providers, startDate, endDate, types);
                    return Json(new { Success = true, Data = data, IsCurrency = true, hasMultipleRecords = false });
                }
                else if (chartName == "AverageProductionPerPatient")
                {
                    var data = _gChartManager.GetAverageProductionPerPatient(offices, providers, startDate, endDate, types);
                    return Json(new { Success = true, Data = data, IsCurrency = true, hasMultipleRecords = false });
                }
                else if (chartName == "ServiceAnalysis")
                {
                    //pie chart
                    var data = _gChartManager.GetServiceAnalysis(offices, providers, startDate, endDate, types);
                    return Json(new { Success = true, Data = data, hasMultipleRecords = true });
                }
                else if (chartName == "CancellationAndNoShows")
                {
                    //pie chart and line chart
                    var data = _gChartManager.GetCancellationAndNoShows(offices, providers, startDate, endDate, types);
                    return Json(new { Success = true, Data = data, hasMultipleRecords = true });
                }

                return Json(new { Success = true });
            }
            catch (Exception ex)
            {
                return Json(new { Sucess = false });
            }
        }

        [HttpPost]
        public ActionResult LoadChartDataBreakdown(string chartName, int[] offices, string[] providers, string startDate, string endDate, string types)
        {
            try
            {
                if (chartName == "NewPatient")
                {
                    var data = _gChartManager.GetNewPatient(offices, providers, startDate, endDate);
                    return Json(new { Success = true, Data = data });
                }
                else if (chartName == "TotalNetProduction")
                {
                    var data = _gChartManager.GetTotalNetProductionBreakdown(offices, providers, startDate, endDate, types);
                    return Json(new { Success = true, Data = data });
                }
                else if (chartName == "TotalPaymentReceipt")
                {
                    var data = _gChartManager.GetTotalPaymentReceiptBreakdown(offices, providers, startDate, endDate, types);
                    return Json(new { Success = true, Data = data });
                }
                else if (chartName == "TotalNetPaymentReceipt")
                {
                    var data = _gChartManager.GetTotalNetPaymentReceiptBreakdown(offices, providers, startDate, endDate, types);
                    return Json(new { Success = true, Data = data });
                }
                else if (chartName == "TotalNetHygenistProduction")
                {
                    var data = _gChartManager.GetTotalHygenistProductionBreakdown(offices, providers, startDate, endDate, types);
                    return Json(new { Success = true, Data = data });
                }
                else if (chartName == "TotalNetDoctorProduction")
                {
                    var data = _gChartManager.GetTotalNetDoctorProductionBreakdown(offices, providers, startDate, endDate, types);
                    return Json(new { Success = true, Data = data });
                }
                else if (chartName == "ServiceAnalysis")
                {
                    var data = _gChartManager.GetServiceAnalysisBreakdown(offices, providers, startDate, endDate, types);
                    return Json(new { Success = true, Data = data });
                }
                else if (chartName == "CancellationAndNoShows")
                {
                    var data = _gChartManager.GetCancellationAndNoShowsBreakdown(offices, providers, startDate, endDate, types);
                    return Json(new { Success = true, Data = data, hasMultipleRecords = true });
                }

                return Json(new { Success = true });
            }   
            catch (Exception ex)
            {
                return Json(new { Sucess = false });
            }
        }


        [HttpPost]
        public ActionResult LoadPatientList(int[] offices, string[] providers, string startDate, string endDate)
        {
            try
            {
                var data = _gChartManager.GetPatientRecords(offices, providers, startDate, endDate);
                return Json(new { Success = true, Data = data });
            }
            catch (Exception ex)
            {
                return Json(new { Sucess = false });
            }
        }

        [HttpPost]
        public ActionResult LoadPatientProfile(int id, int officeSequence)
        {
            try
            {
                var data = _gChartManager.GetPatientProfile(id, officeSequence);
                return Json(new { Success = true, Data = data });
            }
            catch (Exception ex)
            {
                return Json(new { Sucess = false });
            }
        }


        [HttpPost]
        public ActionResult LoadPatientAppointment(int id, int officeSequence, string startDate, string endDate)
        {
            try
            {
                var data = _gChartManager.LoadPatientAppointment(id, officeSequence, startDate, endDate);
                return Json(new { Success = true, Data = data });
            }
            catch (Exception ex)
            {
                return Json(new { Sucess = false });
            }
        }

        [HttpPost]
        public ActionResult LoadPatientTreatment(int id, int officeSequence, string startDate, string endDate)
        {
            try
            {
                var data = _gChartManager.LoadPatientTreatment(id, officeSequence, startDate, endDate);
                return Json(new { Success = true, Data = data });
            }
            catch (Exception ex)
            {
                return Json(new { Sucess = false });
            }
        }

        #endregion




        #region Kendo Download

        [HttpPost]
        public ActionResult Index_Proxy(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        #endregion
    }
}