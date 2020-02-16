using System;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;

namespace Olimpiada2019Judet.DataAcces
{
    public class InitializatorDB
    {
        public static void Initializare(string connectionString)
        {
            ClearDB(connectionString);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string filePath = "Resurse//carti.txt";
                string cmdText = "Insert into carti (id_carte,titlu,autor,gen) values (@id,@titlu,@autor,@gen);";
                using (StreamReader reader = new StreamReader(filePath))
                {
                    int i = 0;
                    while (reader.Peek() >= 0)
                    {
                        i++;
                        var line = reader.ReadLine().Split('*');
                        using (SqlCommand cmd = new SqlCommand(cmdText, con))
                        {
                            cmd.Parameters.AddWithValue("id", i);
                            cmd.Parameters.AddWithValue("titlu", line[0]);
                            cmd.Parameters.AddWithValue("autor", line[1]);
                            cmd.Parameters.AddWithValue("gen", line[2]);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                filePath = "Resurse//utilizatori.txt";
                cmdText = "Insert into utilizatori (email,parola,nume,prenume) values (@email,@parola,@nume,@prenume);";
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (reader.Peek() >= 0)
                    {
                        var line = reader.ReadLine().Split('*');
                        using (SqlCommand cmd = new SqlCommand(cmdText, con))
                        {
                            cmd.Parameters.AddWithValue("email", line[0]);
                            cmd.Parameters.AddWithValue("parola", line[1]);
                            cmd.Parameters.AddWithValue("nume", line[2]);
                            cmd.Parameters.AddWithValue("prenume", line[3]);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                filePath = "Resurse//imprumuturi.txt";
                cmdText = "Insert into imprumut (id_carte,email,data_imprumut) values (@id_carte,@email,@data_imprumut);";
                using (StreamReader reader = new StreamReader(filePath))
                {
                    int i = 0;
                    while (reader.Peek() >= 0)
                    {
                        i++;
                        var line = reader.ReadLine().Split('*');
                        int idCarte = 0;
                        using (SqlCommand cmd = new SqlCommand("Select id_carte from carti where titlu = @titlu", con))
                        {
                            cmd.Parameters.AddWithValue("titlu", line[0]);

                            using (SqlDataReader rdr = cmd.ExecuteReader())
                            {
                                rdr.Read();
                                idCarte = (int)rdr[0];
                            }
                        }

                        using (SqlCommand cmd = new SqlCommand(cmdText, con))
                        {
                            cmd.Parameters.AddWithValue("id_carte", idCarte);
                            cmd.Parameters.AddWithValue("email", line[1]);
                            string date = line[2].Trim();
                            cmd.Parameters.AddWithValue("data_imprumut", DateTime.ParseExact(date, "M/d/yyyy", CultureInfo.InvariantCulture));

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private static void ClearDB(string connectionString)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string cmdText = "Delete from carti";
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.ExecuteNonQuery();
                }
                cmdText = "Delete from utilizatori";
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.ExecuteNonQuery();
                }
                cmdText = "Delete from imprumut";
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}