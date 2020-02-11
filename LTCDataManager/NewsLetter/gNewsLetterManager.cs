using LTCDataModel.NewsLetter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataManager.DataAccess;
using LTCDataModel.Configurations;
using LTCDataModel.Newsletter;
using Microsoft.Extensions.Options;
using LTCDataModel.PetaPoco;

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

        public static List<ScheduledNewsletterViewModel> GetDashboard(int category, string day, int Office_Sequence)
        {

            var db = new Database(DbConfiguration.LtcNewsletter);
            return
                db.Fetch<ScheduledNewsletterViewModel>(
                    $"Select PatientCallList.ID, PatientCallList.Email, PatientCallList.EmailSentTime as ScheduledDate, PatientCallList.EmailSentTime as SentTime, templates_user.TemplateTitle as Title,PatientCallList.Status from PatientCallList inner join templates_user on PatientCallList.NewsletterID = templates_user.LetterID  where PatientCallList.Office_Sequence = {Office_Sequence} AND (PatientCallList.Status ='{category}' OR '{category}'='3') " +
            $" AND((DATE(PatientCallList.EmailSentTime) = CURDATE() and '{day}'='Today') OR(WEEKOFYEAR(PatientCallList.EmailSentTime) = WEEKOFYEAR(CURDATE()) and '{day}'='ThisWeek' ) " +
            $" OR(WEEKOFYEAR(PatientCallList.EmailSentTime) = WEEKOFYEAR(CURDATE()) - 1 and '{day}' ='LastWeek' ) OR(Month(PatientCallList.EmailSentTime) = Month(CURDATE()) and '{day}'='ThisMonth' )) Order by PatientCallList.EmailSentTime desc");
        }

        public static List<gGetPreDefinedTemplateModel> GetPreDefinedTemplates()
        {
            var db = new Database(DbConfiguration.LtcNewsletter);
            return db.Fetch<gGetPreDefinedTemplateModel>($"SELECT templates.*, templatetypes.TypeName  FROM templates  inner join templatetypes on templates.TypeID = templatetypes.TypeID  order by templates.TemplateTitle").ToList();
        }

        internal static void UpdatePatientCallList(int Id)
        {
            var db = new Database(DbConfiguration.LtcNewsletter);
            db.Execute($"Update patientcalllist Set EmailSent = 1, Status = 2 where ID = {Id}");
        }

        public static gGetUserDefinedTemplateModel GetUserDefinedTemplate(int LetterID)
        {
            var db = new Database(DbConfiguration.LtcNewsletter);
            return db.Fetch<gGetUserDefinedTemplateModel>($"SELECT templates_user.*, templatetypes.TypeName FROM templates_user inner join templatetypes on templates_user.TypeID = templatetypes.TypeID where templates_user.LetterID=" + LetterID + " order by templates_user.ModificationDate desc ").FirstOrDefault();
        }
        public static List<gGetUserDefinedTemplateModel> GetUserDefinedTemplates(int officeSequence)
        {
            var db = new Database(DbConfiguration.LtcNewsletter);
            return db.Fetch<gGetUserDefinedTemplateModel>($"SELECT templates_user.*, templatetypes.TypeName FROM templates_user inner join templatetypes on templates_user.TypeID = templatetypes.TypeID where templates_user.Office_Sequence=" + officeSequence + " order by templates_user.ModificationDate desc ").ToList();
        }
        public static List<gArticleModel> GetArticles()
        {
            var db = new Database(DbConfiguration.LtcNewsletter);
            return db.Fetch<gArticleModel>($"SELECT * FROM system_articles where Content is not null ").ToList();
        }

        public static void SaveArticle(gArticleModel model)
        {
            using (var db = new Database(DbConfiguration.LtcNewsletter))
            {

                gArticleModel found = db
                    .Fetch<gArticleModel>($"select * from system_articles where ArticleID={model.ArticleID}")
                    .FirstOrDefault();

                if (found != null)
                    db.Update(model, model.ArticleID);
                else
                    db.Save(model);
            }
        }

        public static List<gIndustryModel> GetIndustries(int officeId)
        {
            var db = new Database(DbConfiguration.LtcNewsletter);
            return db.Fetch<gIndustryModel>($"SELECT * FROM Industrytypes").ToList();
        }
        public static List<gSubIndustryModel> GetSubIndustries(int officeId)
        {
            var db = new Database(DbConfiguration.LtcNewsletter);
            return db.Fetch<gSubIndustryModel>($"SELECT * FROM Industrysubtypes").ToList();
        }
        public static List<gTemplateTypeModel> GetTemplateTypes(int officeId)
        {
            var db = new Database(DbConfiguration.LtcNewsletter);
            return db.Fetch<gTemplateTypeModel>($"SELECT * FROM Templatetypes").ToList();
        }
        public static List<gShellTemplatesModel> GetShellTemplates()
        {
            var db = new Database(DbConfiguration.LtcNewsletter);
            return db.Fetch<gShellTemplatesModel>($"SELECT * FROM templates").ToList();
        }
        public static List<gShellTemplatesModel> GetAll()
        {
            var db = new Database(DbConfiguration.LtcNewsletter);
            return db.Fetch<gShellTemplatesModel>($"SELECT * FROM templates").ToList();
        }
        #endregion

        #region Save

        public static bool CreateDefaultParadigmNewsletter(int office_sequence)
        {

            using (var db = new Database(DbConfiguration.LtcNewsletter))
            {

                var Count = db
                    .Fetch<gSaveUserTemplate>(
                        $"select * from templates_user where  Office_Sequence={office_sequence} AND IsParadigmNewsletter = 1  ")
                    .Count();

                if (Count < 1)
                {
                    // Add Paradigm Templates
                    db.Execute($"INSERT INTO `ltc_newsletter`.`templates_user` (`TemplateTitle`, `TemplateSourceMarkup`, `MainBodymarkup`, `TypeID`, `Office_Sequence`,   `IndustryID`, `ThumbnailPath`, `IndustrySubTypeID`,`IndustrySubTitleID`, `EmailType`, `EmbeddedNewsletter`, `IsParadigmNewsletter`, `IsDefault`, `ModificationDate`)\nselect TemplateTitle , TemplateSourceMarkup , MainBodymarkup , TypeID , {office_sequence}   , IndustryID , ThumbnailPath , IndustrySubTypeID , IndustrySubTitleID , EmailType , EmbeddedNewsletter , IsParadigmNewsletter, IsDefault , '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd")}'   from ltc_newsletter.templates_user where IsParadigmNewsletter = 1 and Office_Sequence = -1 ");
                }

            }
            return true;
        }

        public static bool SaveUserNewsTemplate(gSaveUserTemplate model)
        {
            //gArticleModel article = new gArticleModel();
            //article.Content = model.MainBodymarkup;
            //article.ModificationDate = DateTime.Now;
            //article.Title = model.TemplateTitle;
            //using (var db = new Database(DbConfiguration.LtcNewsletter))
            //{
            //    db.Insert(article);
            //}
            if (model.TypeID == 0)
                model.TypeID = 1;

            model.TemplateSourceMarkup = model.TemplateSourceMarkup ?? "empty";
            using (var db = new Database(DbConfiguration.LtcNewsletter))
            {
                gSaveUserTemplate found = db.Fetch<gSaveUserTemplate>($"select * from templates_user where LetterID={model.LetterID}").FirstOrDefault();
                if (!model.IsParadigmNewsletter)
                {
                    var Count = db
                        .Fetch<gSaveUserTemplate>(
                            $"select * from templates_user where TypeID={model.TypeID} AND Office_Sequence={model.Office_Sequence} AND IsParadigmNewsletter = 1  ")
                        .Count();

                    if (Count < 1)
                    {
                        model.IsParadigmNewsletter = true;
                    }

                    if (found != null)
                    {
                        if ((found.IsParadigmNewsletter && found.IsDefault) && !model.IsParadigmNewsletter)
                        {
                            return false;
                        }
                    }
                }

                if (model.IsParadigmNewsletter)
                {
                    if (!model.IsDefault)
                    {
                        var Count = db.Fetch<gSaveUserTemplate>($"select * from templates_user where TypeID={model.TypeID}  AND Office_Sequence={model.Office_Sequence}  AND IsParadigmNewsletter = 1  AND IsDefault = 1").Count();
                        if (Count < 1)
                            model.IsDefault = true;

                    }
                }


                if (model.IsDefault)
                    db.Execute($"Update templates_user Set IsDefault = 0   where TypeID = {model.TypeID} AND IsParadigmNewsletter = 1 ");

                model.ModificationDate = DateTime.Now.ToUniversalTime();

                if (found != null)
                    db.Update(model, model.LetterID);
                else
                {
                    model.TemplateSourceMarkup = model.MainBodymarkup;
                    db.Save(model);
                }
            }

            return true;
        }
        public static bool MakeDefault(int TemplateID, bool IsDefault)
        {
            using (var db = new Database(DbConfiguration.LtcNewsletter))
            {
                gSaveUserTemplate found = db.Fetch<gSaveUserTemplate>($"select * from templates_user where LetterID={TemplateID}").FirstOrDefault();
                if (found != null)
                {
                    if (found.IsDefault && !IsDefault)
                    {
                        return false;
                    }
                    else
                    {
                        db.Execute($"Update templates_user Set IsDefault = 0 WHERE TypeID = {found.TypeID} ");
                        db.Execute($"Update templates_user Set IsDefault = {IsDefault}, ModificationDate = '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd")}'  Where LetterID={TemplateID} ");
                        return true;
                    }

                }

            }

            return true;
        }

        public static void CopyArticle(int TemplateID, int ArticleId, string name, int Office_Sequence, string Content)
        {
            using (var db = new Database(DbConfiguration.LtcNewsletter))
            {
                gArticleModel art = db.Fetch<gArticleModel>($"select * from system_articles where ArticleID={ArticleId}").FirstOrDefault();

                gSavePredefinedTemplate found = db.Fetch<gSavePredefinedTemplate>($"select * from templates where TemplateID={TemplateID}").FirstOrDefault();
                if (found != null)
                {
                    //gArticleModel obj = new gArticleModel();
                    //obj.Title = name;
                    //obj.ModificationDate= DateTime.Now.ToUniversalTime();
                    //obj.Content = null;
                    //obj.ContentWithDefaultStyle = Content;

                    gSaveUserTemplate obj = new gSaveUserTemplate();
                    obj.TemplateTitle = name;
                    obj.Office_Sequence = Office_Sequence;
                    obj.EmbeddedNewsletter = string.Empty;
                    obj.MainBodymarkup = Content;
                    obj.TemplateSourceMarkup = found.TemplateSourceMarkup;
                    obj.TypeID = 8;
                    obj.ThumbnailPath = found.ThumbnailPath;
                    obj.ModificationDate = DateTime.Now.ToUniversalTime();
                    db.Save(obj);
                }
            }
        }

        public static void CopySystemTemplate(int TemplateID, string name, int Office_Sequence)
        {
            using (var db = new Database(DbConfiguration.LtcNewsletter))
            {
                gSavePredefinedTemplate found = db.Fetch<gSavePredefinedTemplate>($"select * from templates where TemplateID={TemplateID}").FirstOrDefault();
                if (found != null)
                {
                    gSaveUserTemplate obj = new gSaveUserTemplate();
                    obj.TemplateTitle = name;

                    obj.Office_Sequence = Office_Sequence;

                    //obj.IndustryID = found.IndustryID;
                    obj.MainBodymarkup = found.TemplateSourceMarkup;
                    obj.TemplateSourceMarkup = found.TemplateSourceMarkup;
                    obj.TypeID = 8;
                    obj.ThumbnailPath = found.ThumbnailPath;
                    obj.ModificationDate = DateTime.Now.ToUniversalTime();
                    db.Save(obj);
                }
            }
        }
        //select *  from ltc_newsletter.templates_user where IsParadigmNewsletter = 1 and Office_Sequence = -1


        public static void SavePreNewsTemplate(gSavePredefinedTemplate model)
        {
            model.TemplateSourceMarkup = model.TemplateSourceMarkup ?? "empty";


            using (var db = new Database(DbConfiguration.LtcNewsletter))
            {
                gSavePredefinedTemplate found = db.Fetch<gSavePredefinedTemplate>($"select * from templates where TemplateID={model.TemplateID}").FirstOrDefault();
                if (found != null)
                    db.Update(model, model.TemplateID);
                else
                    db.Save(model);
            }
        }
        public static List<gPatientCallListView> GetPatientCallListForEmail()
        {
            var db = new Database(DbConfiguration.LtcNewsletter);
            return db.Fetch<gPatientCallListView>($"Select pl.ID , pl.NewsletterID, tu.TemplateTitle,tu.TemplateSourceMarkup, tu.MainBodymarkup, pl.Account, pl.Status, pl.Email, pl.Office_Sequence, pl.PatientName from patientcalllist pl inner join templates_user tu on pl.NewsletterID = tu.LetterID where Date(pl.EmailSentTime) = curdate() AND (pl.Status in (1)) AND EmailSent = false; ").ToList();
        }

        public static List<gPatientCallListView> GetPatientCallList(int officeId)
        {
            var db = new Database(DbConfiguration.LtcNewsletter);
            return db.Fetch<gPatientCallListView>($"select pl.NewsletterID, tu.TemplateTitle, pl.Account,pl.Status, pl.EmailSentTime as AppointDate  from patientcalllist pl inner join templates_user tu on pl.NewsletterID = tu.LetterID where pl.Office_Sequence = " + officeId).ToList();
        }
        public static void SendSubscriber(gPatientCallList patientCall)
        {
            try
            {
                using (var db = new Database(DbConfiguration.LtcNewsletter))
                {
                    db.Save(patientCall);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion

        #region Delete
        public static void DeletePreDefinedTemplate(int Id)
        {
            using (var db = new Database(DbConfiguration.LtcNewsletter))
            {
                db.Delete("templates", "TemplateID", new gGetPreDefinedTemplateModel { TemplateID = Id });
            }
        }

        public static void DeleteUserDefinedTemplate(int Id)
        {
            using (var db = new Database(DbConfiguration.LtcNewsletter))
            {
                db.Delete("templates_user", "LetterID", new gGetUserDefinedTemplateModel { LetterID = Id });
            }
        }

        public static void DeleteMultiple(int[] Ids)
        {

            using (var db = new Database(DbConfiguration.LtcNewsletter))
            {
                foreach (var Id in Ids)
                {
                    db.Delete("templates_user", "LetterID", new gGetUserDefinedTemplateModel { LetterID = Id });
                }
            }
        }
        #endregion

    }
}
