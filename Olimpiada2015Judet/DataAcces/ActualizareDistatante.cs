using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;

namespace Olimpiada2015Judet.DataAcces
{
    public class ActualizareDistatante
    {
        public static void Actualizeaza(string connectionString)
        {
            SqlDataAcces.ClearDB(connectionString, "Distante");
            List<string> porturi = SqlDataAcces.NumePorturi;

            string filePath = "Resurse//Harta_Distantelor.txt";
            using (StreamReader reader = new StreamReader(filePath))
            {
                foreach (string item in porturi)
                {
                    List<int> line = reader.ReadLine().Split(' ').Select(x=>Int32.Parse(x)).ToList();

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string cmdText = "Insert into Distante (ID_Port, ID_Port_Destinatie, Nume_Port_Destinatie,Distanta) values (@id1, @id2, @nume, @dist);";

                        int index = 1;
                        foreach (int dist in line)
                        {
                            using (SqlCommand cmd = new SqlCommand(cmdText,con))
                            {
                                cmd.Parameters.AddWithValue("id1", porturi.IndexOf(item) + 1);
                                cmd.Parameters.AddWithValue("id2", index);
                                cmd.Parameters.AddWithValue("nume", porturi[index]);
                                cmd.Parameters.AddWithValue("dist", dist);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

            }
        }
    }
}
