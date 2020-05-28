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
            string sql = "SELECT * from appuser";
           
            DataTable dt = ConnectionDB.realizarConsulta(sql);

            List<APPUSER> lista = new List<APPUSER>();
            
            foreach (DataRow fila in dt.Rows)
            {
                APPUSER user = new APPUSER();
                user.idUser = Convert.ToInt32(fila[0].ToString());
                user.fullname = fila[1].ToString();
                user.username = fila[2].ToString();
                user.password = fila[3].ToString();
                user.type = Convert.ToBoolean(fila[4].ToString());
                
                lista.Add(user);
            }
            return lista;
        }
        public static void NewUser(string fullname, string username, string password, bool type)
        {
            string sql = string.Format(
                "INSERT INTO appuser(fullname, username, password, userType) VALUES('{0}', '{1}', '{2}', {3});",
                fullname, username,password, type ? "true" : "false");
            
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
            string sql = String.Format(
                "update appuser set password ='{0}' where username='{1}';", password, username);
            
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