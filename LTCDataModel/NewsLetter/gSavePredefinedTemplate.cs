using LTCDataModel.PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.NewsLetter
{
    [TableName("newsletter_systemtemplate")]
    [PrimaryKey("ID", AutoIncrement = true)]
    public class gSavePredefinedTemplate
    {
        public int ID { get; set; }
        public string ThumbnailPath { get; set; }
        public string Title { get; set; }
        public int ShellTemplateId { get; set; }
        public string Markup { get; set; }
        public int Office_Sequence { get; set; }
        public int IndustryId { get; set; }
        public int SubIndustryId { get; set; }
    }
}
