using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
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
        //private readonly ConfigSettings _configuration;

        //public gUserModuleManager(IOptions<ConfigSettings> configuration)
        //{
        //    _configuration = configuration.Value;
        //    Utility.Config = configuration.Value; ;
        //}

        //public static bool GetAuthenticationModule(int office_sequence, string modulename)
        //{
        //    try
        //    {
        //        bool result = false;
        //        using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LTCSystem))
        //        {
        //            string qry = $"select a.Office_Sequence,a.IsDashboardEnabled, a.IsContactListEnabled ,a.IsOfficeManagementOk, a.IsEFormEnabled as IsESurveyEnabled,a.IsEFormEnabled,a.Newsletter as IsNewsletterEnabled,a.IsOfficePortalEnabled, a.SMS as IsSMSEnabled from businessinfo WHERE Office_Sequence = {office_sequence}";
        //            gUserModule found = db.Fetch<gUserModule>(qry).FirstOrDefault();
        //            if (found != null)
        //            {
        //                switch (modulename)
        //                {
        //                    case "dashboard":
        //                        result = found.IsDashboardEnabled == 1 ? true : false;
        //                        break;
        //                    case "officemanagement":
        //                        result = found.IsParadigmCloudEnabled == 1 ? true : false;
        //                        break;
        //                    case "survey":
        //                        result = found.IsESurveyEnabled == 1 ? true : false;
        //                        break;
        //                    case "form":
        //                        result = found.IsEFormEnabled == 1 ? true : false;
        //                        break;
        //                    case "review":
        //                        result = found.IsContactListEnabled == 1 ? true : false;
        //                        break;
        //                    case "newsletter":
        //                        result = found.IsNewsletterEnabled == 1 ? true : false;
        //                        break;
        //                    case "report":
        //                        result = found.IsOfficePortalEnabled == 1 ? true : false;
        //                        break;
        //                    case "sms":
        //                        result = found.IsSMSEnabled == 1 ? true : false;
        //                        break;
        //                    default:
        //                        break;
        //                }
        //            }
        //            else
        //            {
        //                result = false;
        //            }
        //        }

        //        return result;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }

        //}

        public static gUserModule GetAllAuthenticationModuleByOfficeSequence(int officeSequence)
        {
            gUserModule model;
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcSystem))
            {
                string qry = $"select Office_Sequence,IsDashboardEnabled, IsContactListEnabled ,IsParadigmCloudEnabled, IsSMSSurveyEnabled as IsESurveyEnabled,IsEFormEnabled,Newsletter as IsNewsletterEnabled,IsOfficePortalEnabled, SMS as IsSMSEnabled, email as IsEmailEnabled from businessInfo  WHERE Office_Sequence  = {officeSequence}";
                model = db.Fetch<gUserModule>(qry).FirstOrDefault();
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

            var authColumns = $"select Office_Sequence,isDisplaySummary from authentication Where DoctorId={UserId};";

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

    }

}
