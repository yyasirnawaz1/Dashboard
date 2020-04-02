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

        public string Email { get; set; }
        public string EmailContent { get; set; }
        public string EmailSubject { get; set; }

        public string PatientName { get; set; }
        public int NewsletterID { get; set; }
        public int SubscriberID { get; set; }


        public DateTime DateToSendEmail { get; set; }
        public DateTime EmailSentOnDate { get; set; }

        public int Account { get; set; }
        public int Status { get; set; }
        public bool EmailSent { get; set; }
        public string MessageID { get; set; }
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
        public string PatientName { get; set; }
        public int office_Sequence { get; set; }
        public DateTime DateToSendEmail { get; set; }
        public string EmailContent { get; set; }
        public string EmailSubject { get; set; }
        public bool PublicNewsletter { get; set; }
        public int NoOfRetry { get; set; }

    }
}
