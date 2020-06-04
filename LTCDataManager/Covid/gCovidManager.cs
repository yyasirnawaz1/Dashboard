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

namespace LTCDataManager.Covid
{
    public class gCovidManager
    {
        private readonly ConfigSettings _configuration;

        public gCovidManager(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
            Utility.Config = configuration.Value; ;
        }
        public void SaveSubscriber(gCovidSubscriber model)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid))
            {
                int fid = model.ID;
                gCovidSubscriber found = db.Fetch<gCovidSubscriber>($"select * from subscribers where ID={fid}").FirstOrDefault();
                if (found != null)
                {
                    found.FirstName = model.FirstName;
                    found.LastName = model.LastName;
                    found.EmailAddress = model.EmailAddress;
                    found.CustomID = model.CustomID;
                    found.LastSubscriptionStatusUpdated = model.LastSubscriptionStatusUpdated;
                    found.MiddleInitial = model.MiddleInitial;
                    found.BusinessInfo_ID = model.BusinessInfo_ID;
                    found.Salutation = model.Salutation;
                    found.SubscriptionStatus = model.SubscriptionStatus;

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
                    design.SubscriptionStatus = model.SubscriptionStatus;

                    db.Save(design);
                }
            }
        }
        public void DeleteSubscriber(int Id)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid))
            {
                db.Delete("subscribers", "ID", new gCovidSubscriber { ID = Id });
            }
        }
        public  static List<gCovidSubscriber> GetSubscribers()
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid);
            return db.Fetch<gCovidSubscriber>($"SELECT  * FROM subscribers ").ToList();
        }
        public void Save(gFormCovidEntry model)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid))
            {
                int fid = model.QueueID;
                gFormCovidEntry found = db.Fetch<gFormCovidEntry>($"select * from form_covid_entry where QueueID={fid}").FirstOrDefault();
                if (found != null)
                {
                    found.InPersonScreenDate = model.InPersonScreenDate;
                    found.IsInPersonScreen = model.IsInPersonScreen;
                    found.IsPreScreen = model.IsPreScreen;
                    found.PreScreenDate = model.PreScreenDate;
                    found.SubscriberID = model.SubscriberID;
                    found.StorageInJson = model.StorageInJson;
                    found.BusinessInfo_ID = model.BusinessInfo_ID;
                    found.FormID = model.FormID;

                    db.Update(found, fid);
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

                    db.Save(design);
                }
            }
        }
        public void Delete(int Id)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid))
            {
                db.Delete("form_covid_entry", "QueueID", new gFormCovidEntry {  QueueID = Id });
            }
        }

        public List<gFormCovidEntryViewModel> GetCovidForms()
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcCovid);
            return db.Fetch<gFormCovidEntryViewModel>($"SELECT  * FROM form_covid_entry Inner join subscribers on form_covid_entry.SubscriberID = subscribers.ID Inner Join form_covid_type on form_covid_entry.FormID = form_covid_type.ID ").ToList();
        }
    }
}
