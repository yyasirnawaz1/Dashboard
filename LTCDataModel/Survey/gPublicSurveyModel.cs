using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataModel.PetaPoco;

namespace LTCDataModel.Survey
{
	[TableName("survey_form_public")]
	[PrimaryKey("FormID", AutoIncrement = true)]
	public class gPublicSurveyModel
    {
       
        public int FormID { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime SystemDate { get; set; }
        public short IsActive { get; set; }
    }
}
