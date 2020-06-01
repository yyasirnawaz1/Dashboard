using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using LTCDataManager.DataAccess;
using LTCDataModel.Configurations;
using LTCDataModel.Office;
using LTCDataModel.PetaPoco;
using LTCDataModel.User;
using Microsoft.Extensions.Options;

namespace LTCDataManager.User
{
    public class gUserModuleManager
    {
        public static List<ApplicationUser> GetAllUsers()
        {
           List<ApplicationUser> model;
            using (var db = new Database(DbConfiguration.LtcSystem))
            {
                string qry = $"select Email, Password as PasswordHash, DoctorID as Id, Office_Number, Branch_Number, Provider, Salutation, AuthenticationPhone, LastName, FirstName, Initials, Phone, Fax, AddressLine1, AddressLine2, AddressLine3, City, Province, Country, PostalCode, MondayCSV, TuesdayCSV, WednesdayCSV, ThursdayCSV, FridayCSV, SaturdayCSV, SundayCSV, LastLogin, PhotoImageURL, WebsiteURL, ActivationStatus, LanguageSelected, DateFormat, SelectedTemplateId, SelectedMainTitle_Name_ClinicName, PreferedSubIndustriesCSV, FirstNewsletterDate, NotifyAutoSchedulesBeforeDispatch, NotifyAutoSchedulesAfterDispatch, AutoNewsletterCount, DB_path, serial_number, cust_id, Providerrange, UserName, NormalizedUserName, NormalizedEmail, EmailConfirmed, SecurityStamp, ConcurrencyStamp, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount, Office_Sequence, IsSystemAdministrator, IsAdministrator, IsDisplaySummary, IsDefaultUser from authentication";
                model = db.Fetch<ApplicationUser>(qry).ToList();
            }

            return model;
        }

        public static ApplicationUser GetUserById(int Id)
        {
            ApplicationUser model;
            using (var db = new Database(DbConfiguration.LtcSystem))
            {
                string qry = $"select Email, Password as PasswordHash,SecurityStamp,ConcurrencyStamp, DoctorID as Id, Office_Number, Branch_Number, Provider, Salutation, AuthenticationPhone, LastName, FirstName, Initials, Phone, Fax, AddressLine1, AddressLine2, AddressLine3, City, Province, Country, PostalCode, MondayCSV, TuesdayCSV, WednesdayCSV, ThursdayCSV, FridayCSV, SaturdayCSV, SundayCSV, LastLogin, PhotoImageURL, WebsiteURL, ActivationStatus, LanguageSelected, DateFormat, SelectedTemplateId, SelectedMainTitle_Name_ClinicName, PreferedSubIndustriesCSV, FirstNewsletterDate, NotifyAutoSchedulesBeforeDispatch, NotifyAutoSchedulesAfterDispatch, AutoNewsletterCount, DB_path, serial_number, cust_id, Providerrange, UserName, NormalizedUserName, NormalizedEmail, EmailConfirmed, SecurityStamp, ConcurrencyStamp, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount, Office_Sequence, IsSystemAdministrator, IsAdministrator, IsDisplaySummary, IsDefaultUser from authentication WHERE DoctorID={Id}";
                model = db.Fetch<ApplicationUser>(qry).FirstOrDefault();
            }

            return model;
        }


        public static gDefaultUser GetDefaultUser(int officeSequence)
        {
            gDefaultUser model;
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcSystem))
            {
                string qry = $"select Email, Password, UserName, IsDefaultUser from authentication  WHERE Office_Sequence  = {officeSequence} AND IsDefaultUser = 1 ";
                model = db.Fetch<gDefaultUser>(qry).FirstOrDefault();
            }

            return model;
        }

        public static gBusinessInfoIp GetConnectionString(string email)
        {
            gBusinessInfoIp model;
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcSystem))
            {
                string qry = $@"Select BIP.Dental_DB_Name,  BIP.Dental_DB_IP , BIP.Dental_DB_Port,  BIP.Dental_DB_Type , BIP.Gateway_DB_Name , BIP.Gateway_DB_IP , BIP.Gateway_DB_Port,  BIP.Gateway_DB_Type , BIP.Form_DB_Name , BIP.Form_DB_IP , BIP.Form_DB_Port from authentication A  INNER JOIN businessinfo BI on A.Office_Sequence = BI.Office_Sequence INNER JOIN businessinfo_db_ip BIP ON BI.AlternateDB_ID = BIP.ID Where A.email = '{email.Replace("@","@@")}'";
                model = db.Fetch<gBusinessInfoIp>(qry).FirstOrDefault();
            }
            if (model == null)
            {
                model = new gBusinessInfoIp();
            }

            return model;
        }

        public static gUserProfile NewsletterOfficeInfo(string SyncIdentificator)
        {
            gUserProfile model;
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcSystem))
            {
                string qry = $"select * from businessInfo where SyncIdentificator = '" + SyncIdentificator + "' AND Active = 1 AND Newsletter = 1;";
                model = db.Fetch<gUserProfile>(qry).FirstOrDefault();
            }

            return model;
        }
        public static gUserModule GetAllAuthenticationModuleByOfficeSequence(int officeSequence)
        {
            gUserModule model;
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcSystem))
            {
                string qry = $"select Office_Sequence,IsDashboardEnabled, IsContactListEnabled ,IsParadigmCloudEnabled, IsSMSSurveyEnabled as IsESurveyEnabled,IsEFormEnabled,Newsletter as IsNewsletterEnabled,IsOfficePortalEnabled, SMS as IsSMSEnabled, email as IsEmailEnabled from businessInfo  WHERE Office_Sequence  = {officeSequence}";
                model = db.Fetch<gUserModule>(qry).FirstOrDefault();
            }
            if(model==null)
            {
                model = new gUserModule();
            }

            return model;
        }

        /// <summary>
        /// TODO: check if the query can be optimized. can we directly get the data based on office sequence?
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static gUserPermissionsModel GetUserPermissions(int UserId)
        {
            var query = $"select app.ActionDescription from authentication_pre_permissions app join authentication_permission ap on app.ActionID=ap.ActionID where ap.UserId={UserId};";

            var authColumns = $"select Office_Sequence,isDisplaySummary, IsDefaultUser from authentication Where DoctorId={UserId};";

            var result = new gUserPermissionsModel();

            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcSystem))
            {
                result = db.Fetch<gUserPermissionsModel>(authColumns).FirstOrDefault();
                var permissions = db.Fetch<string>(query).ToList();
                foreach (PropertyInfo propertyInfo in new gUserPermissionsModel().GetType().GetProperties())
                {
                    foreach (string r in permissions)
                        if (r == propertyInfo.Name)
                            propertyInfo.SetValue(result, true);
                }
            }
            return result;
        }

        public static gUserProfile GetUserProfile(int UserId)
        {
            var query = $"select * From Authentication Where DoctorID={UserId};";
            var result = new gUserProfile();

            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcSystem))
            {
                result = db.Fetch<gUserProfile>(query).FirstOrDefault();
            }
            return result;
        }

        public static void UpdateProfile(gUserProfile model)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcSystem))
            {
                gUserProfile newModel = new gUserProfile();
                newModel = model;
                db.Save(newModel);
            }
        }
    }

}
