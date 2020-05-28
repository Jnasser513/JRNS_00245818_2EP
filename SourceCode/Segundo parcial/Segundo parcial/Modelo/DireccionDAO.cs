using System;
using System.Collections.Generic;
using System.Data;

namespace Segundo_parcial.Modelo
{
    public class DireccionDAO
    {
        public static List<Direccion> getLista()
        {
            string sql = "select * from address";
           
            DataTable dt = ConnectionDB.realizarConsulta(sql);

            List<Direccion> lista = new List<Direccion>();
            
            foreach (DataRow fila in dt.Rows)
            {
                Direccion add = new Direccion();
                add.idAddres = Convert.ToInt32(fila[0].ToString());
                add.idUser = Convert.ToInt32(fila[1].ToString());
                add.address = fila[2].ToString();
                
                lista.Add(add);
            }
            return lista;
        }
        
        public static List<Direccion> getLista(APPUSER u)
        {
            string sql = string.Format("SELECT ad.idAddress, ad.address FROM ADDRESS ad " +
                                       "WHERE idUser = {0}", u.idUser);
           
            DataTable dt = ConnectionDB.realizarConsulta(sql);

            List<Direccion> lista = new List<Direccion>();
            
            foreach (DataRow fila in dt.Rows)
            {
                Direccion add = new Direccion();
                add.idAddres = Convert.ToInt32(fila[0].ToString());
                add.idUser = u.idUser;
                add.address = fila[1].ToString();
                
                lista.Add(add);
            }
            return lista;
        }
        
        public static DataTable verDirecciones(APPUSER appuser)
        {
            DataTable dt = null;
            try
            {
                dt = ConnectionDB.realizarConsulta(string.Format("SELECT ad.idAddress, ad.idUser, ad.address FROM ADDRESS ad, APPUSER us " + 
                "WHERE us.username = '{0}' and ad.idUser = us.idUser ", appuser.username));

            }
            catch (Exception)
            {
                return null;
            }

            return dt;
        }
        
        public static void agregarDireccion(string address, string name)
        {
            string sql = String.Format(
                "INSERT INTO ADDRESS(idUser, address) select us.idUser, '{0}'" +
                "from appuser us where us.username = '{1}';",
                address, name);
            
            ConnectionDB.realizarAccion(sql);
        }
        
        public static void eliminarDireccion(string address)
        {
            string sql = String.Format(
                "delete from address where address = '{0}';",
                address);
                    
            ConnectionDB.realizarAccion(sql);
        }
        
        public static void ActualizarDireccion(string old, string newD)
        {
            string sql = String.Format(
                "update address set address = '{0}' WHERE address = '{1}';",
                newD, old);
            
            ConnectionDB.realizarAccion(sql);
        }
    }
}