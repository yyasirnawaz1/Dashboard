using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LTCDataManager.DataAccess;
using LTCDataModel.Configurations;
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

        public static bool GetAuthenticationModule(int userid, string modulename)
        {
            try
            {
                bool result = false;
                using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
                {
                    string qry = $"select a.Office_Sequence,a.IsDashboardOk,a.IsOfficeManagementOk, a.IsSurveyOk,a.IsFormOk,a.IsReviewOk,a.IsNewsletterOk,a.IsReportsOk, IsSmsOk from authentication_module as a INNER JOIN authentication as b ON a.Office_Sequence = b.Office_Sequence AND b.Id = {userid}";
                    gUserModule found = db.Fetch<gUserModule>(qry).FirstOrDefault();
                    if (found != null)
                    {
                        switch (modulename)
                        {
                            case "dashboard":
                                result = found.IsDashboardOk == 1 ? true : false;
                                break;
                            case "officemanagement":
                                result = found.IsOfficeManagementOk == 1 ? true : false;
                                break;
                            case "survey":
                                result = found.IsSurveyOk == 1 ? true : false;
                                break;
                            case "form":
                                result = found.IsFormOk == 1 ? true : false;
                                break;
                            case "review":
                                result = found.IsReviewOk == 1 ? true : false;
                                break;
                            case "newsletter":
                                result = found.IsNewsletterOk == 1 ? true : false;
                                break;
                            case "report":
                                result = found.IsReportsOk == 1 ? true : false;
                                break;
                            case "sms":
                                result = found.IsSmsOk == 1 ? true : false;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        result = false;
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public static bool GetAuthenticationModule(string username, string modulename)
        {
            bool result = false;
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
            {
                string qry = $"select a.Office_Sequence,a.IsDashboardOk,a.IsOfficeManagementOk, a.IsSurveyOk,a.IsFormOk,a.IsReviewOk,a.IsNewsletterOk,a.IsReportsOk from authentication_module as a INNER JOIN authentication as b ON a.Office_Sequence = b.Office_Sequence AND b.Email = {username}";
                gUserModule found = db.Fetch<gUserModule>(qry).FirstOrDefault();
                if (found != null)
                {
                    switch (modulename)
                    {
                        case "dashboard":
                            result = found.IsDashboardOk == 1 ? true : false;
                            break;
                        case "officemanagement":
                            result = found.IsOfficeManagementOk == 1 ? true : false;
                            break;
                        case "survey":
                            result = found.IsSurveyOk == 1 ? true : false;
                            break;
                        case "form":
                            result = found.IsFormOk == 1 ? true : false;
                            break;
                        case "review":
                            result = found.IsReviewOk == 1 ? true : false;
                            break;
                        case "newsletter":
                            result = found.IsNewsletterOk == 1 ? true : false;
                            break;
                        case "report":
                            result = found.IsReportsOk == 1 ? true : false;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

        public static gUserModule GetAllAuthenticationModule(int userid)
        {
            gUserModule model;
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
            {
                string qry = $"select a.Office_Sequence,a.IsDashboardOk,a.IsOfficeManagementOk, a.IsSurveyOk,a.IsFormOk,a.IsReviewOk,a.IsNewsletterOk,a.IsReportsOk, IsSmsOk from authentication_module as a INNER JOIN authentication as b ON a.Office_Sequence = b.Office_Sequence AND b.Id = {userid}";
                model = db.Fetch<gUserModule>(qry).FirstOrDefault();
            }

            return model;
        }

        public static gUserPermissionsModel GetUserPermissions(int UserId)
        {
            var query = $"select app.ActionDescription from authentication_pre_permissions app join authentication_permission ap on app.ActionID=ap.ActionID where ap.UserId={UserId};";

            var authColumns = $"select Office_Sequence,isDisplaySummary  from authentication Where Id={UserId};";



            var result = new gUserPermissionsModel();
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
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

        //public static string GetConnectionString(int OfficeSequence)
        //{
        //    string result = "";
        //    using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway))
        //    {
        //        string qry = $"select IP_Address,DB_Name,DB_Port from authentication_office_ip where Office_sequence = {OfficeSequence}";
        //        AuthenticationOfficeIp detail = db.Fetch<AuthenticationOfficeIp>(qry).FirstOrDefault();
        //        if (detail != null)
        //        {
        //            result = "Server=" + detail.IP_Address + ";userid=ltcuser;password=" + System.Configuration.ConfigurationManager.AppSettings["databasePassword"] + ";database=" + detail.DB_Name + ";Port=" + detail.DB_Port + ";Convert Zero Datetime=True;SslMode=none;Connection Timeout=190;";
        //        }
        //        else
        //        {
        //            result = ConfigurationManager.AppSettings["FallbackConnection"];
        //        }
        //    }

        //    return result;
        //}
    }

}
