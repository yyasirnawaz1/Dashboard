using LTCDataModel.Survey;
using LTCDataModel.User;
using LTCDataModel.Survey;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataManager.DataAccess;
using LTCDataModel.Configurations;
using Microsoft.Extensions.Options;

namespace LTCDataManager.SurveyManager
{
    public class gSurveyManager
    {
        private readonly ConfigSettings _configuration;

        public gSurveyManager(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
            Utility.Config = configuration.Value; ;
        }

        public void SaveDesign(gPrivateSurveyModel model)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
            {
                int fid = model.FormID;
                gPrivateSurveyModel found = db.Fetch<gPrivateSurveyModel>($"select * from survey_form_private where FormID={fid}").FirstOrDefault();
                if (found != null)
                {
                    found.Description = model.Description;
                    found.SystemDate = DateTime.Now;
                    found.Content = model.Content;
                    found.IsActive = 1;
                    found.IsInUsed = 0;
                    db.Update(found, fid);
                }
                else
                {
                    //Save Form Design Object
                    gPrivateSurveyModel design = new gPrivateSurveyModel();
                    design.Office_Sequence = model.Office_Sequence;
                    design.Description = model.Description;
                    design.SystemDate = DateTime.Now;
                    design.Content = model.Content;
                    design.IsActive = 1;
                    design.IsInUsed = 0;
                    db.Save(design);
                }


            }
        }
        public void DeleteDesign(int Id)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
            {
                db.Delete("survey_form_private", "FormID", new gPrivateSurveyModel { FormID = Id });
            }
        }
        public void SavePublicDesign(gPrivateSurveyModel model, int officeId)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
            {
                int fid = model.FormID;
                gPublicSurveyModel found = db.Fetch<gPublicSurveyModel>($"select * from survey_form_public where FormID={fid}").FirstOrDefault();
                if (found != null)
                {
                    found.Description = model.Description;
                    found.SystemDate = DateTime.Now;
                    found.Content = model.Content;
                    found.IsActive = 1;
                    db.Update(found, fid);
                }
                else
                {
                    //Save Form Design Object
                    gPublicSurveyModel design = new gPublicSurveyModel();
                    design.Description = model.Description;
                    design.SystemDate = DateTime.Now;
                    design.Content = model.Content;
                    design.IsActive = 1;
                    db.Save(design);
                }
            }
        }
        public void DeletePublicDesign(int Id)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
            {
                db.Delete("survey_form_public", "FormID", new gPublicSurveyModel { FormID = Id });
            }
        }
        public void SavePublicTag(gPublicTagModel model)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
            {
                gPublicTagModel found = db.Fetch<gPublicTagModel>($"select * from survey_tag_public where TagID={model.TagID}").FirstOrDefault();
                if (found != null)
                    db.Update(model, model.TagID);
                else
                    db.Save(model);
            }
        }
        public void SavePrivateTag(gSurveyPrivateTag model)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
            {
                gSurveyPrivateTag found = db.Fetch<gSurveyPrivateTag>($"select * from survey_tag_private where TagID={model.TagID}").FirstOrDefault();
                if (found != null)
                    db.Update(model, model.TagID);
                else
                    db.Save(model);
            }
        }
        public List<gSurveyCategory> GetPublicCategories()
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gSurveyCategory>($"SELECT * FROM survey_category").ToList();
        }
        public void SavePublicSurveyDesign(gPublicSurveyModel model)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
            {
                int fid = model.FormID;
                gPublicSurveyModel found = db.Fetch<gPublicSurveyModel>($"select * from survey_form_public where FormID={fid}").FirstOrDefault();
                if (found != null)
                {
                    found.Description = model.Description;
                    found.SystemDate = DateTime.Now;
                    found.Content = model.Content;
                    found.IsActive = 1;
                    db.Update(found, fid);
                }
                else
                {
                    //Save Form Design Object
                    gPublicSurveyModel design = new gPublicSurveyModel();
                    design.Description = model.Description;
                    design.SystemDate = DateTime.Now;
                    design.Content = model.Content;
                    design.IsActive = 1;
                    db.Save(design);
                }


            }
        }

        public void DeletePrivateTag(int Id)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
            {
                db.Delete("survey_tag_private", "TagID", new gSurveyPublicTag { TagID = Id });
            }
        }

        public List<gSurveyPrivateTagViewModel> GetPrivateTags(int officeId)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gSurveyPrivateTagViewModel>($"SELECT STP.*, SC.Description as CategoryDescription FROM survey_tag_private STP Left Join survey_category SC on STP.CategoryID = SC.CategoryID where STP.Office_Sequence = " + officeId).ToList();
        }
        public List<gSurveyPublicTag> GetPublicTags()
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gSurveyPublicTag>($"SELECT STP.*, SC.Description as CategoryDescription FROM survey_tag_public STP Left Join survey_category SC on STP.CategoryID = SC.CategoryID").ToList();
        }
        public void DeletePublicTags(int Id)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
            {
                db.Delete("survey_tag_public", "TagID", new gSurveyPublicTag { TagID = Id });
            }
        }
        public List<gSurveyAnswerWithPrivateSurvey> GetSurveysAnswers(string connectionString, int officeId)
        {
            //var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            //return db.Fetch<gSurveyAnswerWithPrivateSurvey>($"select SS.*, SP.Description as SurveyDescription from survey_form_saved ss Left Join Survey_form_private SP on SS.FormID = SP.FormID where SS.Office_Sequence = " + officeId).ToList();

            var db = new LTCDataModel.PetaPoco.Database(connectionString, "MySql");
            return db.Fetch<gSurveyAnswerWithPrivateSurvey>($"select * from _survey_saved where Office_Sequence = " + officeId).ToList();
        }

        //need to change this to ltc-dental database as well
        public string SaveSurveyAnswer(gSurveySavedModel model)
        {
            if (!IsSurveyAlreadyExist(model))
            {
                var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
                model.SystemDate = DateTime.Now;
                db.Save(model);
                return "Survey Saved Succefully";
            }
            return "Survey Already Exists";
        }
        private bool IsSurveyAlreadyExist(gSurveySavedModel model)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            var id = db.Fetch<int>($"SELECT SavedFormID FROM survey_form_saved where office_sequence={model.Office_Sequence} and FormID={model.FormID} and PatientNumber={model.PatientNumber}").FirstOrDefault();
            if (id == 0)
                return false;
            return true;
        }
        public gPrivateSurveyModel GetSurveyDesign(int formid)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gPrivateSurveyModel>($"select * from survey_form_private where FormID={formid}").FirstOrDefault();
        }
        public gPublicSurveyModel GetPublicSurveyDesign(int formid)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gPublicSurveyModel>($"select * from survey_form_public where FormID={formid}").FirstOrDefault();
        }
        public List<gPublicSurveyModel> GetAllPublicSurvey()
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gPublicSurveyModel>($"select * from survey_form_public").ToList();
        }
        public List<gPrivateSurveyModel> GetAllPrivateSurvey(int officeId)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gPrivateSurveyModel>($"select * from survey_form_private where Office_Sequence=" + officeId).ToList();
            //return db.Fetch<gPrivateSurveyModel>($"select * from survey_form_private").ToList();

        }
        public List<gSurveyReportModel> GetSurveyReport(int officeId)
        {
            var dbprivateSurvey = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            var result = dbprivateSurvey.Fetch<gPrivateSurveyModel>($"select * from survey_form_private where Office_Sequence=" + officeId).ToList();

            string qry = $"select IP_Address,DB_Name,DB_Port from authentication_office_ip where Office_sequence = {officeId}";
            var detail = dbprivateSurvey.Fetch<AuthenticationOfficeIp>(qry).FirstOrDefault();
            
            var connectionString = "Server=" + detail.IP_Address + ";userid=ltcuser;password=" + _configuration.DatabasePassword + ";database=" + detail.DB_Name + ";Port=" + detail.DB_Port + ";Convert Zero Datetime=True;SslMode=none;Connection Timeout=190;";



            var db = new LTCDataModel.PetaPoco.Database(connectionString, "MySql");
            var surveyAnswers = db.Fetch<gSurveyReportModel>($"SELECT count(SavedFormID) 'Count', MAX(SavedFormID) 'SavedFormID' FROM _survey_saved  where Office_Sequence = {officeId} group by SavedFormID").ToList();

            for (int i = 0; i < surveyAnswers.Count; i++)
            {
                var item = result.FirstOrDefault(x => x.FormID == surveyAnswers[i].SavedFormID);
                if (item != null)
                    surveyAnswers[i].Description = item.Description;
            }
            return surveyAnswers;

            //var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            //return db.Fetch<gSurveyReportModel>($"SELECT count(SS.FormID) Count ,SP.Description FROM survey_form_saved SS LEFT JOIN survey_form_private SP ON SS.FormID=SP.FormID where SS.Office_Sequence={officeId} group by SP.FormID;").ToList();
        }
    }
}
