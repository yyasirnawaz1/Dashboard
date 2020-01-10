using LTCDataModel.NewsLetter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataManager.DataAccess;
using LTCDataModel.Configurations;
using Microsoft.Extensions.Options;

namespace LTCDataManager.NewsLetter
{
    public class gNewsLetterManager
    {
        private readonly ConfigSettings _configuration;

        public gNewsLetterManager(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
            Utility.Config = configuration.Value; ;
        }

        #region Get
        public static List<gGetPreDefinedTemplateModel> GetPreDefinedTemplates()
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gGetPreDefinedTemplateModel>($"SELECT * FROM newsletter_systemtemplate").ToList();
            //return db.Fetch<gGetPreDefinedTemplateModel>($"SELECT ST.*, SS.Markup as ShellTemplate FROM newsletter_systemtemplate ST Left JOIN newsletter_ShellTemplates SS on ST.ShellTemplateID = SS.ID").ToList();
        }
        public static List<gGetUserDefinedTemplateModel> GetUserDefinedTemplates(int officeId)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gGetUserDefinedTemplateModel>($"SELECT * FROM newsletter_usertemplate where Office_Sequence=" + officeId).ToList();
        }
        public static List<gIndustryModel> GetIndustries(int officeId)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gIndustryModel>($"SELECT * FROM newsletter_industry").ToList();
        }
        public static List<gSubIndustryModel> GetSubIndustries(int officeId)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gSubIndustryModel>($"SELECT * FROM newsletter_subindustry").ToList();
        }
        public static List<gTemplateTypeModel> GetTemplateTypes(int officeId)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gTemplateTypeModel>($"SELECT * FROM newsletter_templatetype").ToList();
        }
        public static List<gShellTemplatesModel> GetShellTemplates()
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gShellTemplatesModel>($"SELECT * FROM newsletter_shelltemplates").ToList();
        }
        #endregion

        #region Save
        public static void SaveUserNewsTemplate(gSaveUserTemplate model)
        {
            model.Markup = model.Markup ?? "empty";
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
            {
                gSaveUserTemplate found = db.Fetch<gSaveUserTemplate>($"select * from newsletter_usertemplate where ID={model.ID}").FirstOrDefault();
                if (found != null)
                    db.Update(model, model.ID);
                else
                    db.Save(model);
            }
        }

        public static void SavePreNewsTemplate(gSavePredefinedTemplate model)
        {
            model.Markup = model.Markup ?? "empty";
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
            {
                gSavePredefinedTemplate found = db.Fetch<gSavePredefinedTemplate>($"select * from newsletter_systemtemplate where ID={model.ID}").FirstOrDefault();
                if (found != null)
                    db.Update(model, model.ID);
                else
                    db.Save(model);
            }
        }

        #endregion

        #region Delete
        public static void DeletePreDefinedTemplate(int Id)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
            {
                db.Delete("newsletter_systemtemplate", "ID", new gSavePredefinedTemplate { ID = Id });
            }
        }

        public static void DeleteUserDefinedTemplate(int Id)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
            {
                db.Delete("newsletter_usertemplate", "ID", new gSaveUserTemplate { ID = Id });
            }
        }
        #endregion
    }
}
