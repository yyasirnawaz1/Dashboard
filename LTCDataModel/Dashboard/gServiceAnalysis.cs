using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.Dashboard
{
    //public class gServiceAnalysisChart
    //{
    //    public decimal TotalAmount { get; set; }
    //    public string ServiceCode { get; set; }
    //    public string CodeCategory { get; set; }
    //}
    public class gServiceAnalysis
    {
        public int Office_Sequence { get; set; }
        public string InvoiceNumber { get; set; }

        public decimal InsAmount { get; set; }
        public decimal PatAmount { get; set; }
        public string ServiceCode { get; set; }
        public string CodeCategory { get; set; }
        public string Provider { get; set; }
        public string ProviderName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceDateString
        {
            get
            {
                return InvoiceDate.ToShortDateString();
            }
        }

        public int Count { get; set; }
    }

    public class gCode
    {
        public int CodeCounter { get; set; }
        public string Code { get; set; }
        public string Line1 { get; set; }

        public int? Line1Int
        {
            get
            {
                return string.IsNullOrEmpty(Line1) ? (int?)null : Convert.ToInt32(Line1);
            }
        }
        public string Line2 { get; set; }
        public int? Line2Int
        {
            get
            {
                return string.IsNullOrEmpty(Line2) ? (int?)null : Convert.ToInt32(Line2);
            }
        }
        public int Office_Sequence { get; set; }
    }
}
