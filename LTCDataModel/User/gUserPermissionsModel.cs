using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.User
{
    public class gUserPermissionsModel
    {
        public bool SurveyCreate { get; set; }
        public bool SurveyDelete { get; set; }
        public bool SurveyEdit { get; set; }
        public bool SurveyDuplicate { get; set; }
        public bool SurveyTagCreate { get; set; }
        public bool SurveyAnsweredPreview { get; set; }
        public bool FormCreate { get; set; }
        public bool FormDelete { get; set; }
        public bool FormEdit { get; set; }
        public bool FormDuplicate { get; set; }
        public bool FormTagCreate { get; set; }
        public bool FormAnsweredPreview { get; set; }
        public bool NewsLetterCreate { get; set; }
        public bool NewsLetterEdit { get; set; }
        public bool NewsLetterDelete { get; set; }
        public bool NewsLetterDuplicate { get; set; }




        public string Office_Sequence {get;set;}
        public bool isDisplaySummary { get;set;}
    }
}
