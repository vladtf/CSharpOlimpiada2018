using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using OlimpiadaCSharp.Models;

namespace OlimpiadaCSharp.Helpers
{
    public class DataAcces
    {
        private static string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\OlimpiadaCSharp.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";

        public static List<LocationModel> GetLocations()
        {
            List<LocationModel> locations = new List<LocationModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand command = new SqlCommand("Select * from Localitati", con);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int IDLocation = (int)reader[0];
                        string Nume = (string)reader[1];
                        LocationModel location = new LocationModel();

                        location.Id = IDLocation;
                        location.Localitate = Nume;

                        locations.Add(location);

                    }
                }
            }
            return locations;
        }

        public static List<string> GetImages(int idLocalitate)
        {
            List<string> Images = new List<string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand command = new SqlCommand("Select CaleFisier from Imagini where IDLocalitate=@idlocalitate", con);
                command.Parameters.AddWithValue("idlocalitate", idLocalitate);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Images.Add(reader.GetString(0));
                    }
                }
            }
            return Images;
        }

        public static void SaveData(string connectionString, string filePath)
        {
            ClearDB(connectionString);
            using (StreamReader file = new StreamReader(filePath))
            {
                while (file.Peek() >= 0)
                {
                    string[] tokens = file.ReadLine().Split('*').Select(x => x.Trim()).ToArray();
                    InsertLocalitate(connectionString, tokens[0]);
                    switch (tokens[1].Trim())
                    {
                        case "ocazional":
                            InsterOcazional(tokens, connectionString);
                            break;
                        case "anual":
                            InsertOthers(tokens, connectionString);
                            break;
                        case "lunar":
                            InsertOthers(tokens, connectionString);
                            break;
                        default:
                            break;
                    }
                }
            }

        }

        private static void InsertOthers(string[] tokens, string connectionString)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("Select IDLocalitate from Localitati where Nume=@localitate", con);
                command.Parameters.AddWithValue("localitate", tokens[0]);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int IDLocalitate = reader.GetInt32(0);
                reader.Dispose();
                command.Dispose();

                command = new SqlCommand("Insert into Planificari(IDLocalitate,Frecventa,Ziua) values(@IDLocalitate,@frecventa,@ziua)", con);
                command.Parameters.AddWithValue("IDLocalitate", IDLocalitate);
                command.Parameters.AddWithValue("frecventa", tokens[1]);
                command.Parameters.AddWithValue("ziua", tokens[2]);
                command.ExecuteNonQuery();
                command.Dispose();

                for (int i = 3; i < tokens.Length; i++)
                {
                    command = new SqlCommand("Insert into Imagini(IDLocalitate,CaleFisier) values(@idlocalitate,@calefisier)", con);
                    command.Parameters.AddWithValue("idlocalitate", IDLocalitate);
                    command.Parameters.AddWithValue("calefisier", tokens[i]);
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
            }
        }

        private static void InsterOcazional(string[] tokens, string connectionString)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("Select IDLocalitate from Localitati where Nume=@localitate", con);
                command.Parameters.AddWithValue("localitate", tokens[0]);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int IDLocalitate = reader.GetInt32(0);
                reader.Dispose();
                command.Dispose();

                command = new SqlCommand("Insert into Planificari(IDLocalitate,Frecventa,DataStart,DataStop) values(@IDLocalitate,@frecventa,@datastart,@datastop)", con);
                command.Parameters.AddWithValue("IDLocalitate", IDLocalitate);
                command.Parameters.AddWithValue("frecventa", tokens[1]);
                DateTime datastart = DateTime.ParseExact(tokens[2], "dd.MM.yyyy", null);
                command.Parameters.AddWithValue("datastart", datastart);
                DateTime datastop = DateTime.ParseExact(tokens[3], "dd.MM.yyyy", null);
                command.Parameters.AddWithValue("datastop", datastop);
                command.ExecuteNonQuery();
                command.Dispose();

                for (int i = 4; i < tokens.Length; i++)
                {
                    command = new SqlCommand("Insert into Imagini(IDLocalitate,CaleFisier) values(@idlocalitate,@calefisier)", con);
                    command.Parameters.AddWithValue("idlocalitate", IDLocalitate);
                    command.Parameters.AddWithValue("calefisier", tokens[i]);
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
            }
        }


        private static void InsertLocalitate(string connectionString, string localitate)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("Insert into Localitati(Nume) values(@localitate)", con);
                command.Parameters.AddWithValue("localitate", localitate);
                command.ExecuteNonQuery();
                con.Close();
            }
        }

        private static void ClearDB(string connectionString)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("Delete from Imagini", con);
                command.ExecuteNonQuery();
                command = new SqlCommand("Delete from Localitati", con);
                command.ExecuteNonQuery();
                command = new SqlCommand("Delete from Planificari", con);
                command.ExecuteNonQuery();
            }
        }
    }
}
