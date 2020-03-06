using Olimpiada2019National.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Olimpiada2019National.DataAcces
{
    public class PersonalDataAcces
    {
        public static List<RezervareModel> GetAllRezervarationByUserId(int userId, string connectionString)
        {
            List<RezervareModel> loans = new List<RezervareModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string cmdText = "Select * from Rezervari where IdCititor = @id";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("id", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RezervareModel loan = new RezervareModel
                            {
                                IdRezerare = (int)reader["IdRezervare"],
                                IdCititor = (int)reader["IdCititor"],
                                IdCarte = (int)reader["IdCarte"],
                                DataRezervare = (DateTime)reader["DataRezervare"],
                                StatusRezervare = (int)reader["StatusRezervare"]
                            };

                            loans.Add(loan);
                        }
                    }
                }
            }

            return loans;
        }

        public static List<ImprumutModel> GetAllLaondsByUserId(int userId, string connectionString)
        {
            List<ImprumutModel> imprumuturi = new List<ImprumutModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string cmdText = "Select * from Imprumuturi where IdCititor = @id";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("id", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ImprumutModel imprumut = new ImprumutModel
                            {
                                IdImprumut = (int)reader["IdImprumut"],
                                IdCititor = (int)reader["IdCititor"],
                                IdCarte = (int)reader["IdCarte"],
                                DataImprumut = (DateTime)reader["DataImprumut"]
                            };
                            try
                            {
                                imprumut.DataRestituire = (DateTime)reader["DataRestituire"];
                            }
                            catch { }

                            imprumut.Carte = GetBookById(imprumut.IdCarte, connectionString);

                            imprumuturi.Add(imprumut);
                        }
                    }
                }
            }

            return imprumuturi;
        }

        private static BookModel GetBookById(int idCarte, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string cmdText = "Select * from Carti where IdCarte = @id";

                using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                {
                    cmd.Parameters.AddWithValue("id", idCarte);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();

                        BookModel book = new BookModel
                        {
                            IdCarte = (int)reader[0],
                            Titlu = (string)reader[1],
                            Autor = (string)reader[2],
                            NrPag = (int)reader[3]
                        };
                        return book;
                    }
                }
            }
        }

        public static void ReturneazaCarte(int idImprumut, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string cmdText = "Update Imprumuturi Set DataRestituire = @data where IdImprumut = @id";

                using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                {
                    cmd.Parameters.AddWithValue("data", DateTime.Now);
                    cmd.Parameters.AddWithValue("id", idImprumut);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}