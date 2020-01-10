using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace LTCDataManager.Shared
{
    class gDataFactory
    {
        //private static string ltcDataBase = GetConnectionString("DefaultConnection");

        //public static MySqlConnection GetClientPEDDBConnection()
        //{
        //    return new MySqlConnection(ltcDataBase);
        //}


        //#region "Helpers"
        //private static string GetConnectionString(string name)
        //{
        //    try
        //    {
        //        return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        //    }
        //    catch
        //    {
        //        //if (ConfigurationManager.ConnectionStrings.HasConnectionString(name))
        //            return ConfigurationManager.ConnectionStrings[name].ConnectionString;

        //    }


        //    return string.Empty;
        //}
        //#endregion
    }
}
