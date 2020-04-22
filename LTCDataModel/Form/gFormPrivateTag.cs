using LTCDataModel.PetaPoco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.Form
{
    [TableName("form_tag_private")]
    [PrimaryKey("TagID", AutoIncrement = true)]
    public class gFormPrivateTag
    {
        public int Office_Sequence { get; set; }
        public int TagID { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public short IsSurveyForm { get; set; }
    }
}
