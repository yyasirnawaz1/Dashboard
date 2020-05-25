using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataModel.PetaPoco;

namespace LTCDataModel.Survey
{
	[Obsolete]
	[TableName("_form_saved")]
	[PrimaryKey("SavedFormID", AutoIncrement = true)]
	public class gSurveySavedModel
	{
		public int Office_Sequence { get; set; }
		public int SavedFormID { get; set; }
		public string Content { get; set; }
		public DateTime SystemDate { get; set; }
		public int FormID { get; set; }
		public int PatientNumber { get; set; }
		
	}
}
