using Olimpiada2016Judet.Models;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Olimpiada2016Judet.DataAcces
{
    public class InitializareDB
    {
        public static void Initializare()
        {
            SqlDataAcces.ClearDB();
            string filePath = "Resurse\\meniu.txt";

            using (StreamReader reader = new StreamReader(filePath))
            {
                reader.ReadLine();

                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLine().Split(';').ToList();

                    if (line[0] == "")
                    {
                        break;
                    }
                    MeniuModel meniu = new MeniuModel
                    {
                        Id = int.Parse(line[0]),
                        DenumereaProdus = line[1],
                        Descriere = line[2],
                        Pret = int.Parse(line[3]),
                        KCal = int.Parse(line[4]),
                        Felul = int.Parse(line[5])
                    };

                    using (SqlConnection con = new SqlConnection(SqlDataAcces.ConnectionString))
                    {
                        con.Open();

                        string cmdText = "Insert into Meniu (id_produs, denumire_produs, descriere, pret, kcal, felul) " +
                            "Values (@id, @nume, @descriere, @pret, @kcal, @felul) ;";

                        using (SqlCommand cmd = new SqlCommand(cmdText, con))
                        {
                            cmd.Parameters.AddWithValue("id", meniu.Id);
                            cmd.Parameters.AddWithValue("nume", meniu.DenumereaProdus);
                            cmd.Parameters.AddWithValue("descriere", meniu.Descriere);
                            cmd.Parameters.AddWithValue("pret", meniu.Pret);
                            cmd.Parameters.AddWithValue("kcal", meniu.KCal);
                            cmd.Parameters.AddWithValue("felul", meniu.Felul);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}