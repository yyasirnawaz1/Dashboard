using LTCDataModel.Office;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataManager.DataAccess;
using LTCDataModel.Configurations;
using Microsoft.Extensions.Options;

namespace LTCDataManager
{
    public class Utility
    {
        public static ConfigSettings Config;

        public static List<gDentalProvider> ProviderToList(string[] model)
        {
            List<gDentalProvider> result = new List<gDentalProvider>();
            foreach (var item in model)
            {
                var data = item.Split('_');
                if (data.Length == 2)
                {
                    result.Add(new gDentalProvider
                    {
                        Office_Sequence = Convert.ToInt32(data[0]),
                        Provider = data[1]
                    });
                }
            }
            return result;
        }

        public static string FilterProviderToString(int officeSequence, List<gDentalProvider> model)
        {
            var result = "";

            var officeRecords = model.Where(x => x.Office_Sequence == officeSequence);
            foreach (var item in officeRecords)
            {
                result += "\'" + item.Provider + "\'" + ",";
            }

            result = result.Trim(',');
            return result;
        }

        //public static string ProviderToString(string[] providerList)
        //{
        //    var result = "";
        //    foreach (var item in providerList)
        //    {
        //        result += "\'" + item + "\'" + ",";
        //    }
        //    result = result.Trim(',');
        //    return result;
        //}

        /// <summary>
        /// This method should cache the authentication_office_list table detail rather than hitting the database on every call, 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<gOfficelist> GetAllowedOffices(int userId)
        {
            try
            {
                var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
                var allowedOffices = db.Fetch<gOfficelist>($"SELECT AOL.Office_Sequence, AOL.Providerrange,AOI.IP_Address, AOI.DB_Name,AOI.DB_Port FROM authentication_office_list AOL LEFT JOIN authentication_office_ip AOI ON AOL.office_sequence = AOI.office_sequence WHERE AOL.UserID ={userId}").ToList();
                return allowedOffices;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static string GetConnectionStringByOfficeId(int officeSequence)
        {
            try
            {
                ////TODO: Start Replace this code with [GetAllowedOffices] method as that will get the records from cache in future. 
                //Start 
                var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
                var connectionString = db.Fetch<gOfficelist>($"SELECT * FROM authentication_office_ip WHERE Office_sequence = {officeSequence}").FirstOrDefault();
                //END
                if (connectionString != null)
                {
                    return CreateConnectionString(connectionString);
                }
                else
                {
                    return Config.FallbackConnection;//  ConfigurationManager.AppSettings["FallbackConnection"];
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static string CreateConnectionString(gOfficelist model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.IP_Address))
                {
                    //return $"Server={model.IP_Address};userid=ltcuser;password={System.Configuration.ConfigurationManager.AppSettings["databasePassword"]};database={model.DB_Name};Port={model.DB_Port};Convert Zero Datetime=True;SslMode=none;Connection Timeout=190;";
                    return $"Server={model.IP_Address};userid=ltcuser;password={Config.DatabasePassword};database={model.DB_Name};Port={model.DB_Port};Convert Zero Datetime=True;SslMode=none;Connection Timeout=190;";

                }
                else
                {
                    return Config.FallbackConnection;// ConfigurationManager.AppSettings["FallbackConnection"];
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void RemoveDuplicates(ref List<gDentalProvider> listWithoutDuplicate, List<gDentalProvider> listWithDuplicate)
        {
            try
            {
                foreach (var singleProvider in listWithDuplicate)
                {
                    if (listWithoutDuplicate.Count(x => x.Office_Sequence == singleProvider.Office_Sequence && x.Provider == singleProvider.Provider) == 0)
                    {
                        listWithoutDuplicate.Add(singleProvider);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static string HygenistType(string chartName)
        {
            var setting = (string)Config.GetType().GetProperty(chartName).GetValue(Config, null);  //ConfigurationManager.AppSettings[chartName];
            if (setting == null)
            {
                return "0,1";
            }
            else if (setting == "*")
            {
                return "0,1";
            }
            else if (setting.ToLower() == "d")
            {
                return "0";
            }
            else if (setting.ToLower() == "h")
            {
                return "1";
            }
            else
            {
                return "0,1";
            }
        }

    }
}
