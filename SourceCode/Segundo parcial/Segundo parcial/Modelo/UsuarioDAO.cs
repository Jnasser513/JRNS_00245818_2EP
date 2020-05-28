using System;
using System.Collections.Generic;
using System.Data;
using Segundo_parcial.Controlador;

namespace Segundo_parcial.Modelo
{
    public class UsuarioDAO
    {
        public static APPUSER GetUser(String user, String password)
        {
            string sql = String.Format("SELECT * FROM appuser WHERE username = '{0}' AND password = '{1}';", user, password);

            DataTable dt = ConnectionDB.realizarConsulta(sql);

            APPUSER add = new APPUSER();
            foreach (DataRow fila in dt.Rows)
            {
                add.username = fila[1].ToString();
                add.password = fila[2].ToString();
            }
            return add;
        }

        public static List<APPUSER> GetLista()
        {
            string sql = "SELECT * FROM appuser";

            DataTable dt = ConnectionDB.realizarConsulta(sql);
            
            List<APPUSER> lista = new List<APPUSER>();
            foreach (DataRow fila in dt.Rows)
            {
                APPUSER u = new APPUSER();
                u.fullname = fila[0].ToString();
                u.username = fila[1].ToString();
                u.password = fila[2].ToString();
                u.type = Convert.ToBoolean(fila[3].ToString());
                lista.Add(u);
            }

            return lista;
        }
        public static void NewUser(string fullname, string username, string password, bool type)
        {
            string sql = String.Format(
                "INSERT INTO appuser(fullname, username, password, userType) VALUES ('{0}', '{1}', '{2}', '{true}');",
                fullname, username, password, type);

                ConnectionDB.realizarAccion(sql);
        }
        public static void ActualizarPermiso(string username, bool userType)
        {
            string sql = string.Format(
                "UPDATE appuser SET userType='{true}' WHERE username='{1}';",
                userType, username);

            ConnectionDB.realizarAccion(sql);
        }
        public static void ActualizarPassword(string username, string password)
        {
            string sql = string.Format(
                "UPDATE appuser SET password='{0}' WHERE username='{1}';",
                username, password);
            
            ConnectionDB.realizarAccion(sql);
        }

        public static void EliminarUsuario(string username)
        {
            string sql = string.Format(
                "DELETE FROM appuser WHERE username='{0}';",
                username);
            
            ConnectionDB.realizarAccion(sql);
        }
    }
}