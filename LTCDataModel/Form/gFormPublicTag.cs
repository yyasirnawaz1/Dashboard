using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.Form
{
    public class gFormPublicTag
    {
        public int Office_Sequence { get; set; }
        public int TagID { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public string CategoryDescription { get; set; }

        public int TagType { get; set; }
        public int MaxSize { get; set; }

        public string Caption { get; set; }
        public string DataField { get; set; }
        public short IsSurveyForm { get; set; }
    }
}
