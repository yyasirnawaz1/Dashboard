using LTCDataModel.PetaPoco;using System;using System.Collections.Generic;using System.ComponentModel.DataAnnotations;using System.Text;namespace LTCDataModel.Covid{


    [TableName("form_covid_entry")]    [PrimaryKey("QueueID", AutoIncrement = true)]    public class gFormCovidEntry    {        public int BusinessInfo_ID { get; set; }        public int QueueID { get; set; }        public int FormID { get; set; }        public int SubscriberID { get; set; }        public Boolean IsPreScreen { get; set; }        public DateTime? PreScreenDate { get; set; }        public Boolean IsInPersonScreen { get; set; }        public DateTime? InPersonScreenDate { get; set; }        public string StorageInJson { get; set; }        public string CustomID { get; set; }        [Ignore]        public string PatientName { get; set; }        [Ignore]        public string Email { get; set; }

        public Boolean IsCOVIDPossible { get; set; }        public int Counter { get; set; }        public int FormAction { get; set; }    }


    [TableName("form_covid_type")]
    [PrimaryKey("ID", AutoIncrement = true)]
    public class gFormCovidType
    {

        public int ID { get; set; }
        public int Form_ID { get; set; }
        public string Covid_Form_Description { get; set; }
        public int? FormAction { get; set; }

    }
    public class IdModel
    {
        public string Id { get; set; }
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
        public int Office_Sequence { get; set; }
        public int PatientNumber { get; set; }


        public DateTime LastSubscriptionStatusUpdated { get; set; }

        public string CustomID { get; set; }
        public string CellPhone { get; set; }
    }
    public class gCovidSubscriberView
    {
        public string ID { get; set; }
        public string BusinessInfo_ID { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public string EmailAddress { get; set; }
        public bool SubscriptionStatus { get; set; }
        public string Office_Sequence { get; set; }
        public string PatientNumber { get; set; }
        public DateTime LastSubscriptionStatusUpdated { get; set; }
        public string CustomID { get; set; }

    }

    public class gFormCovidEntryViewModel
    {

        public int BusinessInfo_ID { get; set; }
        public int QueueID { get; set; }
        public int FormID { get; set; }
        public string CustomID { get; set; }
        public int SubscriberID { get; set; }
        public bool IsPreScreen { get; set; }
        public DateTime PreScreenDate { get; set; }
        public bool IsInPersonScreen { get; set; }
        public DateTime InPersonScreenDate { get; set; }
        public byte[] StorageInJson { get; set; }
        public string StorageInJsonView { get; set; }
        public string LoggedInUser { get; set; }
        public string Covid_Form_Description { get; set; }

        public string Salutation { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleInitial { get; set; }
        public string EmailAddress { get; set; }
        public bool IsCOVIDPossible { get; set; }
        [Ignore]
        public string ContactMethod { get; set; }
        
        public int Counter { get; set; }

        public string FormAction { get; set; }
        [Ignore]
        public bool IsAlreadySave { get; set; }

        public bool IsViewed { get; set; }
        
    }
    public class gEmailModel
    {
        public string Id { get; set; }
        public string QueueId { get; set; }

    }

    public class gformInPdfInputModel
    {

        public int FromTable { get; set; }
        public int QueueID { get; set; }
        public string PDF { get; set; }

    }

    [TableName("form_in_pdf")]
    [PrimaryKey("ID", AutoIncrement = true)]
    public class gformInPdf
    {

        public int ID { get; set; }
        public int FromTable { get; set; }
        public int QueueID { get; set; }
        public byte[] PDF { get; set; }

    }

    public class gAPIFormCovidEntryViewModel
    {

        public int BusinessInfo_ID { get; set; }
        public int QueueID { get; set; }
        public int FormID { get; set; }
        public string CustomID { get; set; }
        public bool IsPreScreen { get; set; }
        public DateTime PreScreenDate { get; set; }
        public bool IsInPersonScreen { get; set; }
        public DateTime InPersonScreenDate { get; set; }
        public byte[] StorageInJson { get; set; }
        public bool IsCOVIDPossible { get; set; }

        public int Counter { get; set; }

        public string FormAction { get; set; }

        public bool IsViewed { get; set; }

    }
}