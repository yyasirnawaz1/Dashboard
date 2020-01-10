using LTCDataModel.PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.Survey
{
    [TableName("survey_catagory")]
    [PrimaryKey("CategoryID", AutoIncrement = true)]
    public class gSurveyCategory
    {
        public int CategoryID { get; set; }
        public string Survey_Tag_Type { get; set; }
        public string Description { get; set; }
    }
}
