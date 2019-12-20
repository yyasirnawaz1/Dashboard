using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.Dashboard
{
    public class gCharts
    {
        public int ID { get; set; }
        public string Title { get; set; } //heading to display in chart
        public string Name { get; set; } // ID of chart
        public string Icon { get; set; }
        public string Page_Name { get; set; }
        public string Chart_Type { get; set; } // pie, bar, circular, Card (small cards without charts)
        public string Position { get; set; } // top, center , bottom  positions for where the chart will render
        public int? Order { get; set; }
        public int? Required_Permission_Level { get; set; }

        public string FilterTypes { get; set; }
    }
}
