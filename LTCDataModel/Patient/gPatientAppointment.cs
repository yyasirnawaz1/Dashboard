using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.Patient
{
    public class gPatientAppointment
    {
        public string InvoiceDate { get; set; }
        public string Provider { get; set; }
        public string Procedure { get; set; }
        public string Status { get; set; }
        public int Fee { get; set; }
    }
}
