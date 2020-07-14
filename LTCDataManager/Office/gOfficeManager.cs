using LTCDataModel.Office;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataManager.DataAccess;
using LTCDataModel.Configurations;
using Microsoft.Extensions.Options;
using LTCDataModel.Office;
using LTCDataModel.PetaPoco;
using LTCDataModel.User;

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
        public static gOfficeInfo GetOfficeName(int OfficeNumber)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcSystem);
            return db.Fetch<gOfficeInfo>($"SELECT * FROM businessinfo where Office_Sequence = '{OfficeNumber}' ").FirstOrDefault();
        }

        //public static gOfficeInfo GetOfficeNames(int OfficeNumber)
        //{
        //    var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcSystem);
        //    return db.Fetch<gOfficeInfo>($"SELECT * FROM businessinfo where Office_Sequence = '{OfficeNumber}' ").FirstOrDefault();
        //}

        //public static gPatientOfficeInfo GetOffice(string email)
        //{
        //    var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcSystem);
        //    return db.Fetch<gPatientOfficeInfo>($"select Office_Number, DoctorID from authentication where Email  = '{email.Replace("@", "@@")}' ").FirstOrDefault();
        //}
        //public static gPatientOfficeInfo GetPatientInfo(string email)
        //{
        //    var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcSystem);
        //    return db.Fetch<gPatientOfficeInfo>($"select * from authentication where Email  = '{email.Replace("@", "@@")}' ").FirstOrDefault();
        //}

        //public static List<gBusinesInfo> GetOfficeDetailByUserId(int userId)
        //{
        //    var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
        //    return db.Fetch<gBusinesInfo>($"select a.Office_Sequence as Id,b.Business_Name as ClinicName from authentication_office_list a INNER JOIN authentication_Businessinfo b ON a.Office_Sequence=b.Office_Sequence where a.UserId={userId} ").ToList();
        //}

        public static List<gBusinessInfo> GetOfficeDetailByOfficeSequence(int officeSequence)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcSystem);
            return db.Fetch<gBusinessInfo>($"select a.Office_Sequence as Id,b.ClinicName as ClinicName from businessinfo WHERE Office_Sequence={officeSequence} ").ToList();
        }
        public static gBusinessInfo GetBuisnessInfo(string syncIdentifier)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcSystem);
            return db.Fetch<gBusinessInfo>($"select Office_Sequence as Id,ClinicName,  as ClinicName, Active, Newsletter from businessinfo WHERE Office_Sequence={syncIdentifier} ").FirstOrDefault();
        }
        //public static gBusinesInfo GetOfficeDetailByOfficeId(int OfficeSequence)
        //{
        //    var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
        //    return db.Fetch<gBusinesInfo>($"select b.Office_Sequence as Id,b.Business_Name as ClinicName FROM authentication_businessinfo b  WHERE b.Office_Number ={OfficeSequence} ").FirstOrDefault();
        //}

        public static gOffice GetOfficeName(string connectionString, int officeSequence)
        {
            var db = new LTCDataModel.PetaPoco.Database(connectionString, "MySql");
            return db.Fetch<gOffice>($"SELECT Office_sequence, name FROM office WHERE Office_sequence=" + officeSequence).FirstOrDefault();
        }

        //[Obsolete]
        //public static int GetOfficeId(int UserId)
        //{
        //    var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcSystem);
        //    return db.Fetch<int>(@"select Office_Sequence from authentication where Id=" + UserId).FirstOrDefault();
        //}

        public static List<gBusinessInfo> GetOffices(int userId)
        {
            var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcSystem);
            List<gBusinessInfo> offices = new List<gBusinessInfo>();

            var allowedOfficeList = db.Fetch<string>($"SELECT Office_Sequence FROM authentication_office_list where UserId = " + userId).ToList();
            //TODO: if allowed list is empty. then handle it properly, right now it throws an error 
            if (allowedOfficeList != null && allowedOfficeList.Count > 0)
            {
                offices = db.Fetch<gBusinessInfo>($"SELECT Office_Sequence as Id,ClinicName FROM businessinfo where Office_Sequence in (" + string.Join(",", allowedOfficeList) + ")").ToList();
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
                //throw;
                return null;
            }
        }

        public static int GetOfficeSequenceByOfficeNumber(int? officeNumber)
        {
            int? model = 0;
            using (var db = new Database(DbConfiguration.LtcSystem))
            {
                string qry = $"select * from businessinfo where office_number={officeNumber}";
                model = db.Fetch<int>(qry).FirstOrDefault();
            }

            return model ?? 0;
        }

        public static List<gBusinessInfo> GetAllOffices()
        {
            List<gBusinessInfo> model = new List<gBusinessInfo>();
            using (var db = new Database(DbConfiguration.LtcSystem))
            {
                string qry = $"SELECT Office_Sequence, Office_Number, ClinicName FROM businessinfo";
                model = db.Fetch<gBusinessInfo>(qry).ToList();
            }

            return model;
        }

        public static List<int> GetAuthenticatedOfficeListByUserId(int userId)
        {
            List<int> model = new List<int>();
            using (var db = new Database(DbConfiguration.LtcSystem))
            {
                string qry = $"SELECT BI.Office_Number FROM authentication_office_list OL Inner Join businessinfo BI On BI.office_sequence = OL.office_sequence where OL.userid = {userId}";
                model = db.Fetch<int>(qry).ToList();
            }

            return model ?? new List<int>();
        }

        public static void UpdateUser(ApplicationUser model)
        {
            var sql = $"UPDATE authentication SET " +
                        (model.Branch_Number != null ? $"Branch_Number={model.Branch_Number}," : "Branch_Number=null,") +
                        (model.Office_Number != null ? $"Office_Number={model.Office_Number}," : "Office_Number=null,") +
                        $"Office_Sequence={model.Office_Sequence}," +
                        (model.SelectedTemplateId != null ? $"SelectedTemplateId={model.SelectedTemplateId}," : "SelectedTemplateId=null,") +
                        (model.FirstNewsletterDate != null ? $"FirstNewsletterDate='{model.FirstNewsletterDate}'," : "FirstNewsletterDate=null,") +
                        (model.AutoNewsletterCount != null ? $"AutoNewsletterCount={model.AutoNewsletterCount}," : "AutoNewsletterCount=null,") +
                        (model.Cust_id != null ? $"Cust_id={model.Cust_id}," : "Cust_id=null,") +
                        (string.IsNullOrEmpty(model.PasswordHash) == false ? $"Password='{model.PasswordHash}'," : "") +
                        (string.IsNullOrEmpty(model.AuthenticationPhone) == false ? $"AuthenticationPhone='{model.AuthenticationPhone}'," : "AuthenticationPhone=null,") +
                        (string.IsNullOrEmpty(model.AuthenticationPhone) == false ? $"Phone='{model.AuthenticationPhone}'," : "Phone=null,") +
                        (string.IsNullOrEmpty(model.Provider) == false ? $"Provider='{model.Provider}'," : "Provider=null,") +
                        (string.IsNullOrEmpty(model.Salutation) == false ? $"Salutation='{model.Salutation}'," : "Salutation=null,") +
                        (string.IsNullOrEmpty(model.FirstName) == false ? $"FirstName='{model.FirstName}'," : "FirstName=null,") +
                        (string.IsNullOrEmpty(model.LastName) == false ? $"LastName='{model.LastName}'," : "LastName=null,") +
                        (string.IsNullOrEmpty(model.Initials) == false ? $"Initials='{model.Initials}'," : "Initials=null,") +
                        (string.IsNullOrEmpty(model.AddressLine1) == false ? $"AddressLine1='{model.AddressLine1}'," : "AddressLine1=null,") +
                        (string.IsNullOrEmpty(model.AddressLine2) == false ? $"AddressLine2='{model.AddressLine2}'," : "AddressLine2=null,") +
                        (string.IsNullOrEmpty(model.AddressLine3) == false ? $"AddressLine3='{model.AddressLine3}'," : "AddressLine3=null,") +
                        (string.IsNullOrEmpty(model.City) == false ? $"City='{model.City}'," : "City=null,") +
                        (string.IsNullOrEmpty(model.Province) == false ? $"Province='{model.Province}'," : "Province=null,") +
                        (string.IsNullOrEmpty(model.Country) == false ? $"Country='{model.Country}'," : "Country=null,") +
                        (string.IsNullOrEmpty(model.PostalCode) == false ? $"PostalCode='{model.PostalCode}'," : "PostalCode=null,") +
                        (string.IsNullOrEmpty(model.Fax) == false ? $"Fax='{model.Fax}'," : "Fax=null,") +
                        (string.IsNullOrEmpty(model.PhotoImageURL) == false ? $"PhotoImageURL='{model.PhotoImageURL}'," : "PhotoImageURL=null,") +
                        (string.IsNullOrEmpty(model.WebsiteURL) == false ? $"WebsiteURL='{model.WebsiteURL}'," : "WebsiteURL=null,") +
                        (string.IsNullOrEmpty(model.ActivationStatus) == false ? $"ActivationStatus='{model.ActivationStatus}'," : "ActivationStatus=null,") +
                        (string.IsNullOrEmpty(model.LanguageSelected) == false ? $"LanguageSelected='{model.LanguageSelected}'," : "LanguageSelected=null,") +
                        (string.IsNullOrEmpty(model.DateFormat) == false ? $"DateFormat='{model.DateFormat}'," : "DateFormat=null,") +
                        (string.IsNullOrEmpty(model.SelectedMainTitle_Name_ClinicName) == false ? $"SelectedMainTitle_Name_ClinicName='{model.SelectedMainTitle_Name_ClinicName}'," : "SelectedMainTitle_Name_ClinicName=null,") +
                        (string.IsNullOrEmpty(model.PreferedSubIndustriesCSV) == false ? $"PreferedSubIndustriesCSV='{model.PreferedSubIndustriesCSV}'," : "PreferedSubIndustriesCSV=null,") +
                        (string.IsNullOrEmpty(model.DB_Path) == false ? $"DB_Path='{model.DB_Path}'," : "DB_Path=null,") +
                        (string.IsNullOrEmpty(model.Serial_Number) == false ? $"Serial_Number='{model.Serial_Number}'," : "Serial_Number=null,") +
                        (string.IsNullOrEmpty(model.Providerrange) == false ? $"Providerrange='{model.Providerrange}', " : "Providerrange=null,") +

                       ($"TwoFactorEnabled={model.TwoFactorEnabled},") +
                       ($"IsAdministrator={model.IsAdministrator},") +
                       ($"IsSystemAdministrator={model.IsSystemAdministrator},") +
                       ($"IsDefaultUser={model.IsDefaultUser},") +
                       ($"IsDisplaySummary={model.IsDisplaySummary},");

            using (var db = new Database(DbConfiguration.LtcSystem))
            {
                sql = sql.TrimEnd(',') + $" Where DoctorID = {model.Id}";
                db.Execute(new Sql(sql));
            }
        }

        public static void InsertAllowedOffices(int userId, List<int> allowedOffices)
        {
            if (allowedOffices.Count() > 0)
            {

                var offices = string.Join<int>(",", allowedOffices);
                var officeSequenceList = new List<int>();
                using (var db = new Database(DbConfiguration.LtcSystem))
                {
                    string qry = $"select office_sequence from businessinfo where office_number in ({offices})";
                    officeSequenceList = db.Fetch<int>(qry).ToList();
                }

                var sql = $"Delete from authentication_office_list WHERE UserId = {userId}; ";
                using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcSystem))
                {
                    var sqlTemplate = $"INSERT INTO authentication_office_list Values ({userId},[officeId],'*'); ";
                    foreach (var item in officeSequenceList)
                    {
                        sql += sqlTemplate.Replace("[officeId]", item.ToString());
                    }
                    db.Execute(new Sql(sql));
                }
            }
            else
            {
                //do nothing

            }
        }

    }
}
