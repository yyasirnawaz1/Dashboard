using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.NewsLetter
{
    public class gSubIndustryModel
    {
        public int Id { get; set; }
        public int IndustryTypeId { get; set; }
        public string SubTypeTitle { get; set; }
        public bool IsEmailconfirmation { get; set; }
        public string SubTypeDescription { get; set; }
    }
}
