using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataModel.PetaPoco;

namespace LTCDataModel.Survey
{
	[TableName("survey_form_private")]
	[PrimaryKey("FormID", AutoIncrement = true)]
	public class gPrivateSurveyModel
	{
		public int Office_Sequence { get; set; }
		public int FormID { get; set; }
		public string Description { get; set; }
		public string Content { get; set; }
		public DateTime SystemDate { get; set; }
		public short IsActive { get; set; }
		public short IsInUsed { get; set; }

		/*
		 * Existing Class Definition
		 * Changed By Yousaf 01/Nov/2018
		public string Office_Sequence { get; set; }
		public string FormID { get; set; }
		public string Description { get; set; }
		public string Content { get; set; }
		public string SystemDate { get; set; }
		public string IsActive { get; set; }
		*/
	}
}
