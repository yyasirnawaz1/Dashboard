using LTCDataModel.PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.NewsLetter
{
    [TableName("patientcalllist")]
    [PrimaryKey("ID", AutoIncrement = true)]
    public class gPatientCallList
    {
        //public string provider;
        public int ID { get; set; }
        public int Office_Sequence { get; set; }
        public int Branch_Number { get; set; }
        //public int Counter { get; set; }
        //public string CallType { get; set; }
        //public DateTime DateToCall { get; set; }
        //public int HourToCall { get; set; }
        //public string CallSubType { get; set; }
        public string Email { get; set; }
        //public DateTime AppointDate { get; set; }
        public string PatientName { get; set; }
        public int NewsletterID { get; set; }
        public int SubscriberID { get; set; }
        public int ScheduledID { get; set; }
        //public string CallListType { get; set; }
        public int ErrorCode { get; set; }
        public string EmailResult { get; set; }
        public bool PublicNewsletter { get; set; }
        //public int UpdateCounter { get; set; }
        //public int DownloadedToClient { get; set; }
       // public string JobCode { get; set; }
        // public string Time { get; set; }
        public DateTime EmailSentTime { get; set; }
        public DateTime EmailReceiveTime { get; set; }
        //public DateTime DialTime { get; set; }
        //public DateTime SMSReceiveTime { get; set; }
        public int Account { get; set; }
        public int Status { get; set; }
        public bool EmailSent { get; set; }
    }
    public class gPatientCallListView
    {
        public int ID { get; set; }
        public int NewsletterId { get; set; }
        public string TemplateTitle { get; set; }
        public string AppointDate { get; set; }
        public int Account { get; set; }
        public int Status { get; set; }
        public string TemplateSourceMarkup { get; set; }
        public string TemplateBodymarkup { get; set; }
        public string Email { get; set; }
        public string PatientName{ get; set; }
        public int Office_Number { get; set; }
    }
}
