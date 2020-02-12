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

        public static int VerificaImprumuturi(UtilizatorModel utilizator)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                DateTime data = DateTime.Now.AddDays(-30);
                string cmdText = "Select * from imprumut where email = @email and data_imprumut > @data";

                using (SqlCommand cmd = new SqlCommand(cmdText,con))
                {
                    cmd.Parameters.AddWithValue("email", utilizator.email);
                    cmd.Parameters.AddWithValue("data", data);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            i++;
                        }
                    }
                }
            }
            return i;
        }

        public static void ImprumutaCarte(int idCarte, UtilizatorModel utilizator)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string cmdText = "Insert into imprumut ( id_carte, email, data_imprumut ) values " +
                    "(@idCarte, @email, @data)";

                using (SqlCommand cmd = new SqlCommand(cmdText,con))
                {
                    DateTime data = DateTime.Now;

                    cmd.Parameters.AddWithValue("idCarte", idCarte);
                    cmd.Parameters.AddWithValue("email", utilizator.email);
                    cmd.Parameters.AddWithValue("@data", data);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<ImprumutModel> GetImprumuturiUtilizator(UtilizatorModel utilizator)
        {
            List<ImprumutModel> imprumuturi = new List<ImprumutModel>();
            int i = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string cmdText = "Select c.id_carte, c.titlu, c.autor, i.data_imprumut from carti c, imprumut i where c.id_carte = i.id_carte and i.email = @email";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("email", utilizator.email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            i++;
                            DateTime date = (DateTime)reader[3];
                            ImprumutModel imprumut = new ImprumutModel
                            {
                                Index = i,
                                IDCarte = (int)reader[0],
                                Titlu = (string)reader[1],
                                Autor = (string)reader[2],
                                DataImprumut = date,
                                DataDisponibilitate = date.AddDays(30)

                            };
                            imprumuturi.Add(imprumut);
                        }
                    }
                }
            }
            return imprumuturi;
        }

        public static List<ImprumutModel> GetImprumuturiAn(DateTime anStart, DateTime anEnd)
        {
            List<ImprumutModel> imprumuturi = new List<ImprumutModel>();
            int i = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string cmdText = "Select c.id_carte, c.titlu, c.autor, i.data_imprumut from carti c, imprumut i where c.id_carte = i.id_carte and i.data_imprumut >= @anStart and i.data_imprumut <=@anEnd";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("anStart", anStart);
                    cmd.Parameters.AddWithValue("anEnd", anEnd);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            i++;
                            DateTime date = (DateTime)reader[3];
                            ImprumutModel imprumut = new ImprumutModel
                            {
                                Index = i,
                                IDCarte = (int)reader[0],
                                Titlu = (string)reader[1],
                                Autor = (string)reader[2],
                                DataImprumut = date,
                                DataDisponibilitate = date.AddDays(30)

                            };
                            imprumuturi.Add(imprumut);
                        }
                    }
                }
            }
            return imprumuturi;
        }


        public static List<String> GetCartiCitite()
        {
            List<string> carti = new List<string>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                string cmdText = "Select Titlu from carti c, imprumut i where c.id_carte = i.id_carte;";
                using (SqlCommand cmd = new SqlCommand(cmdText,con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            carti.Add((string)reader[0]);
                        }
                    }
                }
            }

            return carti;
        }
    }
}
