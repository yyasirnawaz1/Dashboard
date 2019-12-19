using LTCDataModel.FormEntryHome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataModel.Configurations;
using Microsoft.Extensions.Options;

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

        public static List<gPendingSurveyViewModel> GetSurveys(string connectionString, int OfficeId)
        {

            var q = @"SELECT 
                     ss.SavedFormID,ss.Content,ss.SystemDate,pt.FirstName,pt.LastName 
                     from _survey_saved ss left join patient pt on ss.PatientNumber=pt.PatientNumber where ss.SurveyProcessed=0 and ss.Office_sequence=" + OfficeId;

            var db = new LTCDataModel.PetaPoco.Database(connectionString, "MySql");

            var results = db.Fetch<gPendingSurveyModel>(q).ToList();

            var newResults = new List<gPendingSurveyViewModel>();

            foreach (var item in results)
            {
                newResults.Add(new gPendingSurveyViewModel
                {
                    SavedFormID=item.SavedFormID,
                    PatientName = item.FirstName+" "+item.LastName,
                    Content = item.Content,
                    SystemDate = item.SystemDate.ToString("MM/dd/yyyy")
                });
            }

            return newResults;
        }
    }
}
