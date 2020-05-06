using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LTCDashboard.Data
{
    public class ApplicationUser : IdentityUser<int>
    {
        //[Key]
        // these 3 columns are already there
        //public int DoctorID { get; set; }
        //public string Password { get; set; }
        //public string Phone { get; set; }

        public int? Office_Number { get; set; } // changed from office_sequence to officeNumber
        public int Office_Sequence { get; set; } // changed from office_sequence to officeNumber
        public int? Branch_Number { get; set; } // changed from office_sequence to officeNumber

        public string AuthenticationPhone { get; set; }

        public string Provider { get; set; }
        public string Salutation { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Initials { get; set; }

        public string Fax { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        public bool IsSystemAdministrator { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsDisplaySummary { get; set; }
        public bool IsDefaultUser { get; set; }
      

        public string MondayCSV { get; set; }
        public string TuesdayCSV { get; set; }
        public string WednesdayCSV { get; set; }
        public string ThursdayCSV { get; set; }
        public string FridayCSV { get; set; }
        public string SaturdayCSV { get; set; }
        public string SundayCSV { get; set; }

        public DateTime? LastLogin { get; set; }
        public string PhotoImageURL { get; set; }
        public string WebsiteURL { get; set; }
        public string ActivationStatus { get; set; }
        public string LanguageSelected { get; set; }
        public string DateFormat { get; set; }
        public int? SelectedTemplateId { get; set; }
        public string SelectedMainTitle_Name_ClinicName { get; set; }
        public string PreferedSubIndustriesCSV { get; set; }
        public DateTime? FirstNewsletterDate { get; set; }
        public bool NotifyAutoSchedulesBeforeDispatch { get; set; }
        public bool NotifyAutoSchedulesAfterDispatch { get; set; }
        public int? AutoNewsletterCount { get; set; }
        public string DB_Path { get; set; }
        public string Serial_Number { get; set; }
        public int Cust_id { get; set; }
        public string Providerrange { get; set; }
    }
}
