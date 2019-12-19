using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.Dashboard
{
    public class gBreakdown
    {
        public int Office_Sequence { get; set; }
        public string OfficeName { get; set; }
        public int PatientNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceDateString
        {
            get
            {
                return InvoiceDate.ToShortDateString();
            }
        }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public decimal PatAmount { get; set; }
        public decimal InsAmount { get; set; }
        public string Provider { get; set; }
        public string InvoiceType { get; set; }
        public string InvoiceTypeDetail
        {
            get
            {
                if (InvoiceType == "1")
                    return "Treatment";
                else if (InvoiceType == "C")
                    return "Credit";
                else
                    return InvoiceType;
            }
        }

        public decimal TotalAmount
        {
            get
            {
                return PatAmount + InsAmount;
            }
        }
    }
}
