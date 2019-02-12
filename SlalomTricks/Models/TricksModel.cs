using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SlalomTricks.Models
{
    public class TricksModel : IDisposable
    {
        private SqlConnection connection;

        public TricksModel()
        {
            string strConn = "Data Source=localhost\\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True;connection timeout=90; Initial Catalog=SlalomDB";
            connection = new SqlConnection(strConn);
            Console.WriteLine("connection String: \r\n "+strConn);
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public List<Trick> Read()
        {
            List<Trick> lista = new List<Trick>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"Select * FROM Tricks";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Trick trick = new Trick();
                trick.IdTrick = (int)reader["idTrick"];
                trick.Name = (string)reader["Name"];
                trick.Family = (string)reader["Family"];
                trick.Value = (int)reader["Value"];
                lista.Add(trick);
            }

            return lista;
        }

        public void Create(Trick trick)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"Insert into Tricks VALUES (@name, @value, @family)";

            cmd.Parameters.AddWithValue("@name", trick.Name);
            cmd.Parameters.AddWithValue("@value", trick.Value);
            cmd.Parameters.AddWithValue("@family", trick.Family);

            cmd.ExecuteNonQuery();
        }

        public void Update(Trick trick)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"Update Tricks set Name=@name, Value=@value, Family=@family where idTrick = @idTrick";

            cmd.Parameters.AddWithValue("@name", trick.Name);
            cmd.Parameters.AddWithValue("@family", trick.Family);
            cmd.Parameters.AddWithValue("@value", trick.Value);
            cmd.Parameters.AddWithValue("@idTrick", trick.IdTrick);

            cmd.ExecuteNonQuery();

        }

        public void Delete(Trick trick)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"Delete from Tricks WHERE idTrick = @idTrick";

            cmd.Parameters.AddWithValue("@idTrick", trick.IdTrick);

            cmd.ExecuteNonQuery();

        }

    }
}
