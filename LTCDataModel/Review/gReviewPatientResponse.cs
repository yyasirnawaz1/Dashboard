using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.Review
{
    public class gReviewPatientResponse
    {
        public int Office_Sequence { get; set; }
        public int Patient_number { get; set; }
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }
        public string ReviewBody { get; set; }
        public string ReviewType { get; set; }
        public string ReviewerName { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
         

    }
}
