using LTCDataModel.SMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataManager.DataAccess;
using LTCDataModel.Configurations;
using Microsoft.Extensions.Options;

namespace LTCDataManager.SMS
{
    public class gSmsManager
    {
        private readonly ConfigSettings _configuration;

        public gSmsManager(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
            Utility.Config = configuration.Value; ;
        }

        public static int DailySMSCount(string office_sequence)
        {
            // ltcdental
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcDental);
            var res = db.ExecuteScalar<int?>($"select  Sum(smssend) from appointbookingstatsbyuser where appointbookingstatsbyuser.Office_Sequence  in (" + office_sequence + ") and DATE(auditdate) = CURDATE()");
            if (res == null)
            {
                return 0;
            }
            return res.Value;
        }
        public static int DailyEmailCount(string officeSequence)
        {
            // ltcdental
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcDental);
             var res =  db.ExecuteScalar<int?>($"select Sum(emailsend) from appointbookingstatsbyuser where appointbookingstatsbyuser.Office_Sequence  in (" + officeSequence + ") and DATE(auditdate) = CURDATE()");
            if (res == null)
            {
                return 0;
            }
            return res.Value;
        }
        public static int DailyPreConfirmationCount(string officeSequence)
        {
            // ltcdental
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcDental);
            var res = db.ExecuteScalar<int?>($"select  Sum(sendcount) from appointbookingstatsbyuserdetail where appointbookingstatsbyuserdetail.Office_sequence  in (" + officeSequence + ") and DATE(auditdate) = CURDATE() and (smstype = 11 OR smstype = 12) ");
            if (res == null)
            {
                return 0;
            }
            return res.Value;
        }
        public static int DailyRecallCount(string officeSequence)
        {
            // ltcdental
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcDental);
            var res = db.ExecuteScalar<int?>($"select  Sum(sendcount) from appointbookingstatsbyuserdetail where appointbookingstatsbyuserdetail.Office_sequence  in (" + officeSequence + ") and DATE(auditdate) = CURDATE() and (smstype = 3) ");
            if (res == null)
            {
                return 0;
            }
            return res.Value;
        }
        public static List<gSMS> LoadSMSByDate(string officeSequence, DateTime startDate, DateTime endDate)
        {
            // ltcdental
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcDental);
            return db.Fetch<gSMS>($"select smssend as sendcount, auditdate from appointbookingstatsbyuser where auditdate between '{startDate.ToString("yyyy-MM-dd H:mm:ss")}' AND '{endDate.ToString("yyyy-MM-dd H:mm:ss")}' AND Office_Sequence  in (" + officeSequence + ")  Order By auditdate ").ToList();
        }
        public static List<gSMS> LoadEmailByDate(string officeSequence, DateTime startDate, DateTime endDate)
        {
            // ltcdental
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcDental);
            return db.Fetch<gSMS>($"select emailsend  as sendcount, auditdate from appointbookingstatsbyuser where  auditdate between '{startDate.ToString("yyyy-MM-dd H:mm:ss")}' AND '{endDate.ToString("yyyy-MM-dd H:mm:ss")}' AND  Office_Sequence  in (" + officeSequence + ") Order By auditdate ").ToList();
        }
        public static List<gSMS> LoadDailyRecallByDate(string officeSequence, DateTime startDate, DateTime endDate)
        {
            // ltcdental
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcDental);
            return db.Fetch<gSMS>($"select  sendcount, auditdate from appointbookingstatsbyuserdetail where (smstype = 3) and auditdate  between '{startDate.ToString("yyyy-MM-dd H:mm:ss")}' AND '{endDate.ToString("yyyy-MM-dd H:mm:ss")}' AND Office_Sequence  in (" + officeSequence + ")").ToList();
        }
        public static List<gSMS> LoadPreConfirmationByDate(string officeSequence, DateTime startDate, DateTime endDate)
        {
            // ltcdental
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcDental);
            return db.Fetch<gSMS>($"select  sendcount, auditdate from appointbookingstatsbyuserdetail where (smstype = 11 OR smstype = 12) AND auditdate between '{startDate.ToString("yyyy-MM-dd H:mm:ss")}' AND '{endDate.ToString("yyyy-MM-dd H:mm:ss")}' AND Office_Sequence  in (" + officeSequence + ")   ").ToList();
        }
        public static List<gAppointments> LoadAppointments(string officeSequence, DateTime startDate, DateTime endDate, int PageIndex=1, int PageSize=10)
        {
            // ltcdental
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcDental);
            return db.Fetch<gAppointments>($"select  _portal_patient_appointment_h.SystemDate, _portal_patient_appointment_h.Name , _portal_patient_appointment_h.Office_Sequence, _portal_patient_appointment_h.ActionType,  _portal_patient_appointment_h.AppointmentDate, _portal_patient_appointment_h.AppointmentTime, _portal_patient_appointment_h.ResponseFromPatient, _portal_patient_appointment_h.ActionDone, __sms_email_log.EMailSendDate, __sms_email_log.SMSSendDate from _portal_patient_appointment_h Inner JOin  __sms_email_log on _portal_patient_appointment_h.Office_Sequence = __sms_email_log.Office_Sequence and _portal_patient_appointment_h.AppointmentCounter = __sms_email_log.Counter   where   _portal_patient_appointment_h.SystemDate between '{startDate.ToString("yyyy-MM-dd H:mm:ss")}' AND '{endDate.ToString("yyyy-MM-dd H:mm:ss")}' AND _portal_patient_appointment_h.Office_Sequence  in (" + officeSequence + ")  union all " +
                $"select  _portal_patient_appointment.SystemDate,_portal_patient_appointment.Name , _portal_patient_appointment.Office_Sequence, _portal_patient_appointment.ActionType,  _portal_patient_appointment.AppointmentDate, _portal_patient_appointment.AppointmentTime, _portal_patient_appointment.ResponseFromPatient, _portal_patient_appointment.ActionDone, __sms_email_log.EMailSendDate, __sms_email_log.SMSSendDate from _portal_patient_appointment Inner JOin  __sms_email_log on _portal_patient_appointment.Office_Sequence = __sms_email_log.Office_Sequence and _portal_patient_appointment.AppointmentCounter = __sms_email_log.Counter  where   _portal_patient_appointment.SystemDate between '{startDate.ToString("yyyy-MM-dd H:mm:ss")}' AND '{endDate.ToString("yyyy-MM-dd H:mm:ss")}' AND _portal_patient_appointment.Office_Sequence  in (" + officeSequence + ")  LIMIT " + PageSize + " OFFSET " + PageIndex).ToList();
        }
        public static int AppointmentCount(string officeSequence, DateTime startDate, DateTime endDate)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcDental);
            int count = db.ExecuteScalar<int>($"select  Count(1) from _portal_patient_appointment_h Inner JOin  __sms_email_log on _portal_patient_appointment_h.Office_Sequence = __sms_email_log.Office_Sequence and _portal_patient_appointment_h.AppointmentCounter = __sms_email_log.Counter   where   _portal_patient_appointment_h.SystemDate between '{startDate.ToString("yyyy-MM-dd H:mm:ss")}' AND '{endDate.ToString("yyyy-MM-dd H:mm:ss")}' AND _portal_patient_appointment_h.Office_Sequence  in (" + officeSequence + ") ");
            count += db.ExecuteScalar<int>($"select  Count(1)   from _portal_patient_appointment Inner JOin  __sms_email_log on _portal_patient_appointment.Office_Sequence = __sms_email_log.Office_Sequence and _portal_patient_appointment.AppointmentCounter = __sms_email_log.Counter  where   _portal_patient_appointment.SystemDate between '{startDate.ToString("yyyy-MM-dd H:mm:ss")}' AND '{endDate.ToString("yyyy-MM-dd H:mm:ss")}' AND _portal_patient_appointment.Office_Sequence  in (" + officeSequence + ") ");
            return count;
        }

    }
}
