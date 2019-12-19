using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.FormEntryHome
{
    public class gSearchPatient
    {
        public int Account { get; set; }
        public int PatientNumber { get; set; }
        public int Office_Sequence { get; set; }
        public string Doctor { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Sex { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime LastVisit { get; set; }
        public string BestPhone { get; set; }
        public string PhoneNumber { get; set; }
        public string MobilePhone { get; set; }
    }
    public class gSearchPatientViewModel
    {
        public int Account { get; set; }
        public int PatientNumber { get; set; }
        public int Office_Sequence { get; set; }
        public string Doctor { get; set; }
        public string Name { get; set; }
        public string Sex_Age { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string Birthdate { get; set; }
        public string LastVisit { get; set; }
    }
}
