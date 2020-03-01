using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using Olimpiada2019National.Models;
using Olimpiada2019National.Helpers;

namespace Olimpiada2019National.DataAcces
{
    public class SqlDataAcces
    {
        public static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Biblioteca.mdf;Integrated Security=True;Connect Timeout=30";

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
                            else if (DateTime.TryParseExact(value, "MM/dd/yyyy hh/mm/ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                            {
                                tokens.Add(String.Format("Convert( datetime, '{0}')", date));
                            }
                            else if (tableName == "utilizatori" && i == 3)
                            {
                                tokens.Add(String.Format("'{0}'", CriptareParola.Criptare(value)));
                            }
                            else
                            {
                                tokens.Add(String.Format("'{0}'", value));
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

        public static UserModel Autentificare(string email, string parola)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string cmdText = "Select * from Utilizatori where Email = @email and Parola = @parola";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("parola", parola);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();

                        UserModel utilizator = new UserModel();
                        if (reader.HasRows)
                        {
                            utilizator = new UserModel
                            {
                                ID = (int)reader["IdUtilizator"],
                                TipUtilizator = (int)reader["TipUtilizator"],
                                NumePenume = (string)reader["NumePrenume"],
                                Email = (string)reader["Email"],
                                Parola = (string)reader["Parola"]
                            };
                        }
                        return utilizator;
                    }
                }
            }
        }

        public static void InregistreazaUtilizator(UserModel utilizator)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string cmdText = "Insert into utilizatori values (@tip, @nume, @email, @parola)";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("tip", utilizator.TipUtilizator);
                    cmd.Parameters.AddWithValue("nume", utilizator.NumePenume);
                    cmd.Parameters.AddWithValue("email", utilizator.Email);
                    cmd.Parameters.AddWithValue("parola", utilizator.Parola);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static int GetUserIDByEmail(string email)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string cmdText = "Select IdUtilizator from Utilizatori where Email = @email";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("email", email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();

                        int id = (int)reader[0];

                        return id;
                    }
                }
            }
        }
    }
}