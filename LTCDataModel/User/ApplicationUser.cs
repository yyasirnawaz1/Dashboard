using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace LTCDataModel.User
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Display(Name = "Office No.")]
        public int? Office_Number { get; set; }

        [Required]
        [Display(Name = "Office Sequence")]
        public int Office_Sequence { get; set; }

        [Display(Name = "Branch No.")]
        public int? Branch_Number { get; set; }

        public string AuthenticationPhone { get; set; }

        public string Provider { get; set; }
        public string Salutation { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
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

        [Display(Name = "System Admin")]
        public bool? IsSystemAdministrator { get; set; } = false;

        [Display(Name = "Super Admin")]
        public bool? IsAdministrator { get; set; } = false;
        public bool? IsDisplaySummary { get; set; } = false;


        [Display(Name = "Default User")]
        public bool? IsDefaultUser { get; set; } = false;


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
        public bool? NotifyAutoSchedulesBeforeDispatch { get; set; } = false;
        public bool? NotifyAutoSchedulesAfterDispatch { get; set; } = false;
        public int? AutoNewsletterCount { get; set; }
        public string DB_Path { get; set; }
        public string Serial_Number { get; set; }
        public int? Cust_id { get; set; }
        public string Providerrange { get; set; }
        public bool? IsEditUserEnabled { get; set; }
        public bool? IsEditModuleEnabled { get;set;}
        public bool? IsAssignOfficeEnabled { get; set; }
    }
}

    

