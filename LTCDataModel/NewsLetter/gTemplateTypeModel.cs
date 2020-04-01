using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.NewsLetter
{
    public class gTemplateTypeModel
    {
        public int TypeID { get; set; }
        public int OfficeId { get; set; }
        public int TemplateId { get; set; }
        public string TypeDescription { get; set; }
        public string TypeName { get; set; }
    }
    public class gArticleCategories
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
