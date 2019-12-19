using LTCDataModel.PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTCDataModel.Office
{
	
	[TableName("authentication_office_list")]
	
	public class gOfficelist
	{
		public int DoctorID { get; set; }
		public int Office_Number { get; set; }
		public int Branch_Number { get; set; }
		public string Providerrange { get; set; }
		public string Office_Sequence { get; set; }
		public string IP_Address { get; set; }
		public string DB_Name { get; set; }
        public string DB_Port { get; set; }


		
	}
}