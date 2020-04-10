using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataManager.DataAccess;
using LTCDataModel.Configurations;
using LTCDataModel.Dashboard;
using LTCDataModel.Patient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LTCDataManager.Dashboard
{
    public class gChartManager
    {
        private readonly ConfigSettings _configuration;
        private Mapping _mapping;
        public gChartManager(IOptions<ConfigSettings> configuration,
            IOptions<Mapping> mapping)
        {
            _configuration = configuration.Value;
            _mapping = mapping.Value;
            Utility.Config = configuration.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offices"></param>
        /// <param name="providerList"> This list contains entries with [OfficeSequence]_[Providers] </param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public decimal GetNewPatient(int[] offices, string[] providerList, string startDate, string endDate)
        {
            decimal result = 0;
            var providers = Utility.ProviderToList(providerList);
            foreach (var office in offices)
            {
                if (providers.All(x => x.Office_Sequence != office))
                {
                    continue;
                }

                var db = PocoDatabase.DbConnection(Utility.GetConnectionStringByOfficeId(office));
                string query = $"SELECT count(accountopendate) as result FROM patient where activepatient=1 AND accountopendate >= '{startDate}' AND accountopendate <= '{endDate}' AND office_sequence = {office} AND doctor in ({Utility.FilterProviderToString(office, providers)})";
                var model = db.ExecuteScalar<decimal?>(query);
                if (model.HasValue)
                {
                    result += model.Value;
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offices"></param>
        /// <param name="providerList">This list contains entries with [OfficeSequence]_[Providers]</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<gPatientList> GetPatientRecords(int[] offices, string[] providerList, string startDate, string endDate)
        {
            List<gPatientList> result = new List<gPatientList>();
            var providers = Utility.ProviderToList(providerList);
            foreach (var office in offices)
            {
                if (providers.All(x => x.Office_Sequence != office))
                {
                    continue;
                }

                var db = PocoDatabase.DbConnection(Utility.GetConnectionStringByOfficeId(office));
                string query = $"SELECT office_sequence,patientnumber,firstname,lastname FROM patient where activepatient=1 AND accountopendate >= '{startDate}' AND accountopendate <= '{endDate}' AND office_sequence = {office} AND doctor in ({Utility.FilterProviderToString(office, providers)})";

                var model = db.Fetch<gPatientList>(query);
                result.AddRange(model);
            }

            return result;
        }

        public gPatientProfile GetPatientProfile(int patientNumber, int officeSequence)
        {
            var db = PocoDatabase.DbConnection(DbConfiguration.LtcDental);

            string query = "" +
            $"SELECT (Select CONCAT(street, '<br />', street2, '<br />', street2, '<br />', postalcode) FROM address WHERE Office_sequence = {officeSequence} AND patientNumber = {patientNumber}) as Address, " +
            $"(Select phonenumber from patientphone ph WHERE PatientNumber = pt.PatientNumber AND phoneType = 'P' AND Office_sequence = {officeSequence} AND patientNumber = {patientNumber}) AS HomeNumber, " +
            $"(Select phonenumber from patientphone ph WHERE PatientNumber = pt.PatientNumber AND phoneType = 'W' AND Office_sequence = {officeSequence} AND patientNumber = {patientNumber}) As WorkPhone, " +
            $"(Select phonenumber from patientphone ph WHERE PatientNumber = pt.PatientNumber AND phoneType = 'O' AND Office_sequence = {officeSequence} AND patientNumber = {patientNumber}) As OtherPhone, " +
            $"DATEDIFF(now(), birthdate) / 365.25 as Age, " +
            $"sex, " +
            $"maritalstatus, " +
            $"'' as InsuranceProvider " +
            $"FROM patient pt WHERE Office_sequence = {officeSequence} AND patientNumber = {patientNumber}";


            var model = db.Fetch<gPatientProfile>(query).FirstOrDefault();

            return model;
        }

        public List<gPatientAppointment> LoadPatientAppointment(int id, int officeSequence, string startDate, string endDate)
        {
            var db = PocoDatabase.DbConnection(DbConfiguration.LtcDental);

            string query = $"select invoicedate,provider,(patamount/100)+(insamount/100) as Fee from item WHERE patientNumber={id} AND invoicedate >= '{startDate}' AND invoicedate <= '{endDate}' AND Office_sequence = {officeSequence}";

            var model = db.Fetch<gPatientAppointment>(query);

            return model;
        }

        public List<gPatient> LoadPatientTreatment(int id, int officeSequence, string startDate, string endDate)
        {
            //var db = PocoDatabase.DbConnection(DbConfiguration.LtcDental);

            //string query = $"SELECT * FROM patient where patientNumber={id} AND accountopendate >= '{startDate}' AND accountopendate <= '{endDate}' AND office_sequence in ({offices})";

            //var model = db.Fetch<gPatient>(query);

            //return model;
            return null;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="offices"></param>
        /// <param name="providerList">This list contains entries with [OfficeSequence]_[Providers]</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public decimal GetTotalNetProduction(int[] offices, string[] providerList, string startDate, string endDate, string types)
        {
            types = Utility.StringToCharacterString(types, _configuration.TotalNetProductionInvoiceTypes);

            decimal result = 0;
            var providers = Utility.ProviderToList(providerList);
            foreach (var office in offices)
            {
                if (providers.All(x => x.Office_Sequence != office))
                {
                    continue;
                }

                var db = PocoDatabase.DbConnection(Utility.GetConnectionStringByOfficeId(office));
                string query =
                $"SELECT IFNULL(sum(patamount/100) + sum(insamount/100),0) as amount FROM item " +
                $"where provider in (SELECT provider FROM provider WHERE provider in ({Utility.FilterProviderToString(office, providers)}) AND Office_sequence = {office} AND activeProvider = 1 AND hygienist in ({Utility.HygenistType("TotalNetProductionTypes")})) and " +
                $"invoiceType in ({types}) and invoicedate >= '{startDate}' AND invoicedate <= '{endDate}' " +
                $"AND Office_sequence = {office}";

                var model = db.ExecuteScalar<decimal>(query);
                result += model;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offices"></param>
        /// <param name="providerList">This list contains entries with [OfficeSequence]_[Providers]</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<gBreakdown> GetTotalNetProductionBreakdown(int[] offices, string[] providerList, string startDate, string endDate, string types)
        {
            types = Utility.StringToCharacterString(types, _configuration.TotalNetProductionInvoiceTypes);

            List<gBreakdown> result = new List<gBreakdown>();
            var providers = Utility.ProviderToList(providerList);
            foreach (var office in offices)
            {
                if (providers.All(x => x.Office_Sequence != office))
                {
                    continue;
                }

                var db = PocoDatabase.DbConnection(Utility.GetConnectionStringByOfficeId(office));
                string query =
              $"SELECT i.Office_sequence,i.patientNumber,i.invoicedate,i.patamount/100 patamount ,i.insamount/100 insamount ,i.provider,i.invoiceType, lastname, firstname,title FROM item i LEFT JOIN patient P on i.patientnumber = P.patientnumber AND i.Office_sequence = P.Office_sequence " +
              $"where provider in (SELECT provider FROM provider WHERE provider in ({Utility.FilterProviderToString(office, providers)}) AND i.Office_sequence = {office} AND activeProvider = 1 AND hygienist in ({Utility.HygenistType("TotalNetProductionTypes")})) and " +
              $"invoiceType in ({types}) and invoicedate >= '{startDate}' AND invoicedate <= '{endDate}' " +
              $"AND i.Office_sequence = {office} order by invoicedate,invoiceType";

                var model = db.Fetch<gBreakdown>(query);
                result.AddRange(model);
            }

            return AddInvoiceType(result);
        }



        public decimal GetTotalPaymentReceipt(int[] offices, string[] providerList, string startDate, string endDate, string types)
        {
            types = Utility.StringToCharacterString(types, _configuration.TotalPaymentReceiptInvoiceTypes);

            decimal result = 0;
            var providers = Utility.ProviderToList(providerList);
            foreach (var office in offices)
            {
                if (providers.All(x => x.Office_Sequence != office))
                {
                    continue;
                }

                var db = PocoDatabase.DbConnection(Utility.GetConnectionStringByOfficeId(office));
                string query =
                $"SELECT IFNULL(sum(patamount/100) + sum(insamount/100),0) as amount FROM item " +
                $"where provider in (SELECT provider FROM provider WHERE provider in ({Utility.FilterProviderToString(office, providers)}) AND Office_sequence = {office} AND activeProvider = 1 AND hygienist in ({Utility.HygenistType("TotalPaymentReceiptTypes")})) and " +
                $"invoiceType in ({types}) and invoicedate >= '{startDate}' AND invoicedate <= '{endDate}' " +
                $"AND Office_sequence = {office}";

                var model = db.ExecuteScalar<decimal>(query);
                result += model;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offices"></param>
        /// <param name="providerList">This list contains entries with [OfficeSequence]_[Providers]</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<gBreakdown> GetTotalPaymentReceiptBreakdown(int[] offices, string[] providerList, string startDate, string endDate, string types)
        {
            types = Utility.StringToCharacterString(types, _configuration.TotalPaymentReceiptInvoiceTypes);

            List<gBreakdown> result = new List<gBreakdown>();
            var providers = Utility.ProviderToList(providerList);
            foreach (var office in offices)
            {
                if (providers.All(x => x.Office_Sequence != office))
                {
                    continue;
                }

                var db = PocoDatabase.DbConnection(Utility.GetConnectionStringByOfficeId(office));
                string query =
              $"SELECT i.Office_sequence,i.patientNumber,i.invoicedate,i.patamount/100 patamount ,i.insamount/100 insamount ,i.provider,i.invoiceType, lastname, firstname,title FROM item i LEFT JOIN patient P on i.patientnumber = P.patientnumber AND i.Office_sequence = P.Office_sequence " +
              $"where provider in (SELECT provider FROM provider WHERE provider in ({Utility.FilterProviderToString(office, providers)}) AND i.Office_sequence = {office} AND activeProvider = 1 AND hygienist in ({Utility.HygenistType("TotalPaymentReceiptTypes")})) and " +
              $"invoiceType in ({types}) and invoicedate >= '{startDate}' AND invoicedate <= '{endDate}' " +
              $"AND i.Office_sequence = {office} order by invoicedate,invoiceType";

                var model = db.Fetch<gBreakdown>(query);
                result.AddRange(model);
            }

            return AddInvoiceType(result);
        }



        public decimal GetTotalNetPaymentReceipt(int[] offices, string[] providerList, string startDate, string endDate, string types)
        {
            types = Utility.StringToCharacterString(types, _configuration.TotalNetPaymentReceiptInvoiceTypes);

            decimal result = 0;
            var providers = Utility.ProviderToList(providerList);
            foreach (var office in offices)
            {
                if (providers.All(x => x.Office_Sequence != office))
                {
                    continue;
                }

                var db = PocoDatabase.DbConnection(Utility.GetConnectionStringByOfficeId(office));
                string query =
                $"SELECT IFNULL(sum(patamount/100) + sum(insamount/100),0) as amount FROM item " +
                $"where provider in (SELECT provider FROM provider WHERE provider in ({Utility.FilterProviderToString(office, providers)}) AND Office_sequence = {office} AND activeProvider = 1 AND hygienist in ({Utility.HygenistType("TotalNetPaymentReceiptTypes")})) and " +
                $"invoiceType in ({types}) and invoicedate >= '{startDate}' AND invoicedate <= '{endDate}' " +
                $"AND Office_sequence = {office}";

                var model = db.ExecuteScalar<decimal>(query);
                result += model;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offices"></param>
        /// <param name="providerList">This list contains entries with [OfficeSequence]_[Providers]</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<gBreakdown> GetTotalNetPaymentReceiptBreakdown(int[] offices, string[] providerList, string startDate, string endDate, string types)
        {
            types = Utility.StringToCharacterString(types, _configuration.TotalNetPaymentReceiptInvoiceTypes);

            List<gBreakdown> result = new List<gBreakdown>();
            var providers = Utility.ProviderToList(providerList);
            foreach (var office in offices)
            {
                if (providers.All(x => x.Office_Sequence != office))
                {
                    continue;
                }

                var db = PocoDatabase.DbConnection(Utility.GetConnectionStringByOfficeId(office));
                string query =
              $"SELECT i.Office_sequence,i.patientNumber,i.invoicedate,i.patamount/100 patamount ,i.insamount/100 insamount ,i.provider,i.invoiceType, lastname, firstname,title FROM item i LEFT JOIN patient P on i.patientnumber = P.patientnumber AND i.Office_sequence = P.Office_sequence " +
              $"where provider in (SELECT provider FROM provider WHERE provider in ({Utility.FilterProviderToString(office, providers)}) AND i.Office_sequence = {office} AND activeProvider = 1 AND hygienist in ({Utility.HygenistType("TotalNetPaymentReceiptTypes")})) and " +
              $"invoiceType in ({types}) and invoicedate >= '{startDate}' AND invoicedate <= '{endDate}' " +
              $"AND i.Office_sequence = {office} order by invoicedate,invoiceType";

                var model = db.Fetch<gBreakdown>(query);
                result.AddRange(model);
            }

            return AddInvoiceType(result);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="offices"></param>
        /// <param name="providerList">This list contains entries with [OfficeSequence]_[Providers]</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public decimal GetTotalHygenistProduction(int[] offices, string[] providerList, string startDate, string endDate, string types)
        {
            types = Utility.StringToCharacterString(types, _configuration.TotalHygenistProductionInvoiceTypes);

            decimal result = 0;
            var providers = Utility.ProviderToList(providerList);
            foreach (var office in offices)
            {
                if (providers.All(x => x.Office_Sequence != office))
                {
                    continue;
                }

                var db = PocoDatabase.DbConnection(Utility.GetConnectionStringByOfficeId(office));
                string query =
                 $"SELECT IFNULL(sum(patamount/100) + sum(insamount/100),0) as amount FROM item " +
                 $"where provider in (SELECT provider FROM provider WHERE provider in ({Utility.FilterProviderToString(office, providers)}) AND Office_sequence = {office} AND activeProvider = 1 AND hygienist in ({Utility.HygenistType("TotalHygenistProductionType")})) and " +
                 $"invoiceType in ({types}) and invoicedate >= '{startDate}' AND invoicedate <= '{endDate}' " +
                 $"AND Office_sequence = {office}";

                var model = db.ExecuteScalar<decimal>(query);
                result += model;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offices"></param>
        /// <param name="providerList">This list contains entries with [OfficeSequence]_[Providers]</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<gBreakdown> GetTotalHygenistProductionBreakdown(int[] offices, string[] providerList, string startDate, string endDate, string types)
        {
            types = Utility.StringToCharacterString(types, _configuration.TotalHygenistProductionInvoiceTypes);

            List<gBreakdown> result = new List<gBreakdown>();
            var providers = Utility.ProviderToList(providerList);
            foreach (var office in offices)
            {
                if (providers.All(x => x.Office_Sequence != office))
                {
                    continue;
                }

                var db = PocoDatabase.DbConnection(Utility.GetConnectionStringByOfficeId(office));
                string query =
                $"SELECT i.Office_sequence,i.patientNumber,i.invoicedate,i.patamount/100 patamount ,i.insamount/100 insamount,i.provider,i.invoiceType, lastname, firstname,title FROM item i LEFT JOIN patient P on i.patientnumber = P.patientnumber  AND i.Office_sequence = P.Office_sequence " +
                $"where provider in (SELECT provider FROM provider WHERE provider in ({Utility.FilterProviderToString(office, providers)}) AND i.Office_sequence = {office} AND activeProvider = 1 AND hygienist in ({Utility.HygenistType("TotalHygenistProductionType")})) and " +
                $"invoiceType in ({types}) and invoicedate >= '{startDate}' AND invoicedate <= '{endDate}' " +
                $"AND i.Office_sequence = {office} order by invoicedate,invoiceType";

                var model = db.Fetch<gBreakdown>(query);
                result.AddRange(model);
            }

            return AddInvoiceType(result);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="offices"></param>
        /// <param name="providerList">This list contains entries with [OfficeSequence]_[Providers]</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public decimal GetTotalNetDoctorProduction(int[] offices, string[] providerList, string startDate, string endDate, string types)
        {
            types = Utility.StringToCharacterString(types, _configuration.TotalTotalNetDoctorProductionInvoiceTypes);

            decimal result = 0;
            var providers = Utility.ProviderToList(providerList);
            foreach (var office in offices)
            {
                if (providers.All(x => x.Office_Sequence != office))
                {
                    continue;
                }

                var db = PocoDatabase.DbConnection(Utility.GetConnectionStringByOfficeId(office));
                string query =
           $"SELECT IFNULL(sum(patamount/100) + sum(insamount/100),0) as amount FROM item " +
           $"where provider in (SELECT provider FROM provider WHERE provider in ({Utility.FilterProviderToString(office, providers)}) AND Office_sequence = {office} AND activeProvider = 1 AND hygienist in ({Utility.HygenistType("TotalTotalNetDoctorProductionType")})) and " +
           $"invoiceType in ({types}) and invoicedate >= '{startDate}' AND invoicedate <= '{endDate}' " +
           $"AND Office_sequence = {office}";

                var model = db.ExecuteScalar<decimal>(query);
                result += model;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offices"></param>
        /// <param name="providerList">This list contains entries with [OfficeSequence]_[Providers]</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<gBreakdown> GetTotalNetDoctorProductionBreakdown(int[] offices, string[] providerList, string startDate, string endDate, string types)
        {
            types = Utility.StringToCharacterString(types, _configuration.TotalTotalNetDoctorProductionInvoiceTypes);

            List<gBreakdown> result = new List<gBreakdown>();
            var providers = Utility.ProviderToList(providerList);
            foreach (var office in offices)
            {
                if (providers.All(x => x.Office_Sequence != office))
                {
                    continue;
                }

                var db = PocoDatabase.DbConnection(Utility.GetConnectionStringByOfficeId(office));
                string query =
                 $"SELECT i.Office_sequence,i.patientNumber,i.invoicedate,i.patamount/100 patamount ,i.insamount/100 insamount,i.provider,i.invoiceType, lastname, firstname,title FROM item i LEFT JOIN patient P on i.patientnumber = P.patientnumber  AND i.Office_sequence = P.Office_sequence " +
                 $"where provider in (SELECT provider FROM provider WHERE provider in ({Utility.FilterProviderToString(office, providers)}) AND i.Office_sequence = {office} AND activeProvider = 1 AND hygienist in ({Utility.HygenistType("TotalTotalNetDoctorProductionType")})) and " +
                 $"invoiceType in ({types}) and invoicedate >= '{startDate}' AND invoicedate <= '{endDate}' " +
                 $"AND i.Office_sequence = {office} order by invoicedate,invoiceType";

                var model = db.Fetch<gBreakdown>(query);

                result.AddRange(model);
            }

            return AddInvoiceType(result);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="offices"></param>
        /// <param name="providerList">This list contains entries with [OfficeSequence]_[Providers]</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<gPieChart> GetServiceAnalysis(int[] offices, string[] providerList, string startDate, string endDate, string types)
        {
            types = Utility.StringToCharacterString(types, _configuration.ServiceAnalysisInvoiceTypes);

            List<gCode> codes = new List<gCode>();
            List<gPieChart> serviceAnalysisList = new List<gPieChart>();
            var providers = Utility.ProviderToList(providerList);
            foreach (var office in offices)
            {
                if (providers.All(x => x.Office_Sequence != office))
                {
                    continue;
                }

                var db = PocoDatabase.DbConnection(Utility.GetConnectionStringByOfficeId(office));
                string query = $"Select Office_sequence,codecounter,code,line1,line2 from code where codetype = 'Y'  AND Office_Sequence = {office};";

                var officeCodeList = db.Fetch<gCode>(query);
                codes.AddRange(officeCodeList);

                //xlabel = Total sum
                //yValue = CodeCategory

                query = $"Select "
                        + $"IFNULL(sum(patamount/100) + sum(insamount/100),0) as yValue, IFNULL(d.servicecode,0) as UsableInformation from item  i "
                        + $"LEFT JOIN detail d on i.invoiceNumber = d.invoicenumber "
                        + $"LEFT JOIN provider p on i.provider = p.provider "
                        + $"WHERE i.provider in (SELECT provider FROM provider WHERE provider in ({Utility.FilterProviderToString(office, providers)}) AND Office_sequence = {office} AND activeProvider = 1 AND hygienist in ({Utility.HygenistType("ServiceAnalysisTypes")})) "
                        + $"AND InvoiceType in ({types}) "
                        + $"AND i.invoicedate >= '{startDate}' "
                        + $"AND i.invoicedate <= '{endDate}' "
                        + $"AND i.Office_sequence = {office} "
                        + $"Group by d.servicecode ";

                var seAnalysis = db.Fetch<gPieChart>(query);

                foreach (var item in seAnalysis)
                {
                    var code = officeCodeList.FirstOrDefault(x => x.Office_Sequence == office && x.Code == item.UsableInformation);
                    if (code != null)
                    {
                        item.xLabel = code.Code;
                    }
                    else
                    {
                        if (int.TryParse(item.UsableInformation, out var serviceCode))
                        {
                            var intCode = officeCodeList.FirstOrDefault(x => x.Office_Sequence == office
                                                                            && serviceCode >= x.Line1Int
                                                                            && serviceCode <= x.Line2Int);

                            item.xLabel = intCode != null ? intCode.Code : "";

                        }
                        else
                        {
                            item.xLabel = item.UsableInformation;
                        }
                    }
                }


                serviceAnalysisList.AddRange(seAnalysis);
            }

            return serviceAnalysisList.OrderBy(x => x.xLabel).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offices"></param>
        /// <param name="providerList">This list contains entries with [OfficeSequence]_[Providers]</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<gServiceAnalysis> GetServiceAnalysisBreakdown(int[] offices, string[] providerList, string startDate, string endDate, string types)
        {
            types = Utility.StringToCharacterString(types, _configuration.ServiceAnalysisInvoiceTypes);

            List<gCode> codes = new List<gCode>();
            List<gServiceAnalysis> serviceAnalysisList = new List<gServiceAnalysis>();
            var providers = Utility.ProviderToList(providerList);
            foreach (var office in offices)
            {
                if (providers.All(x => x.Office_Sequence != office))
                {
                    continue;
                }

                var db = PocoDatabase.DbConnection(Utility.GetConnectionStringByOfficeId(office));
                string query = $"Select Office_sequence,codecounter,code,line1,line2 from code where codetype = 'Y'  AND Office_Sequence = {office};";

                var officeCodeList = db.Fetch<gCode>(query);
                codes.AddRange(officeCodeList);

                query = $"Select "
                        + $"i.Office_sequence,i.invoiceNumber, sum(i.insamount/100) as insamount,sum(i.patamount/100) as patamount, d.servicecode, "
                        + $"d.provider, p.name as ProviderName, d.invoicedate from item  i "
                        + $"LEFT JOIN detail d on i.invoiceNumber = d.invoicenumber "
                        + $"LEFT JOIN provider p on i.provider = p.provider "
                        + $"WHERE i.provider in (SELECT provider FROM provider WHERE provider in ({Utility.FilterProviderToString(office, providers)}) AND Office_sequence = {office} AND activeProvider = 1 AND hygienist in ({Utility.HygenistType("ServiceAnalysisTypes")})) "
                        + $"AND InvoiceType in ({types}) "
                        + $"AND i.invoicedate >= '{startDate}' "
                        + $"AND i.invoicedate <= '{endDate}' "
                        + $"AND i.Office_sequence = {office} "
                        + $"Group by i.Office_sequence, d.servicecode, i.invoicenumber, d.provider, d.invoicedate "
                        + $"order by i.invoiceNumber, d.provider, d.invoicedate, d.servicecode; ";

                var seAnalysis = db.Fetch<gServiceAnalysis>(query);

                foreach (var item in seAnalysis)
                {
                    var code = officeCodeList.FirstOrDefault(x => x.Office_Sequence == office && x.Code == item.ServiceCode);
                    if (code != null)
                    {
                        item.CodeCategory = code.Code;
                    }
                    else
                    {
                        if (int.TryParse(item.ServiceCode, out var serviceCode))
                        {
                            var intCode = officeCodeList.FirstOrDefault(x => x.Office_Sequence == office
                                                                            && serviceCode >= x.Line1Int
                                                                            && serviceCode <= x.Line2Int);
                            if (intCode != null)
                            {
                                item.CodeCategory = intCode.Code;
                            }

                        }
                        else
                        {
                            item.CodeCategory = item.ServiceCode;
                        }
                    }
                }


                serviceAnalysisList.AddRange(seAnalysis);
            }

            return serviceAnalysisList;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="offices"></param>
        /// <param name="providerList">This list contains entries with [OfficeSequence]_[Providers]</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public string GetCancellationAndNoShows(int[] offices, string[] providerList, string startDate, string endDate, string types)
        {
            types = Utility.StringToCharacterString(types, _configuration.CancellationAndNoShowsStatusTypes);

            List<gCode> codes = new List<gCode>();
            List<object> serviceAnalysisList = new List<object>();
            var providers = Utility.ProviderToList(providerList);
            foreach (var office in offices)
            {
                if (providers.All(x => x.Office_Sequence != office))
                {
                    continue;
                }

                var db = PocoDatabase.DbConnection(Utility.GetConnectionStringByOfficeId(office));

                //string query = $"SELECT contactdate as xLabel, count(r.patientNumber) as yValue FROM recall r "
                //        + $"LEFT JOIN provider p on r.provider = p.provider  "
                //        + $"LEFT JOIN patient pt on r.patientNumber = pt.patientnumber "
                //        + $"WHERE r.provider in (SELECT provider FROM provider WHERE provider in ({Utility.FilterProviderToString(office, providers)}) AND Office_sequence = {office} AND activeProvider = 1 AND hygienist in ({Utility.HygenistType("CancellationAndNoShowsTypes")})) "
                //        + $"AND status in ({types}) "
                //        + $"AND r.contactdate >= '{startDate}' "
                //        + $"AND r.contactdate <= '{endDate}' "
                //        + $"AND r.Office_sequence = {office} "
                //        + $"Group By r.office_sequence, contactdate ";


                string query = $"SELECT DATE_FORMAT(contactdate, \"%Y/%m\") as contactDate, count(r.patientNumber) as count, status FROM recall r "
                          + $"LEFT JOIN provider p on r.provider = p.provider  "
                       + $"LEFT JOIN patient pt on r.patientNumber = pt.patientnumber "
                       + $"WHERE r.provider in (SELECT provider FROM provider WHERE provider in ({Utility.FilterProviderToString(office, providers)}) AND Office_sequence = {office} AND activeProvider = 1 AND hygienist in ({Utility.HygenistType("CancellationAndNoShowsTypes")})) "
                       + $"AND status in ({types}) "
                       + $"AND r.contactdate >= '{startDate}' "
                       + $"AND r.contactdate <= '{endDate}' "
                       + $"AND r.Office_sequence = {office} "
                       + $"Group By r.office_sequence, DATE_FORMAT(contactdate, '%m %Y'), r.status "
                       + $"";

                var seAnalysis = db.Fetch<object>(query);
               
                serviceAnalysisList.AddRange(seAnalysis);
            }
            return JsonConvert.SerializeObject(serviceAnalysisList, Formatting.Indented);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offices"></param>
        /// <param name="providerList">This list contains entries with [OfficeSequence]_[Providers]</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<gCancellationAndNoShows> GetCancellationAndNoShowsBreakdown(int[] offices, string[] providerList, string startDate, string endDate, string types)
        {
            types = Utility.StringToCharacterString(types, _configuration.CancellationAndNoShowsStatusTypes);

            List<gCode> codes = new List<gCode>();
            List<gCancellationAndNoShows> serviceAnalysisList = new List<gCancellationAndNoShows>();
            var providers = Utility.ProviderToList(providerList);
            foreach (var office in offices)
            {
                if (providers.All(x => x.Office_Sequence != office))
                {
                    continue;
                }

                var db = PocoDatabase.DbConnection(Utility.GetConnectionStringByOfficeId(office));

                string query = $"SELECT r.Office_sequence,p.provider as provider, CONCAT(p.title , ' ', p.name) as providerName,  r.patientNumber, CONCAT(pt.title , ' ', pt.firstname ,' ', pt.lastname) as patientName, r.contactdate, r.job, r.timeslot FROM recall r "
                        + $"LEFT JOIN provider p on r.provider = p.provider  "
                        + $"LEFT JOIN patient pt on r.patientNumber = pt.patientnumber "
                        + $"WHERE r.provider in (SELECT provider FROM provider WHERE provider in ({Utility.FilterProviderToString(office, providers)}) AND Office_sequence = {office} AND activeProvider = 1 AND hygienist in ({Utility.HygenistType("CancellationAndNoShowsTypes")})) "
                        + $"AND status in ({types}) "
                        + $"AND r.contactdate >= '{startDate}' "
                        + $"AND r.contactdate <= '{endDate}' "
                        + $"AND r.Office_sequence = {office} ";

                var seAnalysis = db.Fetch<gCancellationAndNoShows>(query);
                foreach (var item in seAnalysis)
                {
                    item.Time = TimeSpan.FromMinutes(item.TimeSlot);
                }

                serviceAnalysisList.AddRange(seAnalysis);
            }

            return serviceAnalysisList;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="offices"></param>
        /// <param name="providerList">This list contains entries with [OfficeSequence]_[Providers]</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public decimal GetAverageProductionPerPatient(int[] offices, string[] providerList, string startDate, string endDate, string types)
        {
            types = Utility.StringToCharacterString(types, _configuration.TotalAverageProductionPerPatientInvoiceTypes);

            decimal paidAmount = 0;
            decimal patientCount = 0;
            var providers = Utility.ProviderToList(providerList);
            foreach (var office in offices)
            {
                if (providers.All(x => x.Office_Sequence != office))
                {
                    continue;
                }

                var db = PocoDatabase.DbConnection(Utility.GetConnectionStringByOfficeId(office));

                string query =
                $"SELECT IFNULL(sum(patamount/100) + sum(insamount/100),0) as amount FROM item " +
                $"where provider in (SELECT provider FROM provider WHERE provider in ({Utility.FilterProviderToString(office, providers)}) AND Office_sequence = {office} AND activeProvider = 1 AND hygienist in ({Utility.HygenistType("TotalAverageProductionPerPatientType")})) and " +
                $"invoiceType in ({types}) and invoicedate >= '{startDate}' AND invoicedate <= '{endDate}' " +
                $"AND Office_sequence = {office}";
                var model = db.ExecuteScalar<decimal>(query);

                string numberOfPatientsQuery = $"SELECT count(accountopendate) as result FROM patient where activepatient=1 AND accountopendate >= '{startDate}' AND accountopendate <= '{endDate}' AND office_sequence = {office} AND doctor in ({Utility.FilterProviderToString(office, providers)}) ";
                //string numberOfPatientsQuery = $"SELECT count(accountopendate) as result FROM patient where activepatient=1 AND office_sequence = {office} AND doctor in ({Utility.FilterProviderToString(office, providers)}) ";

                var numberOfPatients = db.ExecuteScalar<decimal>(numberOfPatientsQuery);

                paidAmount += model;
                patientCount += numberOfPatients;
            }


            if (paidAmount != 0 && patientCount != 0)
            {
                return paidAmount / patientCount;
            }

            return 0;
        }


        public List<gBreakdown> AddInvoiceType(List<gBreakdown> list)
        {
            foreach (var item in list)
            {
                item.InvoiceTypeDetail = item.InvoiceType;
                if (!string.IsNullOrEmpty(item.InvoiceType))
                {
                    var type = _mapping.Map.FirstOrDefault(x => x.Type == item.InvoiceType);
                    if (type != null)
                    {
                        item.InvoiceTypeDetail = type.Description;
                    }
                }

            }
            return list;
        }
    }
}
