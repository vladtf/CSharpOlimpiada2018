using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Olipmpiada2018Judet.Models;
using System.Data.SqlClient;

namespace Olipmpiada2018Judet.DataAcces
{
    public class SqlDataAcces
    {
        public static string ConnectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\eLearning1918.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";

        public static UserModel Autentificare(string connectionString, string email)
        {
            UserModel utilizator = new UserModel();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string cmdText = "Select * from Utilizatori where EmailUtilizator = @email";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("email", email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();

                        if (reader.HasRows)
                        {
                            utilizator = new UserModel
                            {
                                IDUtilizator = (int)reader["IdUtilizator"],
                                Nume = (string)reader["NumePrenumeUtilizator"],
                                Parola = (string)reader["ParolaUtilizator"],
                                Email = (string)reader["EmailUtilizator"],
                                Clasa = (string)reader["ClasaUtilizator"]
                            };
                        }
                    }
                }
            }

            return utilizator;
        }
    }
}
