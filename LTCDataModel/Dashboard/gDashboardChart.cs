using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.Dashboard
{
    public class gDashboardChart
    {
        public string Title { get; set; } //heading to display in chart
        public string Name { get; set; } // ID of chart
        public string Icon { get; set; }

        //public string ChartSequence { get; set; } // sequence will be in tables that are selected by the user, different table
        public int ChartLevel { get; set; }
        public string Path { get; set; } // this will dynamically load the chart from MVC
        public string PageName { get; set; }

        public List<String> ChartTypes { get; set; } // pie, bar, circular, Card (small cards without charts)
        public string Position { get; set; } // top, center , bottom  positions for where the chart will render
        public int Order { get; set; }
    }
}
