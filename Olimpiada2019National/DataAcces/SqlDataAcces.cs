using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;

namespace Olimpiada2019National.DataAcces
{
    public class SqlDataAcces
    {
        public static string ConnectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Biblioteca.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";

        public static void InitiateDB()
        {
            List<string> tables = new string[] { "carti", "imprumuturi", "rezervari", "utilizatori" }.ToList();

            tables.ForEach(tableName => InitiateTable(tableName));
        }

        private static void TruncateTable(string tableName)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string cmdText = "Truncate table " + tableName;
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static void InitiateTable(string tableName)
        {
            TruncateTable(tableName);

            string filePath = "Resurse//" + tableName + ".txt";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (reader.Peek() >= 0)
                    {
                        List<string> line = reader.ReadLine().Split(';').ToList();

                        string cmdText = "Insert into " + tableName + " values ( ";

                        List<string> tokens = new List<string>();
                        for (int i = 0; i < line.Count; i++)
                        {
                            string value = line[i];

                            int x;
                            DateTime date;

                            if (Int32.TryParse(value, out x) || value.ToLower() == "null")
                            {
                                tokens.Add(value);
                            }
                            else if (DateTime.TryParseExact(value,"MM/dd/yyyy hh/mm/ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                            {
                                tokens.Add(String.Format("Convert( datetime, '{0}')", date));
                            }
                            else
                            {
                                tokens.Add(String.Format("' {0} '", value));
                            }

                        }
                        cmdText += string.Join(" , ", tokens) + ");";

                        using (SqlCommand cmd = new SqlCommand(cmdText, con))
                        {
                            cmd.ExecuteNonQuery();
                        }

                    }
                }
            }

        }
    }
}
