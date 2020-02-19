using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Drawing;
using Olimpiada2015Judet.DataAcces;

namespace Olimpiada2015Judet.Forms
{
    public class InitializarePorturi
    {
        public static void Salvare(string connectionString, List<Point> porturi)
        {
            List<string> numePorturi = SqlDataAcces.NumePorturi;


            SqlDataAcces.ClearDB(connectionString,"Porturi");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();


                string cmdText = "Insert into Porturi ( Nume_port, Pozitie_X, Pozitie_Y) values (@nume, @x, @y);";

                int index = 0;
                foreach (Point item in porturi)
                {

                    using (SqlCommand cmd = new SqlCommand(cmdText, con))
                    {
                        cmd.Parameters.AddWithValue("nume", numePorturi[index]);
                        cmd.Parameters.AddWithValue("x", item.X);
                        cmd.Parameters.AddWithValue("y", item.Y);

                        cmd.ExecuteNonQuery();
                    }
                    index++;
                }
            }
        }

    }

}
