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
using LTCDataModel.Covid;
using LTCDataModel.Office;
using LTCDataModel.PetaPoco;

namespace LTCDataManager.Covid
{
    public class gCovidManager
    {
        private readonly ConfigSettings _configuration;

        public gCovidManager(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
            Utility.Config = configuration.Value;
        }
        public static gFormCovidEntryViewModel GetCovidFormByQueueId(int queueId)
        {
            var form = gCovidManager.GetCovidFormById(queueId);
            if (form == null)
                form = new gFormCovidEntryViewModel();
            if (!form.IsInPersonScreen)
                form.InPersonScreenDate = DateTime.Now;

            if (!form.IsPreScreen)
                form.PreScreenDate = DateTime.Now;



            if (form.StorageInJson != null)
                form.StorageInJsonView = Encoding.UTF8.GetString(form.StorageInJson, 0, form.StorageInJson.Length);
            return form;
        }
        public static gFormCovidEntryViewModel GetFormInfo(int subscriberId, int formId)
        {
            var form = gCovidManager.GetCovidFormBySubscriberId(subscriberId, formId);
            if (form == null)
                form = new gFormCovidEntryViewModel();
            if (!form.IsInPersonScreen)
                form.InPersonScreenDate = DateTime.Now;

            if (!form.IsPreScreen)
                form.PreScreenDate = DateTime.Now;

            form.SubscriberID = subscriberId;
            if (form.FormID < 1)
                form.FormID = formId;

            if (form.StorageInJson != null)
                form.StorageInJsonView = Encoding.UTF8.GetString(form.StorageInJson, 0, form.StorageInJson.Length);
            return form;
        }
        public static gCovidSubscriber GetByEmail(string email)
        {
            try
            {
                var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid);
                return db.Fetch<gCovidSubscriber>($"SELECT * FROM subscribers where EmailAddress = '{email.Replace("@", "@@")}' ").FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static int SaveSubscriber(gCovidSubscriber model)
        {
            int fid = model.ID;
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid))
            {
                gCovidSubscriber found = db.Fetch<gCovidSubscriber>($"select * from subscribers where ID={fid}").FirstOrDefault();
                if (found != null)
                {
                    found.FirstName = model.FirstName;
                    found.LastName = model.LastName;
                    found.EmailAddress = model.EmailAddress;
                    found.LastSubscriptionStatusUpdated = model.LastSubscriptionStatusUpdated;
                    found.MiddleInitial = model.MiddleInitial;
                    found.BusinessInfo_ID = model.BusinessInfo_ID;
                    found.Salutation = model.Salutation;
                    found.PatientNumber = model.PatientNumber;
                    found.SubscriptionStatus = model.SubscriptionStatus;
                    found.Office_Sequence = model.Office_Sequence;
                    db.Update(found, fid);
                }
                else
                {
                    //Save Form Design Object
                    gCovidSubscriber design = new gCovidSubscriber();
                    design.FirstName = model.FirstName;
                    design.LastName = model.LastName;
                    design.EmailAddress = model.EmailAddress;
                    design.CustomID = model.CustomID;
                    design.LastSubscriptionStatusUpdated = model.LastSubscriptionStatusUpdated;
                    design.MiddleInitial = model.MiddleInitial;
                    design.BusinessInfo_ID = model.BusinessInfo_ID;
                    design.Salutation = model.Salutation;
                    design.PatientNumber = model.PatientNumber;
                    design.Office_Sequence = model.Office_Sequence;
                    design.SubscriptionStatus = model.SubscriptionStatus;

                    db.Save(design);
                    fid = design.ID;
                }
            }
            return fid;
        }
        public static void DeleteSubscriber(int Id)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid))
            {
                db.Delete("subscribers", "ID", new gCovidSubscriber { ID = Id });
            }
        }
        public static gCovidSubscriber GetSubscriberById(int Id)
        {
            gCovidSubscriber res = null;
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid))
            {
                res = db.Fetch<gCovidSubscriber>($"Select * from subscribers where ID = {Id}").FirstOrDefault();
            }
            return res;
        }

        public static gCovidSubscriber GetSubscriberByCustomId(string Id)
        {
            gCovidSubscriber res = null;
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid))
            {
                res = db.Fetch<gCovidSubscriber>($"Select * from subscribers where CustomID = '{Id}'").FirstOrDefault();
            }
            return res;
        }


        public static gCovidSubscriber GetSubscriberByPatientNumberAndOfficeSequence(int patientNumber, int officesequence, string email)
        {
            gCovidSubscriber res = null;
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid))
            {
                res = db.Fetch<gCovidSubscriber>($"Select * from subscribers where PatientNumber = {patientNumber} AND Office_Sequence = {officesequence}").ToList().FirstOrDefault(x => x.EmailAddress == email);
            }
            return res;
        }

        public static int DeleteSubscriberByPatientNumberAndOfficeSequence(int patientNumber, int officesequence)
        {
            int res = 0;
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid))
            {
                res = db.Execute($"Delete from subscribers where PatientNumber = {patientNumber} AND Office_Sequence = {officesequence}");
            }
            return res;
        }

        public static List<gFormCovidType> GetAllTypes()
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid);
            return db.Fetch<gFormCovidType>($"SELECT  * FROM form_covid_type ").ToList();
        }
        public static List<gCovidSubscriber> GetSubscribers(int businessInfoID)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid);
            return db.Fetch<gCovidSubscriber>($"SELECT  * FROM subscribers where BusinessInfo_ID = {businessInfoID}").ToList();
        }
        public static int Save(gFormCovidEntry model)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid))
            {
                int fid = model.QueueID;
                gFormCovidEntry found = db.Fetch<gFormCovidEntry>($"select QueueID, CustomID,Counter, FormAction, businessInfo_id from form_covid_entry where QueueID={fid}").FirstOrDefault();
                if (found != null)
                {
                    found.InPersonScreenDate = model.InPersonScreenDate;
                    found.IsInPersonScreen = model.IsInPersonScreen;
                    found.IsPreScreen = model.IsPreScreen;
                    found.PreScreenDate = model.PreScreenDate;
                    found.SubscriberID = model.SubscriberID;
                    found.StorageInJson = model.StorageInJson;

                    if (model.BusinessInfo_ID != 0)
                        found.BusinessInfo_ID = model.BusinessInfo_ID;

                    found.FormID = model.FormID;
                    found.CustomID = found.CustomID;
                    found.IsCOVIDPossible = model.IsCOVIDPossible;


                    if (model.FormAction > 0)
                        found.FormAction = model.FormAction;

                    db.Update(found, fid);
                    return fid;

                }
                else
                {
                    //Save Form Design Object
                    gFormCovidEntry design = new gFormCovidEntry();
                    design.InPersonScreenDate = model.InPersonScreenDate;
                    design.IsInPersonScreen = model.IsInPersonScreen;
                    design.IsPreScreen = model.IsPreScreen;
                    design.PreScreenDate = model.PreScreenDate;
                    design.SubscriberID = model.SubscriberID;
                    design.StorageInJson = model.StorageInJson;
                    design.BusinessInfo_ID = model.BusinessInfo_ID;
                    design.FormID = model.FormID;
                    design.CustomID = model.CustomID;
                    design.IsCOVIDPossible = model.IsCOVIDPossible;
                    design.Counter = model.Counter;
                    if (model.FormAction > 0)
                        design.FormAction = model.FormAction;

                    var QueueID = db.Insert(design);
                    return int.Parse(QueueID.ToString());
                }
            }

        }
        public static int SavePdf(gformInPdf model)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid))
            {

                //Save Form Design Object
                var QueueID = db.Insert(model);
                return int.Parse(QueueID.ToString());

            }

        }
        public static void FormViewed(int Id)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid))
            {
                db.Execute($"Update form_covid_entry Set IsViewed = true where QueueID = {Id}" );
            }
        }
        public static void Delete(int Id)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid))
            {
                db.Delete("form_covid_entry", "QueueID", new gFormCovidEntry { QueueID = Id });
            }
        }
        public static gFormCovidEntryViewModel GetCovidFormBySubscriberId(int subscriberId, int formId)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid);
            return db.Fetch<gFormCovidEntryViewModel>($"SELECT  * FROM form_covid_entry  where form_covid_entry.SubscriberID = {subscriberId} AND form_covid_entry.FormID= {formId} ").FirstOrDefault();
        }
        public static gFormCovidEntryViewModel GetCovidFormById(int queueId)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid);
            return db.Fetch<gFormCovidEntryViewModel>($"SELECT  * FROM form_covid_entry  where form_covid_entry.QueueID = {queueId} ").FirstOrDefault();
        }

        public static gFormCovidType GetCovidFormTypeByFormAction(int formAction)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid);
            return db.Fetch<gFormCovidType>($"SELECT  * FROM form_covid_type  where FormAction = {formAction} ").FirstOrDefault();
        }

        public static gFormCovidEntryViewModel GetCovidFormByCustomId(string FormCustomId)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid);
            return db.Fetch<gFormCovidEntryViewModel>($"SELECT * FROM form_covid_entry where CustomID = '{FormCustomId}'").FirstOrDefault();
        }

        public static gFormCovidEntryViewModel GetFormEntryByCounterAndFormActionAndSubscriberCustomId(string customId, int counter, int fa)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid);
            return db.Fetch<gFormCovidEntryViewModel>($"SELECT  fc.* FROM form_covid_entry fc INNER JOIN subscribers su on fc.subscriberid=su.id where su.customid='{customId}' AND fc.counter={counter} AND FormAction = '{fa}'").FirstOrDefault();
        }

        public static gFormCovidEntryViewModel GetFormEntryByCounterAndFormActionAndSubscriberId(int subscriber, int counter, int fa)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid);
            return db.Fetch<gFormCovidEntryViewModel>($"SELECT  fc.*, su.firstname,su.lastname FROM form_covid_entry fc INNER JOIN subscribers su on fc.subscriberid=su.id where su.id='{subscriber}' AND fc.counter={counter} AND FormAction = '{fa}'").FirstOrDefault();
        }

        public static List<gFormCovidEntryViewModel> GetCovidForms(int businessInfoID)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid);
            return db.Fetch<gFormCovidEntryViewModel>($"SELECT  * FROM form_covid_entry Inner join subscribers on form_covid_entry.SubscriberID = subscribers.ID Inner Join form_covid_type on form_covid_entry.FormID = form_covid_type.ID Where subscribers.BusinessInfo_ID = {businessInfoID} ").ToList();
        }

        public static BusinessUserInfo GetUserProfile(int userId)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid);
            return db.Fetch<BusinessUserInfo>($"SELECT  * FROM businessinfo WHERE Id={userId}").FirstOrDefault();
        }

        public static int GetFirstUserIdByOffice(int officeId)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid);
            return db.Fetch<int>($"SELECT Id FROM businessinfo WHERE Office_Sequence={officeId}").FirstOrDefault();
        }

        public static BusinessUserInfo GetUserByCustomIdANDApiKey(string api, string customId)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid);
            return db.Fetch<BusinessUserInfo>($"SELECT * FROM businessinfo WHERE customId='{customId}' AND API='{api}'").FirstOrDefault();
        }


        public static void UpdateUserProfile(BusinessUserInfo model)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid);
            var sql = $"UPDATE businessinfo SET FirstName='{model.FirstName}', LastName='{model.LastName}', AddressLine1 = '{model.AddressLine1}', AddressLine2 = '{model.AddressLine2}', AddressLine3 = '{model.AddressLine3}', City='{model.City}',Province='{model.Province}',PostalCode='{model.PostalCode}',Country='{model.Country}',PhoneNumber='{model.PhoneNumber}'";
            db.Execute(sql);
        }
    }
}
