using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OlimpiadaCsharp2018.Models;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace OlimpiadaCsharp2018.Helpers
{
    public class DataAcces
    {
        private static string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\CentenarDB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";

        public static List<LectieModel> GetLectii()
        {
            List<LectieModel> lectii = new List<LectieModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string cmdText = "Select * from Lectii";
                using (SqlCommand cmd = new SqlCommand(cmdText,con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LectieModel lectie = new LectieModel
                            {
                                IdLectie = (int)reader["IdLectie"],
                                IdUtilizator = (int)reader["IdUtilizator"],
                                TitluLectie = reader["TitluLectie"].ToString(),
                                Regiune = reader["Regiune"].ToString(),
                                Datacreare = (DateTime)reader["DataCreare"],
                                NumeImagine = reader["NumeImagine"].ToString()
                            };
                            lectii.Add(lectie);
                        }
                    }
                }
            }

            return lectii;
        }

        public static UserModel GetUser(int idUtilizator)
        {
            UserModel utilizator;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string cmdText = "Select * from Utilizatori where IdUtilizator = @idutilizator";
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("idutilizator", idUtilizator);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        utilizator = new UserModel
                        {
                            IdUtilizator = (int)reader["IdUtilizator"],
                            Nume = (string)reader["Nume"],
                            Email = (string)reader["Email"]
                        };
                    }
                }
            }

            return utilizator;
        }
    }
}
