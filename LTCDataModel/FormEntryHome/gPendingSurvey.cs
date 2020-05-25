using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.FormEntryHome
{
    public class gPendingSurveyModel
    {
        public int? PatientNumber { get; set; }
        public int SavedFormID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Content { get; set; }
        public DateTime SystemDate { get; set; }
    }
    public class gPendingSurveyViewModel
    {
        public int? PatientNumber { get; set; }
        public int SavedFormID { get; set; }
        public string PatientName { get; set; }
        public string Content { get; set; }
        public string SystemDate { get; set; }
    }
}
