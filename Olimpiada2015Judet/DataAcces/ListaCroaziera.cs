using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Olimpiada2015Judet.DataAcces
{
    public class ListaCroaziera
    {
        public static DataTable GetCroaziere(string connectionString)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string cmdText = "Select * from Croaziere;";

                using (SqlCommand cmd = new SqlCommand(cmdText,con))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();

                        adapter.Fill(table);

                        return table;
                    }
                }
            }

            throw new NullReferenceException();
        }
    }
}
