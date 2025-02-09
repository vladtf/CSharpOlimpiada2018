﻿using Olimpiada2016Judet.Models;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Olimpiada2016Judet.DataAcces
{
    internal class SqlDataAcces
    {
        public static string ConnectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\GOOD_FOOD.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";

        public static void RegistrareUtilizator(UserModel utilizator)
        {
            if (!VerificaEmail(utilizator.Email))
            {
                MessageBox.Show("Email deja utilizat!");
                return;
            }

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string cmdText = "Insert into Clienti ( parola, nume, prenume, adresa, email ) " +
                    "values ( @parola, @nume, @prenume, @adresa, @email);";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("parola", utilizator.Parola);
                    cmd.Parameters.AddWithValue("nume", utilizator.Parola);
                    cmd.Parameters.AddWithValue("prenume", utilizator.Parola);
                    cmd.Parameters.AddWithValue("adresa", utilizator.Parola);
                    cmd.Parameters.AddWithValue("email", utilizator.Parola);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static bool VerificaEmail(string email)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string cmdText = "Select * from Clienti where email = @email";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("email", email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
        }

        public static UserModel Autentificare(string email, string parola)
        {
            UserModel utilizator = new UserModel();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string cmdText = "Select * from clienti where email = @email and parola = @parola;";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("parola", parola);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            utilizator = new UserModel
                            {
                                Id = (int)reader["id_client"],
                                Parola = (string)reader["parola"],
                                Nume = (string)reader["nume"],
                                Prenume = (string)reader["prenume"],
                                Adresa = (string)reader["adresa"],
                                Email = (string)reader["email"]
                            };
                        }
                        return utilizator;
                    }
                }
            }
        }

        public static DataTable GetMeniu()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string cmdText = "Select * from Meniu";

                DataTable table = new DataTable();

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(table);

                        return table;
                    }
                }
            }
        }

        public static void ClearDB()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string cmdText = "Delete from Meniu";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}