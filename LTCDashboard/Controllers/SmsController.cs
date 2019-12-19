using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDashboard.Models;
using LTCDataManager.SMS;
using LTCDataModel.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LTCDashboard.Controllers
{
    public class SmsController : BaseController
    {
        private ConfigSettings _configuration;
        public SmsController(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
        }

        public IActionResult Index()
        {
            return View();
        }


        public ActionResult LoadSMS()
        {
            return PartialView("~/Views/Shared/PartialViews/sms/_SmsLoad.cshtml");
        }
        public string LoadAppointments(string office_sequence, string startDate, string endDate, string sEcho, int iDisplayStart, int iDisplayLength, string sSearch)
        {
            AppointmentViewModel viewModel = new AppointmentViewModel();
            //int PageIndex = Convert.ToInt32(Request.Form["start"]);
            //int PageSize = Convert.ToInt32(Request.Form["length"]); 
            viewModel.data = gSmsManager.LoadAppointments(office_sequence, DateTime.Parse(startDate), DateTime.Parse(endDate), iDisplayStart, iDisplayLength);

            foreach (var appointment in viewModel.data)
            {
                DateTime dateOnly = appointment.AppointmentDate.Date;
                TimeSpan ts = DateTime.Parse(appointment.AppointmentTime.TrimEnd().TrimStart()).TimeOfDay;
                appointment.AppointmentDate = dateOnly + ts;
                appointment.AppointmentDateTime = appointment.AppointmentDate.ToString("yyyy-MM-dd") + " " + appointment.AppointmentTime;
            }
            viewModel.data = viewModel.data.OrderBy(s => s.AppointmentDate).ToList();
            foreach (var appointment in viewModel.data)
            {
                if (appointment.ActionType == "C")
                {
                    appointment.ActionTypeDetail = "Confirm";
                }
                else if (appointment.ActionType == "P")
                {
                    appointment.ActionTypeDetail = "PreConfirm";
                }
                else if (appointment.ActionType == "AR")
                {
                    appointment.ActionTypeDetail = "Recall";
                }
                else if (appointment.ActionType == "B")
                {
                    appointment.ActionTypeDetail = "Birthday";
                }
                else
                {
                    appointment.ActionTypeDetail = appointment.ActionType;
                }
                if (!appointment.SMSSendDate.HasValue)
                {
                    appointment.SMSSendDateString = "-";
                }
                else
                {
                    appointment.SMSSendDateString = appointment.SMSSendDate.Value.ToString("yyyy-MM-dd");
                }
                if (!appointment.EMailSendDate.HasValue)
                {
                    appointment.EmailSendDateString = "-";
                }
                else
                {
                    appointment.EmailSendDateString = appointment.EMailSendDate.Value.ToString("yyyy-MM-dd");
                }


                if (appointment.ActionDone == true)
                {
                    appointment.Response = "Done";
                }
                else
                {
                    appointment.Response = "-";
                }


            }
            int count = gSmsManager.AppointmentCount(office_sequence, DateTime.Parse(startDate), DateTime.Parse(endDate));

            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("{");
            sb.Append("\"sEcho\": ");
            sb.Append(sEcho);
            sb.Append(",");
            sb.Append("\"iTotalRecords\": ");
            sb.Append(count);
            sb.Append(",");
            sb.Append("\"iTotalDisplayRecords\": ");
            sb.Append(count);
            sb.Append(",");
            sb.Append("\"aaData\": ");
            sb.Append(JsonConvert.SerializeObject(viewModel.data));
            sb.Append("}");
            return sb.ToString();
            // return Json(new { obj = viewModel });

        }
        public JsonResult LoadDashboard(string office_sequence, string startDate, string endDate)
        {
            SMSViewModel viewModel = new SMSViewModel();
            viewModel.DailyEmailCount = gSmsManager.DailyEmailCount(office_sequence);
            viewModel.DailySMSCount = gSmsManager.DailySMSCount(office_sequence);
            viewModel.DailyPreConfirmationCount = gSmsManager.DailyPreConfirmationCount(office_sequence);
            viewModel.DailyRecallCount = gSmsManager.DailyRecallCount(office_sequence);
            var smsData = gSmsManager.LoadSMSByDate(office_sequence, DateTime.Parse(startDate), DateTime.Parse(endDate));
            var emailData = gSmsManager.LoadEmailByDate(office_sequence, DateTime.Parse(startDate), DateTime.Parse(endDate));
            var preconfirmDate = gSmsManager.LoadPreConfirmationByDate(office_sequence, DateTime.Parse(startDate), DateTime.Parse(endDate));
            var recallData = gSmsManager.LoadDailyRecallByDate(office_sequence, DateTime.Parse(startDate), DateTime.Parse(endDate));

            var dates = GetDatesBetween(DateTime.Parse(startDate), DateTime.Parse(endDate));
            viewModel.SMSCounts = new List<CurrentCounts>();
            viewModel.EmailCounts = new List<CurrentCounts>();
            viewModel.ConfirmationCounts = new List<CurrentCounts>();
            viewModel.RecallCounts = new List<CurrentCounts>();
            foreach (var date in dates)
            {
                var sumOfSmsByDay = smsData.Where(p => p.auditdate.Value.Date == date.Date).Sum(c => c.sendcount);
                viewModel.SMSCounts.Add(new CurrentCounts() { Date = date.ToString("yyyy-MM-dd"), Count = sumOfSmsByDay });
                var sumOfEmailByDay = emailData.Where(p => p.auditdate.Value.Date == date.Date).Sum(c => c.sendcount);
                viewModel.EmailCounts.Add(new CurrentCounts() { Date = date.ToString("yyyy-MM-dd"), Count = sumOfEmailByDay });
                var sumOfComfirmationByDay = preconfirmDate.Where(p => p.auditdate.Value.Date == date.Date).Sum(c => c.sendcount);

                viewModel.ConfirmationCounts.Add(new CurrentCounts() { Date = date.ToString("yyyy-MM-dd"), Count = sumOfComfirmationByDay });
                var sumOfrecallData = recallData.Where(p => p.auditdate.Value.Date == date.Date).Sum(c => c.sendcount);
                viewModel.RecallCounts.Add(new CurrentCounts() { Date = date.ToString("yyyy-MM-dd"), Count = sumOfrecallData });

            }



            return Json(new { obj = viewModel });

        }
        public List<DateTime> GetDatesBetween(DateTime startDate, DateTime endDate)
        {
            List<DateTime> allDates = new List<DateTime>();
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                allDates.Add(date);
            return allDates;
        }
    }
}