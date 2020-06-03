using LTCDataModel.PetaPoco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LTCDataModel.Covid
{
    [TableName("form_covid_entry")]
    [PrimaryKey("QueueID", AutoIncrement = true)]
    public class gFormCovidEntry
    {

        public int BusinessInfo_ID { get; set; }
        public int QueueID { get; set; }
        public int FormID { get; set; }
        public int SubscriberID { get; set; }
        public short IsPreScreen { get; set; }
        public DateTime PreScreenDate { get; set; }
        public short IsInPersonScreen { get; set; }
        public DateTime InPersonScreenDate { get; set; }
        public string StorageInJson { get; set; }
        
    }

    [TableName("form_covid_type")]
    [PrimaryKey("ID", AutoIncrement = true)]
    public class gFormCovidType
    {

        public int ID { get; set; }
        public string Covid_Form_Description { get; set; }

    }

    [TableName("subscribers")]
    [PrimaryKey("ID", AutoIncrement = true)]
    public class gCovidSubscriber
    {
        public int ID { get; set; }
        public int BusinessInfo_ID { get; set; }
        
        public string Salutation { get; set; }

        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleInitial { get; set; }

        [Required(ErrorMessage = "Email Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email address")]
        public string EmailAddress { get; set; }

        public bool SubscriptionStatus { get; set; }

        [DisplayFormat(DataFormatString = "{0:dddd, mmmm dd, yyyy hh:mm:ss tt}")]
        public DateTime LastSubscriptionStatusUpdated { get; set; }

        public string CustomID { get; set; }
    }
}
