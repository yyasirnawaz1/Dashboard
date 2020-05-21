using LTCDataModel.FormEntryHome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataModel.Configurations;
using Microsoft.Extensions.Options;
using System.Runtime.InteropServices.WindowsRuntime;

namespace LTCDataManager.FormEntryHome
{
    public class gPendingSurvey
    {
        private readonly ConfigSettings _configuration;

        public gPendingSurvey(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
            Utility.Config = configuration.Value; ;
        }

        //public static List<gPendingSurveyViewModel> GetSurveys(string connectionString, int OfficeId)
        //{

        //    var q = @"SELECT 
        //             ss.SavedFormID,ss.Content,ss.SystemDate,pt.FirstName,pt.LastName 
        //             from _survey_saved ss left join patient pt on ss.PatientNumber=pt.PatientNumber where ss.SurveyProcessed=0 and ss.Office_sequence=" + OfficeId;

        //    var db = new LTCDataModel.PetaPoco.Database(connectionString, "MySql");

        //    var results = db.Fetch<gPendingSurveyModel>(q).ToList();

        //    var newResults = new List<gPendingSurveyViewModel>();

        //    foreach (var item in results)
        //    {
        //        newResults.Add(new gPendingSurveyViewModel
        //        {
        //            SavedFormID = item.SavedFormID,
        //            PatientName = item.FirstName + " " + item.LastName,
        //            Content = item.Content,
        //            SystemDate = item.SystemDate.ToString("MM/dd/yyyy")
        //        });
        //    }

        //    return newResults;
        //}

        public static List<gPendingSurveyViewModel> GetSurveys(string connectionStringDental, string connectionStringForms, int OfficeId)
        {
            var patientCommaSeparatedList = "";

            var formsQuery = $"SELECT PatientNumber, SavedFormID,Content, SystemDate from _survey_saved ss where SurveyProcessed = 0 and Office_sequence = {OfficeId}";
            var db = new LTCDataModel.PetaPoco.Database(connectionStringForms, "MySql");

            var results = db.Fetch<gPendingSurveyModel>(formsQuery).ToList();

            var newResults = new List<gPendingSurveyViewModel>();

            foreach (var item in results)
            {
                newResults.Add(new gPendingSurveyViewModel
                {
                    SavedFormID = item.SavedFormID,
                    Content = item.Content,
                    PatientNumber = item.PatientNumber,
                    SystemDate = item.SystemDate.ToString("MM/dd/yyyy")
                });
            }
            var selectedPatientList = newResults.Select(x => x.PatientNumber).Distinct();
            if (selectedPatientList.Count() > 0)
            {
                patientCommaSeparatedList = string.Join(",", selectedPatientList);
            }
            else
            {
                return newResults;
            }

            var dentalQuery = $"SELECT PatientNumber,FirstName, LastName from patient where Office_sequence = {OfficeId} AND PatientNumber in ({patientCommaSeparatedList}) ";

            var dbDental = new LTCDataModel.PetaPoco.Database(connectionStringDental, "MySql");

            var patientList = dbDental.Fetch<gPendingSurveyModel>(dentalQuery).ToList();

            foreach (var item in newResults)
            {
                var patientName = patientList.FirstOrDefault(x => x.PatientNumber == item.PatientNumber);
                if (patientName != null)
                    item.PatientName = patientName.FirstName + " " + patientName.LastName;
            }

            return newResults;
        }

    }
}
