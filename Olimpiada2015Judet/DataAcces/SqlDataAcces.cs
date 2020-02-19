using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Olimpiada2015Judet.DataAcces
{
    public class SqlDataAcces
    {
        private static string _connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\DBTimpSpatiu.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
        public static string Connectionstring { get { return _connectionString; } }

        public static List<string> NumePorturi { get { return new string[] { "Constanta", "Varna", "Burgas", "Istambul", "Kozlu", "Samsun", "Batumi", "Sokhumi", "Soci", "Anapa", "Yalta", "Sevastopol", "Odessa" }.ToList(); } }


        public static void ClearDB(string connectionString, string tableName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string cmdText = "TRUNCATE TABLE "+tableName;
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
