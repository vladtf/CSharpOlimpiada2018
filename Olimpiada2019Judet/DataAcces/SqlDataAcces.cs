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
    }
}
