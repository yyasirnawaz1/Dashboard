using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.NewsLetter
{
    public class gGetUserDefinedTemplateModel
    {
        public int LetterID { get; set; }

        public string TemplateTitle { get; set; }
        public string TemplateSourceMarkup { get; set; }
        public string MainBodymarkup { get; set; }
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public int Office_Number { get; set; }
        public int Branch_number { get; set; }
        public int DoctorID { get; set; }
        public int IndustryID { get; set; }
        public string ThumbnailPath { get; set; }
        public int IndustrySubTypeID { get; set; }
        public int IndustrySubTitleID { get; set; }
        public bool IsParadigmNewsletter { get; set; }
        public bool IsDefault { get; set; }
        public DateTime ModificationDate { get; set; }

    }
    public class DeleteModel
    {
        public string file { get; set; }
    }
    public class NewsletterViewDeleteModel
    {
        public int tempId { get; set; }
    }
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
    }
}
