using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Olimpiada2015Judet.Models;
using System.Drawing;

namespace Olimpiada2015Judet.DataAcces
{
    public class SqlDataAcces
    {
        private static string _connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\DBTimpSpatiu.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
        public static string ConnectionString { get { return _connectionString; } }

        public static List<string> NumePorturi { get { return new string[] { "Constanta", "Varna", "Burgas", "Istambul", "Kozlu", "Samsun", "Batumi", "Sokhumi", "Soci", "Anapa", "Yalta", "Sevastopol", "Odessa" }.ToList(); } }


        public static void ClearDB(string connectionString, string tableName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string cmdText = "TRUNCATE TABLE " + tableName;
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<List<DistantaModel>> GetDistante()
        {
            List<List<DistantaModel>> distante = new List<List<DistantaModel>>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string cmdText = "Select * from Distante";
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        for (int i = 0; i < 13; i++)
                        {
                            List<DistantaModel> linie = new List<DistantaModel>();
                            reader.Read();

                            for (int j = 0; j < 13; j++)
                            {
                                DistantaModel item = new DistantaModel
                                {
                                    Id_Port = (int)reader["ID_Port"],
                                    Id_destinatie = (int)reader["ID_Port_Destinatie"],
                                    NumeDestinatie = (string)reader["Nume_Port_Destinatie"],
                                    Distanta = (int)reader["Distanta"]
                                };
                                linie.Add(item);
                            }
                            distante.Add(linie);
                        }
                    }
                }
            }

            return distante;
        }

        public static void SalveazaCroaziera(int idCroaziera, int tipCroaziera, List<int> listaPorturi, int pret, int distanta)
        {
            string listaStringPorturi = String.Join(",", listaPorturi.Select(x => x.ToString()));
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string cmdText = "Insert into Croaziere (ID_Croaziera, Tip_Croaziera, Lista_Porturi, Pret, distanta) values (@id, @tip, @lista, @pret, @distanta);";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("id", idCroaziera);
                    cmd.Parameters.AddWithValue("tip", tipCroaziera);
                    cmd.Parameters.AddWithValue("lista", listaStringPorturi);
                    cmd.Parameters.AddWithValue("pret", pret);
                    cmd.Parameters.AddWithValue("distanta", distanta);
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public static string GetCircuitByArray(List<int> porturi)
        {
            string cicruit = "";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string cmdText = "Select Nume_port from Porturi where ID_Port = @id";
                foreach (int item in porturi)
                {
                    using (SqlCommand cmd = new SqlCommand(cmdText, con))
                    {
                        cmd.Parameters.AddWithValue("id", item);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            reader.Read();

                            string answer = reader[0].ToString();

                            cicruit += answer + ",";
                        }
                    }
                }
            }
            return cicruit;
        }

        public static void UpdateCroaziera(int idCroaziera, int nrPasageri, DateTime dataStart, DateTime dataFinal)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string cmdText = "Update Croaziere set Nr_Pasageri = @nrPasager, Data_Start = @dataStart, Data_Final = @dataFinal where ID_Croaziera = @idCroaziera;";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("idCroaziera", idCroaziera);
                    cmd.Parameters.AddWithValue("nrPasager", nrPasageri);
                    cmd.Parameters.AddWithValue("dataStart", dataStart);
                    cmd.Parameters.AddWithValue("dataFinal", dataFinal);

                    cmd.ExecuteNonQuery();
                }

            }
        }

        internal static List<System.Drawing.Point> GetListaPorturi(List<int> listaPorturi)
        {
            List<Point> listaPorturiPuncte = new List<Point>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string cmdText = "Select Pozitie_X, Pozitie_Y from Porturi where ID_Port = @id;";

                foreach (int id in listaPorturi)
                {
                    using (SqlCommand cmd = new SqlCommand(cmdText, con))
                    {
                        cmd.Parameters.AddWithValue("id", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            reader.Read();

                            int x = (int)reader["Pozitie_X"];
                            int y = (int)reader["Pozitie_Y"];

                            listaPorturiPuncte.Add(new Point(x, y));
                        }
                    }
                }
            }

            return listaPorturiPuncte;
        }


    }
}
