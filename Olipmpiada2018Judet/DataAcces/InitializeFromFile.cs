using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;

namespace Olipmpiada2018Judet.DataAcces
{
    class InitializeFromFile
    {
        public static void Initialize()
        {
            string filePath = "date.txt";

            using (StreamReader reader = new StreamReader(filePath))
            {

                while (reader.Peek() >= 0)
                {
                    string tableName = reader.ReadLine();

                    string line = reader.ReadLine();
                    int i = 1;
                    while (line != "Itemi:")
                    {
                        string[] tokens = line.Split(';');

                        UtilizatoriTable(tokens, i);

                        line = reader.ReadLine();
                        i++;
                    }

                    line = reader.ReadLine();
                    i = 1;
                    while (line != "Evaluari:")
                    {
                        string[] tokens = line.Split(';');

                        ItemiTable(tokens, i);

                        line = reader.ReadLine();
                        i++;
                    } 
                    
                    line = reader.ReadLine();
                    i = 1;
                    while (reader.Peek()>=0)
                    {
                        string[] tokens = line.Split(';');

                        EvaluariTable(tokens, i);

                        line = reader.ReadLine();
                        i++;
                    }


                }
            }
        }


        private static void UtilizatoriTable(string[] tokens,int id)
        {
            using (SqlConnection con = new SqlConnection(SqlDataAcces.ConnectionString))
            {
                con.Open();
                string cmdText = "Insert into Utilizatori (IdUtilizator, NumePrenumeUtilizator, ParolaUtilizator, EmailUtilizator, ClasaUtilizator)" +
                    "Values (@idutilizator, @nume, @parola, @email, @clasa)";
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("idutilizator", id);
                    cmd.Parameters.AddWithValue("nume", tokens[0]);
                    cmd.Parameters.AddWithValue("parola", tokens[1]);
                    cmd.Parameters.AddWithValue("email", tokens[2]);
                    cmd.Parameters.AddWithValue("clasa", tokens[3]);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static void ItemiTable(string[] tokens, int id)
        {
            using (SqlConnection con = new SqlConnection(SqlDataAcces.ConnectionString))
            {
                con.Open();
                string cmdText = "Insert into Itemi (IdItem, TipItem, EnuntItem, Raspuns1Item, Raspuns2Item,Raspuns3Item,Raspuns4Item,RaspunsCorectItem)" +
                    "Values (@IdItem, @TipItem, @EnuntItem, @Raspuns1Item, @Raspuns2Item,@Raspuns3Item,@Raspuns4Item,@RaspunsCorectItem)";
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("IdItem", id);
                    cmd.Parameters.AddWithValue("TipItem", int.Parse(tokens[0]));
                    cmd.Parameters.AddWithValue("EnuntItem", tokens[1]);
                    cmd.Parameters.AddWithValue("Raspuns1Item", tokens[2]);
                    cmd.Parameters.AddWithValue("Raspuns2Item", tokens[3]);
                    cmd.Parameters.AddWithValue("Raspuns3Item", tokens[4]);
                    cmd.Parameters.AddWithValue("Raspuns4Item", tokens[5]);
                    cmd.Parameters.AddWithValue("RaspunsCorectItem", tokens[6]);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static void EvaluariTable(string[] tokens, int id)
        {
            using (SqlConnection con = new SqlConnection(SqlDataAcces.ConnectionString))
            {
                con.Open();
                string cmdText = "Insert into Evaluari (IdEvaluare, IdElev, DataEvaluare, NotaEvaluare)" +
                    "Values (@IdEvaluare, @IdElev, @DataEvaluare, @NotaEvaluare)";
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("IdEvaluare", id);
                    cmd.Parameters.AddWithValue("IdElev", int.Parse(tokens[0]));
                    cmd.Parameters.AddWithValue("DataEvaluare", tokens[1]);
                    cmd.Parameters.AddWithValue("NotaEvaluare", tokens[2]);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
