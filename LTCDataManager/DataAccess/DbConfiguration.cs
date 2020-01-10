﻿using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using LTCDataModel.PetaPoco;

namespace LTCDataManager.DataAccess
{
    public class DbConfiguration
    {
        public static string LtcGateway
        {
            get
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                var config = builder.Build();
                return config.GetConnectionString("LTCGateway");
            }
        }

        public static string LtcDental
        {
            get
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                var config = builder.Build();
                return config.GetConnectionString("LTCDentalConnection");
            }
        }

        public static string LtcDashboard
        {
            get
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                var config = builder.Build();
                return config.GetConnectionString("LTCDashboard");
            }
        }

    }

    public class PocoDatabase
    {
        public static Database DbConnection(string conn)
        {
            var db = new Database(conn, "MySql") {CommandTimeout = 900};

            return db;
        }
    }
}