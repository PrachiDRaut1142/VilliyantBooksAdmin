using Freshlo.RI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.Repository
{
    public class DbConfig :IDbConfig
    {
        public string ConnectionString { get; set; }
        public string BusinessInfo { get; set; }


        public DbConfig(string connectionString,string businessInfo)
        {
            ConnectionString = connectionString;
            BusinessInfo = businessInfo;

        }
    }
}
