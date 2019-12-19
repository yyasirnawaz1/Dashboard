using LTCDataModel.PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel.Dashboard
{
    [TableName("user_charts")]
    [PrimaryKey("ID", AutoIncrement = true)]
    public class gUserCharts
    {
        public int ID { get; set; }
        public int User_Id { get; set; }
        public int Chart_Id { get; set; }
        public string FilterTypes { get; set; }

    }
}
