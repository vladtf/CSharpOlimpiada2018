using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Olimpiada2019Judet.Models;
using System.Data.SqlClient;

namespace Olimpiada2019Judet.DataAcces
{
    public static class SqlDataAcces
    {
        public static string ConnectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\FreeBook.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";

        public static void Registrare(string connectionStrin, UtilizatorModel utilizator)
        {
            using (SqlConnection con = new SqlConnection(connectionStrin))
            {
                con.Open();
                string cmdText = "Insert into utilizatori (email,parola,nume,prenume) values (@email,@parola,@nume,@prenume);";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("email", utilizator.email);
                    cmd.Parameters.AddWithValue("parola", utilizator.parola);
                    cmd.Parameters.AddWithValue("nume", utilizator.nume);
                    cmd.Parameters.AddWithValue("prenume", utilizator.prenume);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static UtilizatorModel Logare(string connectionStrin, string email, string parola)
        {
            UtilizatorModel utilizator = new UtilizatorModel();
            using (SqlConnection con = new SqlConnection(connectionStrin))
            {
                con.Open();
                string cmdText = "Select email,parola,nume,prenume from utilizatori where email = @email and parola = @parola";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("parola", parola);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            utilizator = new UtilizatorModel
                            {
                                email = (string)rdr["email"],
                                parola = (string)rdr["parola"],
                                nume = (string)rdr["nume"],
                                prenume = (string)rdr["prenume"]
                            };
                        }
                        
                    }
                }
            }

            return utilizator;
        }
    }
}
