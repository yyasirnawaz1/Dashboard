using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.FormEntryHome
{
    public class gTodayAppointment
    {
        public int AppointCounter { get; set; }
        public int Account { get; set; }
        public string provider { get; set; }
        public DateTime AppointDate { get; set; }
        public int TimeSLot { get; set; }
        public string Name { get; set; }
        public string job { get; set; }
        public string DESCRIPTION { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public DateTime ArrivedTime { get; set; }
        public DateTime TakenInTime { get; set; }
        public string PatientNumber { get; set; }
        public string Office_Sequence { get; internal set; }
    }
    public class gTodayAppointmentViewModel
    {
        public int AppointCounter { get; set; }
        public int Account { get; set; }
        public string provider { get; set; }
        public string AppointDate { get; set; }
        public string TimeSLot { get; set; }
        public string Name { get; set; }
        public string job { get; set; }
        public string DESCRIPTION { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string ArrivedTime { get; set; }
        public string TakenInTime { get; set; }

        public string PatientNumber { get; set; }
        public string Office_Sequence { get;  set; }
    }
    public class gAddQueueEntryModel
    {
        public int AppointmentCounter { get; set; }
        public int FormID { get; set; }
        public int OfficeID { get; set; }
        public int Type { get; set; }
        public string PatientNumber { get; set; }
        public int Office_Sequence { get; set; }
    }
    public class gSaveSurveyAndFormModel
    {
        public int AppointmentCounter { get; set; }
        public int FormID { get; set; }
        public int Type { get; set; }
        public string Content { get; set; }
        public int Office_Sequence { get; set; }
        public string PatientNumber { get; set; }
        public string PdfContent { get; set; }

        public bool IsSurveyForm { get; set; }
    }
}
