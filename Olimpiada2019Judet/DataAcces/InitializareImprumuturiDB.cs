using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace Olimpiada2019Judet.DataAcces
{
    public class InitializareImprumuturiDB
    {
        public static DataTable GetTable(string connectionString)
        {
            DataTable tb = new DataTable();
            
            tb.Columns.Add("id_carte");
            tb.Columns.Add("titlu");
            tb.Columns.Add("autor");
            tb.Columns.Add("gen");
            //tb.Columns.Add("Imprumuta");

            //tb.Columns["Imprumuta"].DataType = typeof(DataGridViewButtonCell);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string cmdText = "Select * from carti";
                using (SqlCommand cmd = new SqlCommand(cmdText,con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(tb);
                    }
                }
            }


            //for (int i = 0; i < tb.Rows.Count; i++)
            //{
            //    tb.Rows[i].
            //}


            return tb;
        }
    }
}
