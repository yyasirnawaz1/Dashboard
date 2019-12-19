using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTCOfficePortal.Models
{
    public class QueueEntryViewModel
    {
        public int AppointmentCounter { get; set; }
        public int FormID { get; set; }
        public int Type { get; set; }

        public string PatientNumber { get; set; }
        public int Office_Sequence { get; set; }
    }
}
