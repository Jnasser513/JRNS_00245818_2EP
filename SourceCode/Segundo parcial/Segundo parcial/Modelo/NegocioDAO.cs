using System;
using System.Collections.Generic;
using System.Data;

namespace Segundo_parcial.Modelo
{
    public class NegocioDAO
    {
        public static List<Negocio> getLista()
        {
            string sql = "select * from business";
            var dt = ConnectionDB.realizarConsulta(sql);
            List<Negocio> lista = new List<Negocio>();

            foreach (DataRow fila in dt.Rows)
            {
                Negocio negocio = new Negocio();
                negocio.idBusiness = Convert.ToInt32(fila[0].ToString());
                negocio.name = fila[1].ToString();
                negocio.description = fila[2].ToString();
                lista.Add(negocio);
            }

            return lista;
        }
        public static void crearNegocio(string name, string des)
        {
            string sql = String.Format("insert into business(name, description) " + "values('{0}', '{1}');", name, des);
            
            ConnectionDB.realizarAccion(sql);
        }
        
        public static void eliminarNegocio(string name)
        {
            string sql = String.Format("delete from business where name ='{0}'; ", name);
            
            ConnectionDB.realizarAccion(sql);
        }
    }
}