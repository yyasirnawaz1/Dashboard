using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataModel.PetaPoco;

namespace LTCDataModel.Form
{
	[TableName("form_public")]
	[PrimaryKey("FormID", AutoIncrement = true)]
	public class gPublicFormModel
    {
       
        public int FormID { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime SystemDate { get; set; }
        public short IsActive { get; set; }
        public short IsSurveyForm { get; set; }
    }
}
