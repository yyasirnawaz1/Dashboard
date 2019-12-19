using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.Survey
{
    public class gSurveyAnswerWithPrivateSurvey
    {

        public int Office_Sequence { get; set; }
        public int SavedFormID { get; set; }
        public string Content { get; set; }
        public DateTime SystemDate { get; set; }
        public int FormID { get; set; }
        public int PatientNumber { get; set; }

        //public string SurveyContent { get; set; }
        public string SurveyDescription { get; set; }

    }
}
