using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataModel.PetaPoco;

namespace LTCDataModel.User
{
	[TableName("authentication_module")]
	[PrimaryKey("Office_Sequence", AutoIncrement = false)]
	public class gUserModule
	{
		public int Office_Sequence { get; set; }
		public short IsDashboardOk { get; set; }
		public short IsOfficeManagementOk { get; set; }
		public short IsSurveyOk { get; set; }
		public short IsFormOk { get; set; }
		public short IsReviewOk { get; set; }
		public short IsNewsletterOk { get; set; }
		public short IsReportsOk { get; set; }
		public short IsSmsOk { get; set; }
		public int IsDashboardEnabled { get; set; }
		public int IsESurveyEnabled { get; set; }
		public int IsEFormEnabled { get; set; }
		public int IsContactListEnabled { get; set; }
		public int IsNewsletterEnabled { get; set; }
		public int IsOfficePortalEnabled { get; set; }
		public int IsSMSEnabled { get; set; }
	}
}
