using LTCDataModel.Form;
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

namespace LTCDataManager.Form
{
    public class gFormManager
    {
        private readonly ConfigSettings _configuration;

        public gFormManager(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
            Utility.Config = configuration.Value; ;
        }

        public void SaveDesign(gPrivateFormModel model)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
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
                    db.Save(design);
                }


            }
        }
        public void DeleteDesign(int Id)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
            {
                db.Delete("form_private", "FormID", new gPrivateFormModel { FormID = Id });
            }
        }
        public void SavePublicDesign(gPrivateFormModel model, int officeId)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
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
                    design.IsActive = 1;
                    db.Save(design);
                }
            }
        }
        public void DeletePublicDesign(int Id)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
            {
                db.Delete("form_public", "FormID", new gPublicFormModel { FormID = Id });
            }
        }
        public void SavePublicTag(gPublicTagModel model)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
            {
                gPublicTagModel found = db.Fetch<gPublicTagModel>($"select * from form_tag_public where TagID={model.TagID}").FirstOrDefault();
                if (found != null)
                    db.Update(model, model.TagID);
                else
                    db.Save(model);
            }
        }
        public void SavePrivateTag(gFormPrivateTag model)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
            {
                gFormPrivateTag found = db.Fetch<gFormPrivateTag>($"select * from form_tag_private where TagID={model.TagID}").FirstOrDefault();
                if (found != null)
                    db.Update(model, model.TagID);
                else
                    db.Save(model);
            }
        }
        public List<gFormCategory> GetPublicCategories()
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gFormCategory>($"SELECT * FROM form_category").ToList();
        }
        public void SavePublicFormDesign(gPublicFormModel model)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
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
                    design.IsActive = 1;
                    db.Save(design);
                }


            }
        }

        public void DeletePrivateTag(int Id)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
            {
                db.Delete("form_tag_private", "TagID", new gFormPublicTag { TagID = Id });
            }
        }

        public List<gFormPrivateTagViewModel> GetPrivateTags(int officeId)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gFormPrivateTagViewModel>($"SELECT STP.*, SC.Description as CategoryDescription FROM form_tag_private STP Left Join form_category SC on STP.CategoryID = SC.CategoryID where STP.Office_Sequence = " + officeId).ToList();
        }
        public List<gFormPublicTag> GetPublicTags()
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gFormPublicTag>($"SELECT STP.*, SC.Description as CategoryDescription FROM form_tag_public STP Left Join form_category SC on STP.CategoryID = SC.CategoryID").ToList();
        }
        public void DeletePublicTags(int Id)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
            {
                db.Delete("form_tag_public", "TagID", new gFormPublicTag { TagID = Id });
            }
        }
        public List<gFormAnswerWithPrivateForm> GetFormsAnswers(string connectionString, int officeId)
        {
            //var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            //return db.Fetch<gFormAnswerWithPrivateForm>($"select SS.*, SP.Description as FormDescription from form_saved ss Left Join form_private SP on SS.FormID = SP.FormID where SS.Office_Sequence = " + officeId).ToList();

            var db = new LTCDataModel.PetaPoco.Database(connectionString, "MySql");
            return db.Fetch<gFormAnswerWithPrivateForm>($"select * from _form_saved  where Office_Sequence = " + officeId).ToList();

        }
        public string SaveFormAnswer(gFormSavedModel model)
        {
            if (!IsFormAlreadyExist(model))
            {
                var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
                model.SystemDate = DateTime.Now;
                db.Save(model);
                return "Form Saved Succefully";
            }
            return "Form Already Exists";
        }
        private bool IsFormAlreadyExist(gFormSavedModel model)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            var id = db.Fetch<int>($"SELECT SavedFormID FROM form_saved where office_sequence={model.Office_Sequence} and FormID={model.FormID} and PatientNumber={model.PatientNumber}").FirstOrDefault();
            if (id == 0)
                return false;
            return true;
        }
        public gPrivateFormModel GetFormDesign(int formid)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gPrivateFormModel>($"select * from form_private where FormID={formid}").FirstOrDefault();
        }
        public gPublicFormModel GetPublicFormDesign(int formid)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gPublicFormModel>($"select * from form_public where FormID={formid}").FirstOrDefault();
        }
        public List<gPublicFormModel> GetAllPublicForm()
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gPublicFormModel>($"select * from form_public").ToList();
        }
        public List<gPrivateFormModel> GetAllPrivateForm(int officeId)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gPrivateFormModel>($"select * from form_private where Office_Sequence=" + officeId).ToList();
            //return db.Fetch<gPrivateFormModel>($"select * from form_private").ToList();

        }
        public List<gFormReportModel> GetFormReport(int officeId)
        {
            var dbprivateSurvey = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            
            var result = dbprivateSurvey.Fetch<gPrivateFormModel>($"select * from form_private where Office_Sequence=" + officeId).ToList();

            string qry = $"select IP_Address,DB_Name,DB_Port from authentication_office_ip where Office_sequence = {officeId}";
            var detail = dbprivateSurvey.Fetch<AuthenticationOfficeIp>(qry).FirstOrDefault();
            var connectionString = "Server=" + detail.IP_Address + ";userid=ltcuser;password=" + _configuration.DatabasePassword + ";database=" + detail.DB_Name + ";Port=" + detail.DB_Port + ";Convert Zero Datetime=True;SslMode=none;Connection Timeout=190;";

            var db = new LTCDataModel.PetaPoco.Database(connectionString, "MySql");
            var formAnswers = db.Fetch<gFormReportModel>($"SELECT count(SavedFormID) 'Count', MAX(SavedFormID) 'SavedFormID' FROM _form_saved  where Office_Sequence = {officeId} group by SavedFormID").ToList();

            for (int i = 0; i < formAnswers.Count; i++)
            {
                var item = result.FirstOrDefault(x => x.FormID == formAnswers[i].SavedFormID);
                if (item != null)
                    formAnswers[i].Description = item.Description;
            }
            return formAnswers;
            //var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            //return db.Fetch<gFormReportModel>($"SELECT count(SS.FormID) Count ,SP.Description FROM form_saved SS LEFT JOIN form_private SP ON SS.FormID=SP.FormID where SS.Office_Sequence={officeId} group by SP.FormID;").ToList();
        }
    }
}
