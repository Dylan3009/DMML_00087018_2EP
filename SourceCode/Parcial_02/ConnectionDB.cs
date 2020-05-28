using System.Data;
using Npgsql;

namespace Parcial_02
{
    public static class ConnectionDB
    {
        private static string sConnection = 
            "Server=127.0.0.1;Port=5432;UserId=postgres;Password=uca;Database=Hugo";
        
        public static DataTable ExecuteQuery(string sql)
        {
            NpgsqlConnection conn = new NpgsqlConnection(sConnection);
            DataSet ds = new DataSet();
            
            conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(ds);
            conn.Close();
            
            return ds.Tables[0];
        }

        public static void ExecuteNonQuery(string sql)
        {
            NpgsqlConnection conn = new NpgsqlConnection(sConnection);
            
            conn.Open();
            NpgsqlCommand nc = new NpgsqlCommand(sql, conn);
            nc.ExecuteNonQuery();
            conn.Close();
        }
    }
}