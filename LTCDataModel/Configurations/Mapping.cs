using System;
using System.Collections.Generic;
using System.Text;

namespace LTCDataModel.Configurations
{
    public class Mapping
    {
        public List<Maps> Map { get; set; }
    }

    public class Maps
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public string Action { get; set; }
    }
}
