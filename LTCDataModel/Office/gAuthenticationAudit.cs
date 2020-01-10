using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LTCDataModel.PetaPoco;

namespace LTCDataModel.Office
{
	[TableName("authentication_audit")]
	[PrimaryKey("User_ID", AutoIncrement = false)]
	public class gAuthenticationAudit
	{

		public int User_ID { get; set; }
		public int Counter { get; set; }
		public DateTime Audit_Date_Time { get; set; }
		public int Audit_Action { get; set; }
		public string Audit_From_IP_Address { get; set; }

	}
}