using System.Data;
using Npgsql;

namespace Segundo_parcial.Modelo
{
    public class ConnectionDB
    {
        private static string host = "127.0.0.1",
            database = "HUGOAPP2P",
            userId = "postgres",
            password = "gaseosa1234";
        
        private static string CadenaConexion =
            $"Host={host};Port=5432;User Id={userId};Password={password};Database={database}";
        
        public static DataTable realizarConsulta(string sql)
        {
            NpgsqlConnection conn = new NpgsqlConnection(CadenaConexion);
            DataSet ds = new DataSet();
            
            conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(ds);
            conn.Close();
            
            return ds.Tables[0];
        }

        public static void realizarAccion(string sql)
        {
            NpgsqlConnection conn = new NpgsqlConnection(CadenaConexion);
            
            conn.Open();
            NpgsqlCommand nc = new NpgsqlCommand(sql, conn);
            nc.ExecuteNonQuery();
            conn.Close();
        }
    }
}