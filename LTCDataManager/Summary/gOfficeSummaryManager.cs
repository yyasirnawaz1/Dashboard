using LTCDataModel.Summary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataManager.DataAccess;
using LTCDataModel.Configurations;
using Microsoft.Extensions.Options;

namespace LTCDataManager.Summary
{
    public class gOfficeSummaryManager
    {
        private readonly ConfigSettings _configuration;

        public gOfficeSummaryManager(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
            Utility.Config = configuration.Value; ;
        }

        public gOfficeSummary GetOfficeSummary(int officeId)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcDental))
            {
                var result = db
                    .Fetch<gOfficeSummary>(
                        $"Select * from _portal_office_summary  where Office_Sequence = {officeId} and SummaryPeriod = 'CD' and CalculationDate = current_date()")
                    .FirstOrDefault();
                if (result != null)
                {
                    if (result.TotalCharge != null)
                        result.TotalCharge = result.TotalCharge / 100;

                    if (result.TotalPayment != null)
                        result.TotalPayment = result.TotalPayment / 100;

                }

                return result;
            }
        }
    }
}
