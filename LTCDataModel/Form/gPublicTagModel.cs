using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataModel.PetaPoco;

namespace LTCDataModel.Form
{
    [TableName("form_tag_public")]
    [PrimaryKey("TagID", AutoIncrement = true)]
    public class gPublicTagModel
    {
        public int TagID { get; set; }
        public int CategoryID { get; set; }
        public string Description { get; set; }
        public string Caption { get; set; }
        public string DataField { get; set; }
        public int TagType { get; set; }
        public int MaxSize { get; set; }

        public short IsSurveyForm { get; set; }
    }
}
