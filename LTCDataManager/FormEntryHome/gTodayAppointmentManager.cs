using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataManager.DataAccess;
using LTCDataModel.Configurations;
using LTCDataModel.FormEntryHome;
using LTCDataModel.PetaPoco;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace LTCDataManager.TodayAppointment
{
    public class gTodayAppointmentManager
    {
        private readonly ConfigSettings _configuration;

        public gTodayAppointmentManager(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
            Utility.Config = configuration.Value; ;
        }

        public static List<gTodayAppointmentViewModel> GetTodayAppointment(string connectionString, int OfficeId, string FromDate = "", string ToDate = "")
        {
            var q = "";
            if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(ToDate))
                q = @"SELECT 
                     AppointCounter,Account,provider,AppointDate,TimeSLot,Name,job,DESCRIPTION,HomePhone,CellPhone,ArrivedTime,TakenInTime,patientnumber,Office_Sequence
                     FROM appoint where AppointDate BETWEEN STR_TO_DATE('" + FromDate + "','%m/%d/%Y') AND STR_TO_DATE('" + ToDate + "','%m/%d/%Y') and account >= 0  and Office_Sequence=" + OfficeId;
            else
                q = @"SELECT 
                     AppointCounter,Account,provider,AppointDate,TimeSLot,Name,job,DESCRIPTION,HomePhone,CellPhone,ArrivedTime,TakenInTime ,patientnumber,Office_Sequence
                     FROM appoint where date(AppointDate) = CURDATE() and account >= 0 and  Office_Sequence=" + OfficeId;

            var db = new Database(connectionString, "MySql");

            var results = db.Fetch<gTodayAppointment>(q).ToList();

            var newResults = new List<gTodayAppointmentViewModel>();

            foreach (var item in results)
            {
                string minuteString = "";
                string time = "";

                double hour = Math.Truncate((double)item.TimeSLot / 60);
                double minute = item.TimeSLot - 60 * hour;

                minuteString = minute < 10 ? "0" + minute.ToString() : minute.ToString();

                if (hour > 12)
                    time = hour - 12 + ":" + minuteString + " PM";
                else if (hour == 12)
                    time = hour + ":" + minuteString + " PM";
                else if (hour == 0)
                    time = "12:" + minuteString + " AM";
                else
                    time = (hour < 10 ? "0" + hour : hour.ToString()) + ":" + minuteString + " AM";

                newResults.Add(new gTodayAppointmentViewModel
                {
                    AppointCounter = item.AppointCounter,
                    Account = item.Account,
                    provider = item.provider,
                    AppointDate = item.AppointDate.ToString("MM'/'dd'/'yyyy"),
                    TimeSLot = time,
                    Name = item.Name,
                    job = item.job,
                    DESCRIPTION = item.DESCRIPTION,
                    HomePhone = item.HomePhone,
                    CellPhone = item.CellPhone,
                    ArrivedTime = item.ArrivedTime.ToString("MM'/'dd'/'yyyy"),
                    TakenInTime = item.TakenInTime.ToString("MM'/'dd'/'yyyy"),
                    PatientNumber = item.PatientNumber,
                    Office_Sequence = item.Office_Sequence
                });
            }

            return newResults;
        }
        public static int AddPortalStatus(string connectionString, int Id, int PortalAction, int Type)
        {
            var db = new Database(connectionString, "MySql");
            db.Execute("call CheckIn_Or_Arrived(" + Id + "," + PortalAction + "," + Type + ");");
            return Id;
        }
        public static bool AddSurveyQueueEntry(string connectionString, gAddQueueEntryModel model)
        {
            var db = new Database(connectionString, "MySql");
            db.Execute("call Create_SurveyQueue_Entry(" + model.AppointmentCounter + "," + model.FormID + "," + model.OfficeID + "," + model.Type + "," + model.PatientNumber + "," + model.Office_Sequence + ");");
            return true;
        }
        public static bool AddFormQueueEntry(string connectionString, gAddQueueEntryModel model)
        {
            var db = new Database(connectionString, "MySql");
            db.Execute("call Create_FormQueue_Entry(" + model.AppointmentCounter + "," + model.FormID + "," + model.OfficeID + "," + model.Type + "," + model.PatientNumber + "," + model.Office_Sequence + ");");
            return true;
        }
        public static bool SaveSurveyAndForm(gSaveSurveyAndFormModel model)
        {
            if (model.Type == 0)
            {
                //var db = new Database(connectionString, "MySql");
                //db.Execute("call Create_SurveyQueue_Entry(" + model.AppointmentCounter + "," + model.FormID + "," + model.OfficeID + "," + model.Type + "," + model.PatientNumber + "," + model.Office_Sequence + ");");

                //for sp
                //var result = db.Fetch<dynamic>(";EXEC GetPermitPendingApproval @@permitYear = @0", 2013);

                using (MySqlConnection conn = new MySqlConnection(DbConfiguration.LtcForm))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Save_Form_Data", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.Open();
                        cmd.Parameters.Add(new MySqlParameter("AC", 0));
                        cmd.Parameters.Add(new MySqlParameter("FormID", model.FormID));
                        cmd.Parameters.Add(new MySqlParameter("Content", model.Content));
                        cmd.Parameters.Add(new MySqlParameter("PatientNumber", model.PatientNumber));
                        cmd.Parameters.Add(new MySqlParameter("OfficeSequence", model.Office_Sequence));
                        cmd.Parameters.Add(new MySqlParameter("PdfFile", model.PdfContent));
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            else
            {
                using (MySqlConnection conn = new MySqlConnection(DbConfiguration.LtcForm))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Save_Survey_Data", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.Open();
                        cmd.Parameters.Add(new MySqlParameter("AC", model.AppointmentCounter));
                        cmd.Parameters.Add(new MySqlParameter("FormID", model.FormID));
                        cmd.Parameters.Add(new MySqlParameter("Content", model.Content));
                        cmd.Parameters.Add(new MySqlParameter("PatientNumber", model.PatientNumber));
                        cmd.Parameters.Add(new MySqlParameter("OfficeSequence", model.Office_Sequence));
                        cmd.Parameters.Add(new MySqlParameter("PdfFile", model.PdfContent));
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            return true;
        }
    }
}
