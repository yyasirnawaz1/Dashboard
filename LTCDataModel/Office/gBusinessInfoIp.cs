using System;
using System.Collections.Generic;
using System.Text;

namespace LTCDataModel.Office
{
    public class gBusinessInfoIp
    {
        public int ID { get; set; }
        public string Dental_DB_Name { get; set; }
        public string Dental_DB_IP { get; set; }
        public int Dental_DB_Port { get; set; }
        public string Dental_DB_Type { get; set; }

        public string Gateway_DB_Name { get; set; }
        public string Gateway_DB_IP { get; set; }
        public int Gateway_DB_Port { get; set; }
        public string Gateway_DB_Type { get; set; }

        public string Form_DB_Name { get; set; }
        public string Form_DB_IP { get; set; }
        public int Form_DB_Port { get; set; }


    }
}
