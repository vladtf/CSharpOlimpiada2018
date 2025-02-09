﻿using Olipmpiada2018Judet.Models;
using System;
using System.Collections.Generic;
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

        public static List<ItemModel> GetAllItems(string connectionString)
        {
            List<ItemModel> items = new List<ItemModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string cmdText = "Select * from Itemi";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ItemModel item = new ItemModel
                            {
                                IdItem = (int)reader["IdItem"],
                                TipItem = (int)reader["TipItem"],
                                EnuntItem = (string)reader["EnuntItem"],
                                Raspuns1Item = (string)reader["Raspuns1Item"],
                                Raspuns2Item = (string)reader["Raspuns2Item"],
                                Raspuns3Item = (string)reader["Raspuns3Item"],
                                Raspuns4Item = (string)reader["Raspuns4Item"],
                                RaspunscorectItem = (string)reader["RaspunscorectItem"]
                            };

                            items.Add(item);
                        }
                    }
                }
            }

            return items;
        }

        public static List<MarkModel> GetAllMarks(string connectionString, UserModel utilizator)
        {
            List<MarkModel> marks = new List<MarkModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string cmdText = "Select * from Evaluari";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MarkModel nota = new MarkModel();
                            if ((int)reader["IdElev"] == utilizator.IDUtilizator)
                            {
                                nota.Data = (DateTime)reader["DataEvaluare"];
                                nota.Nota = (int)reader["NotaEvaluare"];
                                marks.Add(nota);
                            }
                            MarkModel.ToateNotele.Add((int)reader["NotaEvaluare"]);
                        }
                    }
                }
            }

            return marks;
        }

        public static void SalvareNota(string connectionString, int IdUtilizator, int nota)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string cmdText = "Insert into Evaluari(IdElev, DataEvaluare, NotaEvaluare) values (@idelev, @data, @nota);";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("idelev", IdUtilizator);
                    cmd.Parameters.AddWithValue("data", DateTime.Now);
                    cmd.Parameters.AddWithValue("nota", nota);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}