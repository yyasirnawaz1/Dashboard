using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.Dashboard
{
    public class gCancellationAndNoShowsChart
    {
        public int Office_Sequence { get; set; }
        //public string Provider { get; set; }
        //public string ProviderName { get; set; }
        //public int? PatientNumber { get; set; }
        //public string PatientName { get; set; }
        //public DateTime ContactDate { get; set; }
        //public string Job { get; set; }
        //public int TimeSlot { get; set; }
        //public string Time { get; set; }
        public string ContactDate { get; set; }
        public int Count { get; set; }
    }

    public class gCancellationAndNoShows
    {
        public int Office_Sequence { get; set; }
        public string Provider { get; set; }
        public string ProviderName { get; set; }
        public int? PatientNumber { get; set; }
        public string PatientName { get; set; }
        public DateTime ContactDate { get; set; }
        public string ContactDateString => ContactDate.ToString("MM/dd/yyyy");
        public string Job { get; set; }
        public int TimeSlot { get; set; }
        public TimeSpan Time { get; set; }
        public string TimeString => DateTime.Today.Add(Time).ToString("%h:mm tt");
    }
}
