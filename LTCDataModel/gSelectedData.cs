using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCDataModel
{
    public class gSelectedData
    {
        public string type { get; set; }
        public string label { get; set; }
        public string className { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public string required { get; set; }
        public List<gValues> values { get; set; }
        public List<string> userData { get; set; }
        public string subtype { get; set; }
    }

    public class gValues
    {
        public string label { get; set; }
        public string value { get; set; }
    }
}
