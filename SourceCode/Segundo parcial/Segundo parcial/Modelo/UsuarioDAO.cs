using System;
using System.Data;

namespace Segundo_parcial.Modelo
{
    public class UsuarioDAO
    {
        public static APPUSER GetUser(String nombre, String contra)
        {
            string sql = String.Format("SELECT * FROM usuario WHERE nombre = '{0}' AND contrasena = '{1}';", nombre, contra);

            DataTable dt = ConnectionDB.ExecuteQuery(sql);

                APPUSER appuser = new APPUSER();
                foreach (DataRow fila in dt.Rows)
                {
                    appuser.user = fila[0].ToString();
                    appuser.password = fila[1].ToString();
                    appuser.admin = fila[2].ToString();
                }
            return u;
        }

        public static List<Usuario> GetLista()
        {
            string sql = "SELECT * FROM usuario";

            DataTable dt = ConnectionDB.ExecuteQuery(sql);
            
            List<Usuario> lista = new List<Usuario>();
            foreach (DataRow fila in dt.Rows)
            {
                Usuario u = new Usuario();
                u.usuario = fila[0].ToString();
                u.contrasena = fila[1].ToString();
                u.tipo = fila[2].ToString();
                lista.Add(u);
            }

            return lista;
        }
        public static void CrearNuevo(string usuario, string contrasena, string tipo)
        {
            string sql = String.Format(
                "INSERT INTO usuario(nombre, contrasena, tipo) VALUES ('{0}', '{1}', '{2}');",
                usuario, contrasena, tipo);

                ConnectionDB.ExecuteNonQuery(sql);
        }
        public static void ActualizarPermisos(string usuario, string tipo)
        {
            string sql = string.Format(
                "UPDATE usuario SET tipo='{0}' WHERE nombre='{1}';",
                tipo,usuario);
            
            ConnectionDB.ExecuteNonQuery(sql);
        }

        public static void EliminarUsuario(string usuario)
        {
            string sql = string.Format(
                "DELETE FROM usuario WHERE nombre='{0}';",
                usuario);
            
            ConnectionDB.ExecuteNonQuery(sql);
        }
    }
}