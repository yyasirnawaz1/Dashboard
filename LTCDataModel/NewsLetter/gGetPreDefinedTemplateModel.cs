using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataModel.PetaPoco;


namespace LTCDataModel.NewsLetter
{
    public class gGetPreDefinedTemplateModel
    {
        public int TemplateID { get; set; }
        public string TemplateSourceMarkup { get; set; }
        public string ThumbnailPath { get; set; }
        public string TemplateTitle { get; set; }
        public string TypeName { get; set; }
        public int OfficeNo { get; set; }
        public int DocID { get; set; }
        public int IndustryID { get; set; }
        public DateTime ModificationDate { get; set; }
    }
    [TableName("system_articles")]
    [PrimaryKey("ArticleID", AutoIncrement = true)]
    public class gArticleModel
    {
        public int ArticleID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ContentWithDefaultStyle { get; set; }
        
        public DateTime ModificationDate { get; set; }
    }
}
