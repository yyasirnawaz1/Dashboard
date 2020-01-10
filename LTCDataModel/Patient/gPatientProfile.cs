using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.Patient
{
    public class gPatientProfile
    {
        public string Address { get; set; }
        public string HomeNumber { get; set; }
        public string WorkPhone { get; set; }
        public string OtherPhone { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string MaritalStatus { get; set; }
        public string InsuranceProvider { get; set; }
    }
}
