using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using LTCDataModel.PetaPoco;
using Microsoft.AspNetCore.Identity;

namespace LTCDataModel.User
{
    [TableName("authentication")]
    [PrimaryKey("DoctorID", AutoIncrement = true)]
    public class ApplicationUser : IdentityUser<int>
    {
        [Display( Name ="Office No.")]
        public int? Office_Number { get; set; } 

        [Required]
        [Display(Name = "Office Sequence")]
        public int Office_Sequence { get; set; }

        [Display(Name = "Branch No.")]
        public int? Branch_Number { get; set; }
        
        [MaxLength(20)]
        public string AuthenticationPhone { get; set; }

        [MaxLength(10)]
        public string Provider { get; set; }
        [MaxLength(100)]
        public string Salutation { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50)]
        public string LastName { get; set; }
        
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MaxLength(10)]
        public string Initials { get; set; }

        [MaxLength(10)]
        public string Fax { get; set; }

        [MaxLength(200)]
        public string AddressLine1 { get; set; }
        
        [MaxLength(200)]
        public string AddressLine2 { get; set; }

        [MaxLength(200)]
        public string AddressLine3 { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string Province { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }

        [MaxLength(10)]
        public string PostalCode { get; set; }

        [Display(Name = "System Admin")]
        public bool? IsSystemAdministrator { get; set; } = false;

        [Display(Name = "Office Admin")]
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

        [MaxLength(50)]
        public string WebsiteURL { get; set; }

        [MaxLength(50)]
        public string ActivationStatus { get; set; }

        [MaxLength(50)]
        public string LanguageSelected { get; set; }

        [MaxLength(20)]
        public string DateFormat { get; set; }

        public int? SelectedTemplateId { get; set; }

        [MaxLength(20)]
        public string SelectedMainTitle_Name_ClinicName { get; set; }
        public string PreferedSubIndustriesCSV { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FirstNewsletterDate { get; set; }

        public bool? NotifyAutoSchedulesBeforeDispatch { get; set; } = false;
        public bool? NotifyAutoSchedulesAfterDispatch { get; set; } = false;
        public int? AutoNewsletterCount { get; set; }

        [MaxLength(100)]
        public string DB_Path { get; set; }

        [MaxLength(10)]
        public string Serial_Number { get; set; }


        public int? Cust_id { get; set; }

        [MaxLength(45)]
        public string Providerrange { get; set; }
    }
}
