using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Olimpiada2016Judet.DataAcces
{
    public class FinalizareComanda
    {
        public static FinalizareComanda(int idClient, int idProdus, int cantitate)
        {
            using (SqlConnection con = new SqlConnection(SqlDataAcces.ConnectionString))
            {
                con.Open();

                string cmdText = "Insert into Comenzi( id_client, data_comanda) values (@id_client, @data_Comanda)";
                DateTime data = DateTime.Now;
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("id_client", idClient);
                    cmd.Parameters.AddWithValue("data_Comanda", data);

                    cmd.ExecuteNonQuery();
                }

                int idComanda = IdComanda(data, idClient);

                cmdText = "Insert into Comenzi( id_comanda, id_produs, cantitate) values (@id_comanda, @id_produs, @id_cantitate)";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("id_comanda", idComanda);
                    cmd.Parameters.AddWithValue("id_produs", idClient);
                    cmd.Parameters.AddWithValue("id_cantitate", cantitate);
                }
            }

        }


        private static int IdComanda(DateTime data, int idClient)
        {

            using (SqlConnection con = new SqlConnection(SqlDataAcces.ConnectionString))
            {
                con.Open();
                string cmdText = "Select id_comanda from Comenzi where id_client = @idClient and data_comanda = @data";

                using (SqlCommand cmd = new SqlCommand(cmdText,con))
                {
                    cmd.Parameters.AddWithValue("idClient", idClient);
                    cmd.Parameters.AddWithValue("data", data);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();

                        return (int)reader[0];
                    }
                }
            }
 
        }
    }

}