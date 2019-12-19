using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.Summary
{
    public class gOfficeSummary
    {
        public string TotalAppointments { get; set; }
        public decimal? TotalRecalls { get; set; }
        public decimal? TotalCharge { get; set; }
        public decimal? TotalChargeCount { get; set; }
        public decimal? TotalPayment { get; set; }
        public decimal? TotalPaymentCount { get; set; }
        public string LastCalculationDate { get; set; }

        public string CalculationDate { get; set; }

    }
}
