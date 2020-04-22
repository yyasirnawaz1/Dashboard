using LTCDataModel.PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.Form
{
    [TableName("form_catagory")]
    [PrimaryKey("CategoryID", AutoIncrement = true)]
    public class gFormCategory
    {
        public int CategoryID { get; set; }
        public string form_Tag_Type { get; set; }
        public string Description { get; set; }
        public short IsSurveyForm { get; set; }
    }
}
