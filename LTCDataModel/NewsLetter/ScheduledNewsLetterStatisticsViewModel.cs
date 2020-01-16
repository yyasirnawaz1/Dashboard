using LTCDataModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LTCDataModel.Newsletter
{
    public class ScheduledNewsLetterStatisticsViewModel
    {
        public int Scheduled { get; set; }
        public int Sent { get; set; }
       public List<ScheduledNewsletterViewModel> ScheduledNewsLetter { get; set; }

    }
   
    public class ScheduledNewsletterViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public DateTime SentTime { get; set; }
        public string SentTimeString { get; set; }
        public DateTime ScheduledDate { get; set; }
        
        public int ScheduledTime { get; set; }
        
        public string ScheduledTimeString { get; set; }
        public ScheduledNewsLetterStatus Status { get; set; }
        public string ScheduledDateString { get; set; }
       

    }
    
}
