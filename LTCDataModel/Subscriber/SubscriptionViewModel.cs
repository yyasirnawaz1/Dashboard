using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace LTCDataModel.Subscriber
{
    public enum SubscriptionStatus
    {  // do not change order
        UnSubscribed,
        Subscribed

    }
    public class SubscriberFilterParams
    {
        public string Office_Sequence { get; set; }
    }
    public class ResponseViewModel
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }
    public class SubscriptionViewModel
    {
        //public List<Subscription> Subscriptions { get; set; }

        public int Id { get; set; }
        public int Office_Sequence{ get; set; }
        public string DoctorId { get; set; }

        [Required(ErrorMessage = "Name Required")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleInitial { get; set; }

        [Required(ErrorMessage = "Email Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email address")]
        public string EmailAddress { get; set; }

        public bool SubscriptionStatus { get; set; }

        [DisplayFormat(DataFormatString = "{0:dddd, mmmm dd, yyyy hh:mm:ss tt}")]
        public DateTime LastSubscriptionStatusUpdated { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime AddedOn { get; set; }

        public int PatientId { get; set; }

        

    }
}
