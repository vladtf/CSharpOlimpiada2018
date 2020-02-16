using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Olimpiada2016Judet.Models;
using System.Data.SqlClient;

namespace Olimpiada2016Judet.DataAcces
{
    public static class GetAllCommands
    {

        public static List<MeniuModel> Get(int idUtilizator)
        {
            List<MeniuModel> comenzi = new List<MeniuModel>();

            using (SqlConnection con = new SqlConnection(SqlDataAcces.ConnectionString))
            {
                con.Open();

                string cmdText = "Select kcal, denumire_produs " +
                    "from Comenzi c, Subcomenzi s, Meniu m " +
                    "where c.id_client = @id and c.id_comanda = s.id_comanda and s.id_produs = m.id_produs;";

                using (SqlCommand cmd = new SqlCommand(cmdText,con))
                {
                    cmd.Parameters.AddWithValue("id", idUtilizator);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MeniuModel item = new MeniuModel
                            {
                                DenumereaProdus = (string)reader["denumire_produs"],
                                KCal = (int)reader["kcal"]
                            };

                            comenzi.Add(item);
                        }
                    }
                }
            }

            return comenzi;
        }
    }
}
