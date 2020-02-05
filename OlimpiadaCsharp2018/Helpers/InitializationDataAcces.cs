using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using OlimpiadaCsharp2018.Models;
using System.Globalization;

namespace OlimpiadaCsharp2018.Helpers
{
    public class InitializationDataAcces
    {
        private static string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\vladu\Documents\CentenarDB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";

        public static void InitializeDB()
        {
            ClearDB();

            string filePath = "Utilizatori.txt";
            using (StreamReader file = new StreamReader(filePath))
            {
                int i = 1;
                while (file.Peek() >= 0)
                {
                    string[] tokens = file.ReadLine().Split('*').ToArray();
                    UserModel utilizator = new UserModel { Nume = tokens[0], Parola = tokens[1], Email = tokens[2], IdUtilizator = i };

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string cmdText = "Insert into Utilizatori(IdUtilizator,Nume,Parola,Email) values (@idutilizator,@nume,@parola,@email)";
                        using (SqlCommand cmd = new SqlCommand(cmdText, con))
                        {
                            cmd.Parameters.AddWithValue("idutilizator", utilizator.IdUtilizator);
                            cmd.Parameters.AddWithValue("nume", utilizator.Nume);
                            cmd.Parameters.AddWithValue("parola", utilizator.Parola);
                            cmd.Parameters.AddWithValue("email", utilizator.Email);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    i++;
                }
            }

            filePath = "Lectii.txt";

            using (StreamReader file = new StreamReader(filePath))
            {
                int i = 1;
                while (file.Peek() >= 0)
                {
                    string[] tokens = file.ReadLine().Split('*').ToArray();
                    LectieModel lectie;

                    if (tokens.Length == 5)
                    {

                        lectie = new LectieModel
                        {
                            IdLectie = i,
                            IdUtilizator = Int32.Parse(tokens[0]),
                            TitluLectie = tokens[1],
                            Regiune = tokens[2],
                            NumeImagine = tokens[3],
                            Datacreare = DateTime.ParseExact(tokens[4], "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                        };
                    }
                    else
                    {
                        lectie = new LectieModel
                        {
                            IdLectie = i,
                            IdUtilizator = Int32.Parse(tokens[0]),
                            TitluLectie = "",
                            Regiune = tokens[1],
                            NumeImagine = tokens[2],
                            Datacreare = DateTime.ParseExact(tokens[3], "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                        };
                    }
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string cmdText = "Insert into Lectii(IdLectie,IdUtilizator,TitluLectie,Regiune,DataCreare,NumeImagine) " +
                        "values (@idlectie,@idutlizator,@titlulectie,@regiune,@datacreare,@numeimagine)";
                        using (SqlCommand cmd = new SqlCommand(cmdText, con))
                        {
                            cmd.Parameters.AddWithValue("idlectie", lectie.IdLectie);
                            cmd.Parameters.AddWithValue("idutlizator", lectie.IdUtilizator);
                            cmd.Parameters.AddWithValue("titlulectie", lectie.TitluLectie);
                            cmd.Parameters.AddWithValue("regiune", lectie.Regiune);
                            cmd.Parameters.AddWithValue("datacreare", lectie.Datacreare);
                            cmd.Parameters.AddWithValue("numeimagine", lectie.NumeImagine + ".bmp");

                            cmd.ExecuteNonQuery();
                        }
                    }
                    i++;
                }
            }

        }

        public static void ClearDB()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string cmdText = "Delete from Lectii";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    command.ExecuteNonQuery();
                }
                cmdText = "Delete from Utilizatori";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
