using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.SMS
{
    public class gSMS
    {
        public int sendcount { get; set; }
        public DateTime? auditdate { get; set; }

    }
    public class gAppointments
    {
        public int Office_Sequence { get; set; }
        //public int AppointmentCounter { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string AppointmentDateTime { get; set; }
        public string Name { get; set; }
        public string ActionType { get; set; }
        public string ActionTypeDetail { get; set; }
        public bool ActionDone { get; set; }
        public DateTime? ActionDate { get; set; }
        //public int PatientNumber { get; set; }
        public DateTime? EMailSendDate { get; set; }
        public string EmailSendDateString { get; set; }
        //public int? NoOfEmailSend { get; set; }
        //public int Account { get; set; }
        //public string MultiAppointmentsCounter { get; set; }
        public DateTime? SMSSendDate { get; set; }
        public string SMSSendDateString { get; set; }
        //public int? NoOfSMSSend { get; set; }
        public DateTime? SystemDate { get; set; }
        public string Response { get; set; }
        public string SystemDateFormatted { get; set; }
    }
}
