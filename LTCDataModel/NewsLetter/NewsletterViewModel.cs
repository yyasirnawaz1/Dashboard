using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.Newsletter
{
    public class NewsletterViewModel
    {
        //scheduling section
        public DateTime ScheduledDateTime { get; set; }
        public bool SendToSubscribers { get; set; }
        public string Email { get; set; }

        public int TemplateId { get; set; }


        //copy system newsletter
        public string Title { get; set; }

        public string UserId { get; set; }

        //save section
        public int TemplateTypeId { get; set; }

        public int ShellTemplateId { get; set; }

        public string Markup { get; set; }

        public int OfficeId { get; set; }

        public int BranchId { get; set; }

        public int IndustryId { get; set; }

        public int SubIndustryId { get; set; }
       // public string Offset { get; set; }

    }
}
