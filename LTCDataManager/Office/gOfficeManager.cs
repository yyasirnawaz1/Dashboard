using LTCDataModel.Office;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataManager.DataAccess;
using LTCDataModel.Configurations;
using Microsoft.Extensions.Options;

namespace LTCDataManager.Office
{
    public class gOfficeManager
    {
        private readonly ConfigSettings _configuration;

        public gOfficeManager(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
            Utility.Config = configuration.Value; ;
        }

        public static List<gBusinesInfo> GetOfficeDetailByUserId(int userId)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gBusinesInfo>($"select a.Office_Sequence as Id,b.Business_Name as ClinicName from authentication_office_list a INNER JOIN authentication_BusinessInfo b ON a.Office_Sequence=b.Office_Sequence where a.UserId={userId} ").ToList();
        }
        public static gBusinesInfo GetOfficeDetailByOfficeId(int OfficeSequence)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<gBusinesInfo>($"select b.Office_Sequence as Id,b.Business_Name as ClinicName FROM authentication_businessinfo b  WHERE b.Office_Number ={OfficeSequence} ").FirstOrDefault();
        }

        public static gOffice GetOfficeName(string connectionString, int officeSequence)
        {
            var db = new LTCDataModel.PetaPoco.Database(connectionString, "MySql");
            return db.Fetch<gOffice>($"SELECT Office_sequence, name FROM office WHERE Office_sequence=" + officeSequence).FirstOrDefault();
        }

        [Obsolete]
        public static int GetOfficeId(int UserId)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            return db.Fetch<int>(@"select Office_Sequence from authentication where Id=" + UserId).FirstOrDefault();
        }

        public static List<gBusinesInfo> GetOffices(int userId)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
            List<gBusinesInfo> offices = new List<gBusinesInfo>();

            var allowedOfficeList = db.Fetch<string>($"SELECT Office_Sequence FROM authentication_office_list where UserId = " + userId).ToList();
            if (allowedOfficeList != null)
            {
                offices = db.Fetch<gBusinesInfo>($"SELECT Office_Sequence as Id,Business_Name as ClinicName FROM authentication_businessinfo where Office_Sequence in (" + string.Join(",", allowedOfficeList) + ")").ToList();
            }

            return offices;
        }

        [Obsolete("This method is wrong, we need to get connection string from database evertime. use the below GetProvider method that take userid")]
        public List<gDentalProvider> GetProviders(string connectionString)
        {
            try
            {
                var db = new LTCDataModel.PetaPoco.Database(connectionString, "MySql");
                return db.Fetch<gDentalProvider>($"select Provider,Name,hygienist FROM provider WHERE ActiveProvider=1").ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static List<gDentalProvider> GetProviders(int userId)
        {
            try
            {
                List<gDentalProvider> providerList = new List<gDentalProvider>();

                var allowedOffices = Utility.GetAllowedOffices(userId);
                foreach (var item in allowedOffices)
                {
                    var providerDB = new LTCDataModel.PetaPoco.Database(Utility.CreateConnectionString(item), "MySql");
                    if (item.Providerrange.Trim() == "*")
                    {
                        var list = providerDB.Fetch<gDentalProvider>($"SELECT Provider,Name,hygienist,Office_Sequence FROM provider WHERE ActiveProvider=1 AND Office_Sequence = " + item.Office_Sequence).ToList();
                        Utility.RemoveDuplicates(ref providerList, list);
                    }
                    else
                    {
                        var list = providerDB.Fetch<gDentalProvider>($"SELECT Provider,Name,hygienist,Office_Sequence FROM provider WHERE ActiveProvider=1 AND Provider in (" + item.Providerrange.TrimEnd(',') + ") AND Office_Sequence = " + item.Office_Sequence).ToList();
                        Utility.RemoveDuplicates(ref providerList, list);
                    }
                }
                return providerList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



    }
}
