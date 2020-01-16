using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.Office
{
    public class gOffice
    {
        public string Name { get; set; }
        public int Office_Sequence { get; set; }
    }
    public class gOfficeInfo
    {
        public int DoctorID { get; set; }
        public int Office_Number { get; set; }
        public string Business_Name { get; set; }
        public string Phone{ get; set; }
        public string Fax{ get; set; }
        public string AddressLine1{ get; set; }
        public string AddressLine2{ get; set; }
        public string AddressLine3{ get; set; }
        public string City{ get; set; }
        public string Province{ get; set; }
        public string Country{ get; set; }
        public string PostalCode{ get; set; }
        public string Contact{ get; set; }
        public string OfficeEmailAddress { get; set; }
    }
    public class gPatientOfficeInfo
    {
        public int DoctorID { get; set; }
        public int Office_Number { get; set; }
        public string Provider { get; set; }
        public string Salutation{ get; set; }
        public string LastName{ get; set; }
        public string FirstName{ get; set; }
        public string Phone{ get; set; }
        public string Fax{ get; set; }
        public string AddressLine1{ get; set; }
        public string AddressLine2{ get; set; }
        public string AddressLine3{ get; set; }
        public string City{ get; set; }
        public string Province{ get; set; }
        public string Country{ get; set; }
        public string PostalCode{ get; set; }
        public string Email { get; set; }

        public string AppointmentDate{ get; set; }
        public string AppointmentTime{ get; set; }
        public string Name{ get; set; }
  
    }
}
