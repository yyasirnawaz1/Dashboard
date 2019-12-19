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
    public class gPendingForm
    {
        private readonly ConfigSettings _configuration;

        public gPendingForm(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
            Utility.Config = configuration.Value; ;
        }

        public static List<gPendingFormViewModel> GetForms(string connectionString, int OfficeId)
        {

            var q = @"SELECT 
                     ss.SavedFormID,ss.Content,ss.SystemDate,pt.FirstName,pt.LastName
                     from _form_saved ss left join patient pt on ss.PatientNumber=pt.PatientNumber where ss.FormProcessed=0 and ss.Office_sequence=" + OfficeId;

            var db = new LTCDataModel.PetaPoco.Database(connectionString, "MySql");

            var results = db.Fetch<gPendingFormModel>(q).ToList();

            var newResults = new List<gPendingFormViewModel>();

            foreach (var item in results)
            {
                newResults.Add(new gPendingFormViewModel
                {
                    SavedFormID = item.SavedFormID,
                    PatientName = item.FirstName + " " + item.LastName,
                    Content = item.Content,
                    SystemDate = item.SystemDate.ToString("MM/dd/yyyy")
                });
            }

            return newResults;
        }
    }
}
