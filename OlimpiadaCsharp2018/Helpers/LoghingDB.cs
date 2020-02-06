using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using OlimpiadaCsharp2018.Models;

namespace OlimpiadaCsharp2018.Helpers
{
    public class LoghingDB
    {
        public static UserModel Autentificare(string email)
        {
            using( SqlConnection con = new SqlConnection(ConnectionString.String))
            {
                con.Open();
                string cmdText = "Select * from Utilizatori where Convert(VarChar,Email) = @email";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("email", email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            UserModel user = new UserModel
                            {
                                IdUtilizator = (int)reader["IdUtilizator"],
                                Nume = (string)reader["Nume"],
                                Email = (string)reader["Email"],
                                Parola = (string)reader["Parola"]
                            };
                            return user;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        public static void SalvareParolaNoua(string parolaNoua, string email)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.String))
            {
                con.Open();
                string cmdText = "Update Utilizatori set Parola = Convert(varchar,@parolaNoua) where Convert(varchar, Email) = @email";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("parolaNoua", parolaNoua);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
