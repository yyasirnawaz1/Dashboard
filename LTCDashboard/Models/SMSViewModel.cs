using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTCDataModel.SMS;

namespace LTCDashboard.Models
{
    public class SMSViewModel
    {
        public int DailySMSCount { get; set; }
        public int DailyEmailCount { get; set; }
        public int DailyRecallCount { get; set; }
        public int DailyPreConfirmationCount { get; set; }

        public List<CurrentCounts> EmailCounts { get; set; }
        public List<CurrentCounts> ConfirmationCounts { get; set; }
        public List<CurrentCounts> RecallCounts { get; set; }

        public List<CurrentCounts> SMSCounts { get; set; }
    }
    public class AppointmentViewModel
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<gAppointments> data { get; set; }
    }
    public class CurrentCounts
    {
        public string Date { get; set; }
        public int Count { get; set; }
    }
}
