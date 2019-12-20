using LTCDataModel.FormEntryHome;
using LTCDataModel.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataModel.Configurations;
using Microsoft.Extensions.Options;

namespace LTCDataManager.FormEntryHome
{
    public class gSearchPatientManager
    {
        private readonly ConfigSettings _configuration;

        public gSearchPatientManager(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
            Utility.Config = configuration.Value; ;
        }
        //SELECT ltc_dental.patient.Account,Doctor,LastName,FirstName,Sex,Birthdate,LastVisit,BestPhone FROM ltc_dental.patient;
        public static List<gSearchPatientViewModel> GetPatients(string connectionString, int OfficeId)
        {
            var db = new LTCDataModel.PetaPoco.Database(connectionString, "MySql");
            var newResults = new List<gSearchPatientViewModel>();
            try
            {
                var q = @"SELECT pt.Office_sequence,pt.Account,pt.PatientNumber,pt.Doctor,pt.LastName,pt.FirstName,pt.Sex,pt.Birthdate,pt.LastVisit,pt.BestPhone,
                    (Select phonenumber from patientphone ph where PatientNumber = pt.PatientNumber AND phoneType='P' AND Office_sequence=" + OfficeId + @") AS PhoneNumber,
                    (Select phonenumber from patientphone ph where PatientNumber = pt.PatientNumber AND phoneType='W' AND Office_sequence=" + OfficeId + @") As MobilePhone
                    FROM patient pt where pt.Office_sequence=" + OfficeId ;


                var results = db.Fetch<gSearchPatient>(q).ToList();

                foreach (var item in results)
                {
                    newResults.Add(new gSearchPatientViewModel
                    {
                        Account = item.Account,
                        PatientNumber = item.PatientNumber,
                        Doctor = item.Doctor,
                        Name = item.FirstName + " " + item.LastName,
                        Sex_Age = item.Sex.ToUpper() + "" + CalculateAge(item.Birthdate),
                        Office_Sequence = item.Office_Sequence,
                        Birthdate = item.Birthdate.ToString("MM/dd/yyyy"),
                        LastVisit = item.LastVisit.ToString("MM/dd/yyyy"),
                        HomePhone = item.PhoneNumber,
                        CellPhone = item.MobilePhone
                    });
                }

            }
            catch (Exception ex)
            {


            }
            finally
            {
                db.CloseSharedConnection();
            }

            return newResults;
        }

        private static int CalculateAge(DateTime BirthDate)
        {
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - BirthDate.Year;
            // Go back to the year the person was born in case of a leap year
            if (BirthDate > today.AddYears(-age)) age--;

            return age;
        }

        public static List<gPatient> GetPatientDetail(string connectionString, int officeSequence, int patientNumber)
        {
            var db = new LTCDataModel.PetaPoco.Database(connectionString, "MySql");
            var result = new List<gPatient>();
            try
            {
                result= db.Fetch<gPatient>($"Select patientnumber,account,title,title, lastname, firstname, initial,birthdate,sex,doctor from patient where office_Sequence = {officeSequence} and PatientNumber = {patientNumber}");
            }
            catch (Exception ex)
            {


            }
            finally
            {
                db.CloseSharedConnection();
            }
            return result;
        }

        public static List<gAddress> GetPatientAddress(string connectionString, int officeSequence, int patientNumber)
        {
            var db = new LTCDataModel.PetaPoco.Database(connectionString, "MySql");
            var result = new List<gAddress>();
            try
            {

            }
            catch (Exception ex)
            {


            }
            finally
            {
                db.CloseSharedConnection();
            }
            return db.Fetch<gAddress>($"select A.Office_sequence, A.title, A.lastname, A.firstname, A.initial, A.street, A.street2, A.street3, A.citycode, A.postalcode, A.note, A.AddrCounter, A.PostCounter, C.line1 from address A LEFT JOIN code C on A.citycode = C.code where  A.office_Sequence = {officeSequence} and A.PatientNumber = {patientNumber} AND A.type='-'");
            //return db.Fetch<gAddress>($"select Office_sequence, title, lastname, firstname, initial, street, street2, street3, citycode, postalcode, note, AddrCounter, PostCounter from address where  office_Sequence = {officeSequence} and PatientNumber = {patientNumber}");

        }

        public static List<gPatientPhone> GetPatientPhone(string connectionString, int officeSequence, int patientNumber)
        {
            var db = new LTCDataModel.PetaPoco.Database(connectionString, "MySql");
            var result = new List<gPatientPhone>();
            try
            {

                result = db.Fetch<gPatientPhone>($"Select phonetype,areacode,phonenumber,extension,bestcalltimefrom,bestcalltimeto,PhoneCounter  from patientphone where office_Sequence = {officeSequence} and PatientNumber = {patientNumber}");

            }
            catch (Exception ex)
            {


            }
            finally
            {
                db.CloseSharedConnection();
            }
            return result;
        }

    }
}
