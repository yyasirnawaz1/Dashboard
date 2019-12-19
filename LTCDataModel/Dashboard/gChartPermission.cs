using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.Dashboard
{
    public class gChartPermission
    {
        public int ID { get; set; }
        public int Office_Sequence { get; set; }
        public int User_Id { get; set; }
        public int? Permission_Level { get; set; }
        public int? Permission_Group { get; set; }
        public string Permission_Group_Name { get; set; }
        public int? Group_Permission_Level { get; set; }

    }
}
