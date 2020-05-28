using System;
using System.Collections.Generic;
using System.Data;

namespace Segundo_parcial.Modelo
{
    public class ProductoDAO
    {
        public static List<Producto> getLista()
        {
            string sql = "SELECT * FROM product";
           
            DataTable dt = ConnectionDB.realizarConsulta(sql);

            List<Producto> lista = new List<Producto>();
            
            foreach (DataRow fila in dt.Rows)
            {
                Producto prod = new Producto();
                prod.idProduct = Convert.ToInt32(fila[0].ToString());
                prod.idBusiness = Convert.ToInt32(fila[1].ToString());
                prod.name = fila[2].ToString();
                
                lista.Add(prod);
            }
            return lista;
        }
        
        public static void agregarProducto(Producto p, Negocio b)
        {
            string sql = String.Format("insert into product(idbusiness, name) " +
                                       "select bus.idbusiness, '{0}' from business bus where bus.name = '{1}' ", p.name, b.name);
                
            ConnectionDB.realizarAccion(sql);
        }
        
        public static void eliminarProducto(string name)
        {
            string sql = String.Format("delete from product where name = '{0}';", name);
                            
            ConnectionDB.realizarAccion(sql);
        } 
    }
}