using LTCDataModel.PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.NewsLetter
{
    [TableName("templates")]
    [PrimaryKey("ID", AutoIncrement = true)]
    public class gSavePredefinedTemplate
    {
        public int TemplateID { get; set; }
        public string TemplateSourceMarkup { get; set; }
        public string ThumbnailPath { get; set; }
        public string TemplateTitle { get; set; }
        public int TypeID { get; set; }
        public int OfficeNo { get; set; }
        public int DocID { get; set; }
        public int IndustryID { get; set; }
        public bool IsParadigmNewsletter { get; set; }
    }
}
