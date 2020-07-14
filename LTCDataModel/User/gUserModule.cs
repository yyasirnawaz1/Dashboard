using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataModel.PetaPoco;

namespace LTCDataModel.User
{
	[TableName("businessinfo")]
	[PrimaryKey("Office_Sequence", AutoIncrement = false)]
	public class gUserModule
	{
		public int Office_Sequence { get; set; }
		public short IsDashboardEnabled { get; set; }
		public short IsParadigmCloudEnabled { get; set; }
		public short IsESurveyEnabled { get; set; }
		public short IsEFormEnabled { get; set; }
		public short IsContactListEnabled { get; set; }
		public short IsNewsletterEnabled { get; set; }
		public short IsOfficePortalEnabled { get; set; }
		public short IsSMSEnabled { get; set; }
		public short IsEmailEnabled { get; set; }
	}
}
