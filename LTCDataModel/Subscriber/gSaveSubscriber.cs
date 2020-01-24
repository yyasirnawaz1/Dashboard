using LTCDataModel.PetaPoco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.Subscriber
{
    [TableName("subscribers")]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class gSaveSubscriber
    {
        public int Id { get; set; }

        public int DoctorID { get; set; }

        [Required(ErrorMessage = "Name Required")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleInitial { get; set; }

        [Required(ErrorMessage = "Email Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email address")]
        public string EmailAddress { get; set; }

        public SubscriptionStatus SubscriptionStatus { get; set; }

        [DisplayFormat(DataFormatString = "{0:dddd, mmmm dd, yyyy hh:mm:ss tt}")]
        public DateTime LastSubscriptionStatusUpdated { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime AddedOn { get; set; }

        public int Office_Sequence { get; set; }
        
        public int PatientNumber { get; set; }
    }
    public class IdModel
    {
        public int Id { get; set; }
    }
}
