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
                    $"Select PatientCallList.ID, PatientCallList.Email, PatientCallList.DateToSendEmail as ScheduledDate, PatientCallList.DateToSendEmail as SentTime, templates_user.TemplateTitle as Title,PatientCallList.Status from PatientCallList inner join templates_user on PatientCallList.NewsletterID = templates_user.LetterID  where PatientCallList.Office_Sequence = {Office_Sequence} AND (PatientCallList.Status ='{category}' OR '{category}'='3') " +
            $" AND((DATE(PatientCallList.DateToSendEmail) = CURDATE() and '{day}'='Today') OR(WEEKOFYEAR(PatientCallList.DateToSendEmail) = WEEKOFYEAR(CURDATE()) and '{day}'='ThisWeek' ) " +
            $" OR(WEEKOFYEAR(PatientCallList.DateToSendEmail) = WEEKOFYEAR(CURDAT" +
            $"E()) - 1 and '{day}' ='LastWeek' ) OR(Month(PatientCallList.DateToSendEmail) = Month(CURDATE()) and '{day}'='ThisMonth' )) Order by PatientCallList.DateToSendEmail desc");
        }

        public static List<gGetPreDefinedTemplateModel> GetPreDefinedTemplates()
        {
            var db = new Database(DbConfiguration.LtcNewsletter);
            return db.Fetch<gGetPreDefinedTemplateModel>($"SELECT templates.*, templatetypes.TypeName  FROM templates  inner join templatetypes on templates.TypeID = templatetypes.TypeID  order by templates.TemplateTitle").ToList();
        }

        internal static void UpdatePatientCallList(int Id, string msgId)
        {
            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, easternZone);
            var db = new Database(DbConfiguration.LtcNewsletter);
            db.Execute($"Update patientcalllist Set EmailSent = 1, Status = 1 , EmailSentOnDate ='{now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm")}', MessageID ='{msgId}'   where ID = {Id}");
        }
        internal static void UpdatePatientCallListNotSent(int Id, int NoOfRetry)
        {
            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, easternZone);
            var db = new Database(DbConfiguration.LtcNewsletter);
            db.Execute($"Update patientcalllist Set EmailSent = 0, Status = 3, NoOfRetry = {NoOfRetry} where ID = {Id}");
        }
        public static gGetUserDefinedTemplateModel GetUserDefinedTemplate(int LetterID)
        {
            var db = new Database(DbConfiguration.LtcNewsletter);
            return db.Fetch<gGetUserDefinedTemplateModel>($"SELECT templates_user.*, templatetypes.TypeName FROM templates_user inner join templatetypes on templates_user.TypeID = templatetypes.TypeID where templates_user.LetterID=" + LetterID + " order by templates_user.ModificationDate desc ").FirstOrDefault();
        }
        public static List<gGetUserDefinedTemplateModel> GetUserDefinedTemplates(int officeSequence)
        {
            var db = new Database(DbConfiguration.LtcNewsletter);
            return db.Fetch<gGetUserDefinedTemplateModel>($"SELECT templates_user.CategoryID, templates_user.LetterID, templates_user.TemplateTitle, templates_user.TemplateSourceMarkup, templates_user.MainBodymarkup, templates_user.TypeID, templates_user.IsParadigmNewsletter, templates_user.IsDefault, templates_user.ModificationDate, templatetypes.TypeName FROM templates_user inner join templatetypes on templates_user.TypeID = templatetypes.TypeID  where templates_user.Office_Sequence=" + officeSequence + " order by templates_user.ModificationDate desc ").ToList(); // 
        }
        public static List<gArticleViewModel> GetArticles()
        {
            var db = new Database(DbConfiguration.LtcNewsletter);
            return db.Fetch<gArticleViewModel>($" SELECT ArticleID, Title, Content, ContentWithDefaultStyle, ModificationDate , system_articles.CategoryID, articlecategories.CategoryName  FROM system_articles Inner join articlecategories on system_articles.CategoryID = articlecategories.CategoryID  where Content is not null ").ToList();
        }
        public static gArticleModel GetArticle(int articleId)
        {
            var db = new Database(DbConfiguration.LtcNewsletter);
            return db.Fetch<gArticleModel>($"SELECT ArticleID, Title, Content, ContentWithDefaultStyle, ModificationDate FROM system_articles WHERE  ArticleID={articleId}").FirstOrDefault();
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
                //else
                // db.Save(model);
            }
        }
        public static void UpdateArticle(gArticleModelTest model)
        {
            using (var db = new Database(DbConfiguration.LtcNewsletter))
            {

                gArticleModel found = db
                    .Fetch<gArticleModel>($"select * from system_articles where ArticleID={model.ArticleID}")
                    .FirstOrDefault();
                byte[] byteArray = Convert.FromBase64String(model.ContentImage);

                found.ContentImage = byteArray;
                if (found != null)
                    db.Update(found, model.ArticleID);
                //else
                // db.Save(model);
            }
        }
        public static void UpdateLetter(gLetterModelTest model)
        {
            using (var db = new Database(DbConfiguration.LtcNewsletter))
            {

                gSaveUserTemplate found = db
                    .Fetch<gSaveUserTemplate>($"select * from templates_user where LetterID={model.LetterID}")
                    .FirstOrDefault();
                byte[] byteArray = Convert.FromBase64String(model.ContentImage);
                found.ContentImage = byteArray;
                if (found != null)
                    db.Update(found, model.LetterID);
                //else
                // db.Save(model);
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
        public static List<gArticleCategories> GetArticleCategories()
        {
            var db = new Database(DbConfiguration.LtcNewsletter);
            return db.Fetch<gArticleCategories>($"SELECT * FROM articlecategories").ToList();
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
                            $"select LetterID from templates_user where TypeID={model.TypeID} AND Office_Sequence={model.Office_Sequence} AND IsParadigmNewsletter = 1  ")
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
                        var Count = db.Fetch<gSaveUserTemplate>($"select LetterID from templates_user where TypeID={model.TypeID}  AND Office_Sequence={model.Office_Sequence}  AND IsParadigmNewsletter = 1  AND IsDefault = 1").Count();
                        if (Count < 1)
                            model.IsDefault = true;

                    }
                }


                if (model.IsDefault)
                    db.Execute($"Update templates_user Set IsDefault = 0   where TypeID = {model.TypeID} AND IsParadigmNewsletter = 1  AND Office_Sequence = {found.Office_Sequence} ");

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
                        db.Execute($"Update templates_user Set IsDefault = 0 WHERE TypeID = {found.TypeID}  AND Office_Sequence = {found.Office_Sequence} ");
                        db.Execute($"Update templates_user Set IsDefault = {IsDefault}, ModificationDate = '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd")}'  Where LetterID={TemplateID} ");
                        return true;
                    }

                }

            }

            return true;
        }

        public static void CopyArticle(int TemplateID, int ArticleId, string name, int Office_Sequence, string Content, byte[] ContentImage)
        {
            using (var db = new Database(DbConfiguration.LtcNewsletter))
            {
                var categoryId = 1;
                gArticleModel art = db.Fetch<gArticleModel>($"select * from system_articles where ArticleID={ArticleId}").FirstOrDefault();
                if (art != null)
                {
                    categoryId = art.CategoryID;
                }
                gSavePredefinedTemplate found = db.Fetch<gSavePredefinedTemplate>($"select * from templates where TemplateID={TemplateID}").FirstOrDefault();
                if (found != null)
                {
                    
                    gSaveUserTemplate obj = new gSaveUserTemplate();
                    obj.TemplateTitle = name;
                    obj.Office_Sequence = Office_Sequence;
                    obj.EmbeddedNewsletter = string.Empty;
                    obj.MainBodymarkup = Content;
                    obj.TemplateSourceMarkup = found.TemplateSourceMarkup;
                    obj.TypeID = 8;
                    obj.ThumbnailPath = found.ThumbnailPath;
                    obj.ContentImage = ContentImage;
                    obj.ModificationDate = DateTime.Now.ToUniversalTime();
                    obj.CategoryID = categoryId;
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
                    //obj.ContentImage = found
                    //obj.IndustryID = found.IndustryID;
                    obj.MainBodymarkup = found.TemplateSourceMarkup;
                    obj.TemplateSourceMarkup = found.TemplateSourceMarkup;
                    obj.TypeID = 8;
                    obj.ThumbnailPath = found.ThumbnailPath;
                    obj.ModificationDate = DateTime.Now.ToUniversalTime();
                    obj.CategoryID = 1;
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
            return db.Fetch<gPatientCallListView>($"Select pl.NoOfRetry, pl.ID , pl.NewsletterID, tu.TemplateTitle,tu.TemplateSourceMarkup, tu.MainBodymarkup, pl.Account, pl.Status, pl.Email, pl.Office_Sequence, pl.PatientName, pl.DateToSendEmail from patientcalllist pl inner join templates_user tu on pl.NewsletterID = tu.LetterID where Date(pl.DateToSendEmail) = curdate() AND (pl.Status in (1)) AND EmailSent = false; ").ToList();
        }
        public static List<gPatientCallListView> GetPatientCallList()
        {
            var db = new Database(DbConfiguration.LtcNewsletter);
            return db.Fetch<gPatientCallListView>($" Select pl.NoOfRetry, pl.ID , pl.NewsletterID, pl.EmailSubject ,pl.EmailContent, pl.Account, pl.Status, pl.Email, pl.Office_Sequence, pl.PatientName, pl.DateToSendEmail, pl.PublicNewsletter from patientcalllist pl  where Date(pl.DateToSendEmail) <= curdate() AND ((pl.Status in (1)) || (pl.Status in (3) AND (pl.NoOfRetry < 4 || pl.NoOfRetry IS NULL)))   AND EmailSent = false; ").ToList();
        }
        public static List<gPatientCallListView> GetPatientCallList(int officeId)
        {
            var db = new Database(DbConfiguration.LtcNewsletter);
            return db.Fetch<gPatientCallListView>($"select pl.NoOfRetry, pl.NewsletterID, tu.TemplateTitle, pl.Account,pl.Status, pl.DateToSendEmail as AppointDate , pl.DateToSendEmail from patientcalllist pl inner join templates_user tu on pl.NewsletterID = tu.LetterID where pl.Office_Sequence = " + officeId).ToList();
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
        public static void MoveArticle(int CategoryId, int ArticleId)
        {
            using (var db = new Database(DbConfiguration.LtcNewsletter))
            {
                db.Execute($"Update system_articles Set CategoryID = {CategoryId} WHERE ArticleID = {ArticleId} ");
            }
        }

        public static void DeleteUserDefinedTemplate(int Id)
        {
            using (var db = new Database(DbConfiguration.LtcNewsletter))
            {
                gSaveUserTemplate found = db.Fetch<gSaveUserTemplate>($"select * from templates_user where LetterID={Id}").FirstOrDefault();
                if (found != null)
                {
                    if (found.IsDefault)
                    {
                        gSaveUserTemplate firstSystemTemplate = db.Fetch<gSaveUserTemplate>($"select * from templates_user where  TypeID ={found.TypeID} AND Office_Sequence = {found.Office_Sequence}   AND IsParadigmNewsletter = 1  limit 1").FirstOrDefault();
                        if (firstSystemTemplate != null)
                        {
                            db.Execute($"Update templates_user Set IsDefault = 1 WHERE LetterID = {firstSystemTemplate.LetterID}  AND Office_Sequence = {found.Office_Sequence} ");
                        }

                    }
                }
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
