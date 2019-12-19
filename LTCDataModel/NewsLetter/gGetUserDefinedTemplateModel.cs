using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.NewsLetter
{
    public class gGetUserDefinedTemplateModel
    {
        public int ID { get; set; }
        public string ThumbnailPath { get; set; }
        public int IsSystemTemplate { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public int ShellTemplateId { get; set; }
        public string Markup { get; set; }
        public int Office_Sequence { get; set; }
        public int BranchId { get; set; }
        public int IndustryId { get; set; }
        public int SubIndustryId { get; set; }
    }
}
