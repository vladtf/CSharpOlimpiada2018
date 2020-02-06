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
        public static UserModel Autentificare(string email, string parola)
        {
            using( SqlConnection con = new SqlConnection(ConnectionString.String))
            {
                con.Open();
                string cmdText = "Select * from Utilizatori where Convert(VarChar,Email) = @email and Convert(varchar,Parola) = @parola ";

                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {

                    cmd.Parameters.AddWithValue("parola", parola);
                    cmd.Parameters.AddWithValue("email", email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        UserModel user = new UserModel
                        {
                            IdUtilizator = (int)reader["IdUtilizator"],
                            Nume = (string)reader["Nume"],
                            Email = (string)reader["Email"]
                        };
                        return user;
                    }
                }
            }
        }
    }
}
