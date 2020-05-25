 
using LTCDataModel.User;
 
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
using LTCDataModel.Form;

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

        public void SaveDesign(gPrivateFormModel model)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcForm))
            {
                int fid = model.FormID;
                gPrivateFormModel found = db.Fetch<gPrivateFormModel>($"select * from form_private where FormID={fid}").FirstOrDefault();
                if (found != null)
                {
                    found.Description = model.Description;
                    found.SystemDate = DateTime.Now;
                    found.Content = model.Content;
                    found.IsActive = 1;
                    found.IsInUsed = 0;
                    found.IsSurveyForm = 1;
                    db.Update(found, fid);
                }
                else
                {
                    //Save Form Design Object
                    gPrivateFormModel design = new gPrivateFormModel();
                    design.Office_Sequence = model.Office_Sequence;
                    design.Description = model.Description;
                    design.SystemDate = DateTime.Now;
                    design.Content = model.Content;
                    design.IsActive = 1;
                    design.IsInUsed = 0;
                    design.IsSurveyForm = 1;
                    db.Save(design);
                }


            }
        }
        public void DeleteDesign(int Id)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcForm))
            {
                db.Delete("form_private", "FormID", new gPrivateFormModel { FormID = Id });
            }
        }
        public void SavePublicDesign(gPrivateFormModel model, int officeId)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcForm))
            {
                int fid = model.FormID;
                gPublicFormModel found = db.Fetch<gPublicFormModel>($"select * from form_public where FormID={fid}").FirstOrDefault();
                if (found != null)
                {
                    found.Description = model.Description;
                    found.SystemDate = DateTime.Now;
                    found.Content = model.Content;
                    found.IsActive = 1;
                    found.IsSurveyForm = 1;
                    db.Update(found, fid);
                }
                else
                {
                    //Save Form Design Object
                    gPublicFormModel design = new gPublicFormModel();
                    design.Description = model.Description;
                    design.SystemDate = DateTime.Now;
                    design.Content = model.Content;
                    design.IsActive = 1;
                    design.IsSurveyForm = 1;
                    db.Save(design);
                }
            }
        }
        public void DeletePublicDesign(int Id)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcForm))
            {
                db.Delete("form_public", "FormID", new gPublicFormModel { FormID = Id });
            }
        }
        public void SavePublicTag(gPublicTagModel model)
        {
            model.IsSurveyForm = 1;
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcForm))
            {
                gPublicTagModel found = db.Fetch<gPublicTagModel>($"select * from form_tag_public where TagID={model.TagID} ").FirstOrDefault();
                if (found != null)
                    db.Update(model, model.TagID);
                else
                    db.Save(model);
            }
        }
        public void SavePrivateTag(gFormPrivateTag model)
        {
            model.IsSurveyForm = 1;
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcForm))
            {
                gFormPrivateTag found = db.Fetch<gFormPrivateTag>($"select * from form_tag_private where TagID={model.TagID}  AND IsSurveyForm = 1 ").FirstOrDefault();
                if (found != null)
                    db.Update(model, model.TagID);
                else
                    db.Save(model);
            }
        }
        public List<gFormCategory> GetPublicCategories()
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcForm);
            return db.Fetch<gFormCategory>($"SELECT * FROM form_category where IsSurveyForm = 1 AND IsSurveyForm = 1 ").ToList();
        }
        public void SavePublicSurveyDesign(gPublicFormModel model)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcForm))
            {
                int fid = model.FormID;
                gPublicFormModel found = db.Fetch<gPublicFormModel>($"select * from form_public where FormID={fid}").FirstOrDefault();
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
                    gPublicFormModel design = new gPublicFormModel();
                    design.Description = model.Description;
                    design.SystemDate = DateTime.Now;
                    design.Content = model.Content;
                    design.IsSurveyForm = 1;
                    design.IsActive = 1;
                    db.Save(design);
                }


            }
        }

        public void DeletePrivateTag(int Id)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcForm))
            {
                db.Delete("form_tag_private", "TagID", new gFormPublicTag { TagID = Id });
            }
        }

        public List<gFormPrivateTagViewModel> GetPrivateTags(int officeId)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcForm);
            return db.Fetch<gFormPrivateTagViewModel>($"SELECT STP.*, SC.Description as CategoryDescription FROM form_tag_private STP Left Join form_category SC on STP.CategoryID = SC.CategoryID where STP.IsSurveyForm = 1 AND STP.Office_Sequence = " + officeId).ToList();
        }
        public List<gFormPublicTag> GetPublicTags()
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcForm);
            return db.Fetch<gFormPublicTag>($"SELECT STP.*, SC.Description as CategoryDescription FROM form_tag_public STP Left Join form_category SC on STP.CategoryID = SC.CategoryID where  STP.IsSurveyForm = 1 ").ToList();
        }

        public List<gFormPublicTag> GetPublicTags(string connectionString)
        {
            var db = new LTCDataModel.PetaPoco.Database(connectionString);
            return db.Fetch<gFormPublicTag>($"SELECT STP.*, SC.Description as CategoryDescription FROM form_tag_public STP Left Join form_category SC on STP.CategoryID = SC.CategoryID where  STP.IsSurveyForm = 1 ").ToList();
        }

        public void DeletePublicTags(int Id)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcForm))
            {
                db.Delete("form_tag_public", "TagID", new gFormPublicTag { TagID = Id });
            }
        }
        public List<gSurveyAnswerWithPrivateSurvey> GetSurveysAnswers(int officeId)
        {

            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcForm);
            return db.Fetch<gSurveyAnswerWithPrivateSurvey>($"select * from _form_saved where IsSurveyForm=1 AND Office_Sequence = " + officeId).ToList();
        }

        //need to change this to ltc-dental database as well
        public string SaveSurveyAnswer(gFormSavedModel model)
        {
            if (!IsSurveyAlreadyExist(model))
            {
                var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcForm);
                model.SystemDate = DateTime.Now;
                db.Save(model);
                return "Survey Saved Succefully";
            }
            return "Survey Already Exists";
        }
        private bool IsSurveyAlreadyExist(gFormSavedModel model)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcForm);
            var id = db.Fetch<int>($"SELECT SavedFormID FROM _form_saved where IsSurveyForm = 1 AND office_sequence={model.Office_Sequence} and FormID={model.FormID} and PatientNumber={model.PatientNumber}").FirstOrDefault();
            if (id == 0)
                return false;
            return true;
        }
        public gPrivateFormModel GetSurveyDesign(int formid)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcForm);
            return db.Fetch<gPrivateFormModel>($"select * from form_private where FormID={formid}").FirstOrDefault();
        }

        public gPrivateFormModel GetSurveyDesign(int formid,string connectionString)
        {
            var db = new LTCDataModel.PetaPoco.Database(connectionString);
            return db.Fetch<gPrivateFormModel>($"select * from form_private where FormID={formid}").FirstOrDefault();
        }

        public gPublicFormModel GetPublicSurveyDesign(int formid)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcForm);
            return db.Fetch<gPublicFormModel>($"select * from form_public where FormID={formid}").FirstOrDefault();
        }
        public List<gPublicFormModel> GetAllPublicSurvey()
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcForm);
            return db.Fetch<gPublicFormModel>($"select * from form_public where IsSurveyForm = 1").ToList();
        }
        public List<gPrivateFormModel> GetAllPrivateSurvey(int officeId)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcForm);
            return db.Fetch<gPrivateFormModel>($"select * from form_private where  IsSurveyForm = 1 AND Office_Sequence=" + officeId).ToList();
        }

        public List<gPrivateFormModel> GetAllPrivateSurvey(int officeId,string connectionString)
        {
            var db = new LTCDataModel.PetaPoco.Database(connectionString);
            return db.Fetch<gPrivateFormModel>($"select * from form_private where  IsSurveyForm = 1 AND Office_Sequence=" + officeId).ToList();
        }


        public List<gFormReportModel> GetSurveyReport(int officeId)
        {
            var dbprivateSurvey = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcForm);
            var result = dbprivateSurvey.Fetch<gFormReportModel>($"select * from form_private where IsSurveyForm = 1 AND Office_Sequence=" + officeId).ToList();

            //string qry = $"select IP_Address,DB_Name,DB_Port from authentication_office_ip where Office_sequence = {officeId}";
            //var detail = dbprivateSurvey.Fetch<AuthenticationOfficeIp>(qry).FirstOrDefault();
            
            //var connectionString = "Server=" + detail.IP_Address + ";userid=ltcuser;password=" + _configuration.DatabasePassword + ";database=" + detail.DB_Name + ";Port=" + detail.DB_Port + ";Convert Zero Datetime=True;SslMode=none;Connection Timeout=190;";



            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcForm);
            var surveyAnswers = db.Fetch<gFormReportModel>($"SELECT count(SavedFormID) 'Count', MAX(SavedFormID) 'SavedFormID' FROM _form_saved  where IsSurveyForm=1 AND Office_Sequence = {officeId} group by SavedFormID").ToList();

            for (int i = 0; i < surveyAnswers.Count; i++)
            {
                var item = result.FirstOrDefault(x => x.SavedFormID == surveyAnswers[i].SavedFormID);
                if (item != null)
                    surveyAnswers[i].Description = item.Description;
            }
            return surveyAnswers;

        }
    }
}
