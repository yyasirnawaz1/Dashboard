using LTCDataModel.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataManager.DataAccess;
using LTCDataModel.Configurations;
using Microsoft.Extensions.Options;

namespace LTCDataManager.Review
{
    public class gReviewManager
    {
        private readonly ConfigSettings _configuration;

        public gReviewManager(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
            Utility.Config = configuration.Value; ;
        }

        public static List<gReviewOfficeSetting> GetAllOfficeReviewLinks(int office_sequence)
        {
            //  string constr = string.Format(ConString, "ltc_gateway");
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gReviewOfficeSetting>(@"select review_office_settings.Office_Sequence , review_office_settings.Review_Link_Type, review_link_source.Review_Link_Name   FROM review_office_settings Inner JOIN review_link_source on 
review_office_settings.Review_Link_Type = review_link_source.Review_Link_Type
where  Office_Sequence = " + office_sequence);
        }
        public static List<gReviewPatientResponse> LoadReviews(int office_sequence, DateTime startDate, DateTime endDate)
        {
            // ltcdental
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcDental);
            return db.Fetch<gReviewPatientResponse>($"SELECT _review_patient_response.*, patient.lastname, patient.firstname FROM _review_patient_response  Left Outer join patient on _review_patient_response.Patient_number = patient.patientnumber where _review_patient_response.ReviewType <> 'LO' AND _review_patient_response.Office_Sequence = {office_sequence} AND  ReviewDate between '{startDate.ToString("yyyy-MM-dd H:mm:ss")}' AND '{endDate.ToString("yyyy-MM-dd H:mm:ss")}' Order By ReviewDate ").ToList();
        }
        public static int Count(int office_sequence, string ReviewType, DateTime startDate, DateTime endDate)
        {
            // ltcdental
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcDental);
            return db.ExecuteScalar<int>($"SELECT Count(ReviewID) FROM _review_patient_response  where _review_patient_response.Office_Sequence = {office_sequence} And ReviewType = '{ReviewType}' AND  ReviewDate between '{startDate.ToString("yyyy-MM-dd H:mm:ss")}' AND '{endDate.ToString("yyyy-MM-dd H:mm:ss")}'");
        }
        public static int CountBeforeDate(int office_sequence, string ReviewType, DateTime startDate)
        {
            // ltcdental
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcDental);
            return db.ExecuteScalar<int>($"SELECT Count(ReviewID) FROM _review_patient_response  where _review_patient_response.Office_Sequence = {office_sequence} And ReviewType = '{ReviewType}' AND (Month(ReviewDate) < Month('{startDate.ToString("yyyy-MM-dd H:mm:ss")}') AND Month(ReviewDate) < Month('{startDate.ToString("yyyy-MM-dd H:mm:ss")}') AND Year(ReviewDate) AND Month('{startDate.ToString("yyyy-MM-dd H:mm:ss")}'))");
        }
        public static int AverageCountBeforeDate(int office_sequence,DateTime startDate)
        {
            // ltcdental
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcDental);
            return db.ExecuteScalar<int>($"SELECT Count(ReviewID) FROM _review_patient_response  where _review_patient_response.Office_Sequence = {office_sequence} And Rating > 0  AND (Month(ReviewDate) < Month('{startDate.ToString("yyyy-MM-dd H:mm:ss")}') AND Month(ReviewDate) < Month('{startDate.ToString("yyyy-MM-dd H:mm:ss")}') AND Year(ReviewDate) AND Month('{startDate.ToString("yyyy-MM-dd H:mm:ss")}'))");
        }
    }
}
