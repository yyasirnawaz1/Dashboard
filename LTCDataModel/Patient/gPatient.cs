using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.Patient
{
    public class gPatient
    {
        public int Account { get; set; }
        public int PatientNumber { get; set; }
        public string Doctor { get; set; }
        public string Sex { get; set; }
        public string BirthDate { get; set; }
        public DateTime LastVisit { get; set; }
        public string BestPhone { get; set; }
        public string PhoneNumber { get; set; }
        public string MobilePhone { get; set; }
        public string Title { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Intial { get; set; }
    }
}
