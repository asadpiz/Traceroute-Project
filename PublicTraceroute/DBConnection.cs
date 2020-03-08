
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Collections.Generic;

    public class DBConnection
{
        public string connectionString;

        public DBConnection()
        {
            connectionString = WebConfigurationManager.ConnectionStrings["TraceRoute"].ConnectionString;
        }
        public DBConnection(string connectionString)
        {
            this.connectionString = connectionString;
        }
}
